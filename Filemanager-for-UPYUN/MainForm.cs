using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Filemanager_for_UPYUN
{
    public partial class MainForm : Form
    {
        #region Variable

        const char sortAsc = '↑';//排序标记，升序
        const char sortDesc = '↓';//排序标记，降序
        readonly string dirSeparator = Path.DirectorySeparatorChar.ToString();//目录分隔符
        UpYun upYun;
        string curPath = "";//当前路径
        string downPath = "D:\\upyun";//下载路径
        DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);//用于计算最后修改时间
        Stack<string> urlForwardStack = new Stack<string>();//前进栈
        Stack<string> urlBackStack = new Stack<string>();//后退栈
        HashSet<char> invalidChars = new HashSet<char>();//无效字符集

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(UpYun upY, int bucketUsage)
        {
            InitializeComponent();
            this.upYun = upY;
            tslBucketUsage.Text = GetSizeWithUnit(bucketUsage);
        }

        #endregion

        #region Private Mothods

        /// <summary>
        /// 刷新空间使用情况
        /// </summary>
        private void RefreshBucketUsage()
        {
            try
            {
                int bucketUsage = this.upYun.getBucketUsage();
                tslBucketUsage.Text = GetSizeWithUnit(bucketUsage); ;

            }
            catch { }
        }

        /// <summary>
        /// 初使化无效字符集，保存在invalidChars中
        /// </summary>
        private void InitInvalidChars()
        {
            char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
            char[] invalidPathChars = Path.GetInvalidPathChars();
            foreach (char c in invalidFileNameChars)
            {
                invalidChars.Add(c);
            }
            foreach (char c in invalidPathChars)
            {
                invalidChars.Add(c);
            }
            invalidChars.Add('#');
            invalidChars.Add('.');
        }

        /// <summary>
        /// 设置右键菜单
        /// </summary>
        /// <param name="mt"></param>
        private void SetContextMenu(MenuType mt)
        {
            if (mt == MenuType.Default)
            {
                lvList.ContextMenuStrip = defaultContextMenu;
            }
            else
            {
                lvList.ContextMenuStrip = itemContextMenu;
                bool isDir = (mt == MenuType.Dir);
                itemMenuItem_OpenDir.Visible = isDir;
            }

        }

        /// <summary>
        /// 显示文件列表
        /// </summary>
        /// <param name="path"></param>
        private void ShowFileList(string path)
        {
            if (!bgWorker.IsBusy)
            {
                ThreadArgument arg = new ThreadArgument()
                {
                    WorkType = WorkType.Default,
                    Data = path
                };
                panLoading.Visible = true;
                bgWorker.RunWorkerAsync(arg);
            }
        }

        /// <summary>
        /// 显示前进行清理操作
        /// </summary>
        private void ClearListViewBeforeShow()
        {
            lvList.Items.Clear();
            SetUrlBar();
            chkAll.Checked = false;
            SetButtonEnabled(false);
            SetContextMenu(MenuType.Default);
        }

        /// <summary>
        /// 获取指定目录下的所有文件和目录
        /// </summary>
        /// <param name="dirPath">目录名</param>
        /// <returns></returns>
        private List<FolderItem> GetDirFiles(string dirPath)
        {
            return this.upYun.readDir(dirPath);
        }

        /// <summary>
        /// 绑定到ListView
        /// </summary>
        /// <param name="itemList"></param>
        private void BindToListView(List<FolderItem> itemList)
        {
            ClearListViewBeforeShow();
            if (itemList == null || itemList.Count == 0)
            {
                //MessageUtil.Show("没有找到相关文件！");
                return;
            }

            //按类型和扩展名排序
            var tempList = itemList.OrderBy(p => p.FileType).ThenBy(p => Path.GetExtension(p.FileName));
            foreach (FolderItem item in tempList)
            {
                //转换为实际的北京时间
                string modiDate = new DateTime(TimeSpan.FromSeconds(item.Number).Add(new TimeSpan(Jan1st1970.Ticks)).Ticks).AddHours(8).ToString("yyyy-MM-dd HH:mm");
                string fileType = item.FileType;
                string fileName = item.FileName;
                int fileSize = item.Size;
                ListViewItem lvItem = new ListViewItem(new string[] { 
                        fileName,
                        fileType == FileType.Dir ? "-" : GetSizeWithUnit(fileSize),
                        modiDate
                    });
                lvItem.Tag = item;//用Tag来保存item，以便后续使用
                lvList.Items.Add(lvItem);
                //获取默认打开图片
                string ext = "";
                if (fileType == FileType.File)
                {
                    ext = Path.GetExtension(item.FileName);
                }
                else if (fileType == FileType.Dir)
                {
                    ext = "DIR";
                }
                //绑定到ImageList中
                if (!imgSmallList.Images.ContainsKey(ext))
                {
                    if (fileType == FileType.File)
                    {
                        Icon smallIcon = IconUtils.GetIconForFileExtension(ext, false, false);
                        Icon largeIcon = IconUtils.GetIconForFileExtension(ext, true, false);
                        imgSmallList.Images.Add(ext, smallIcon);
                        imageLargeList.Images.Add(ext, largeIcon);
                    }
                    else if (fileType == FileType.Dir)
                    {
                        Icon smallIcon = IconUtils.GetIconForFolder(false, false);
                        Icon largeIcon = IconUtils.GetIconForFolder(true, false);
                        imgSmallList.Images.Add(ext, smallIcon);
                        imageLargeList.Images.Add(ext, largeIcon);
                    }
                }

                lvItem.ImageKey = ext;
            }
        }

        /// <summary>
        /// 设置当前显示路径
        /// </summary>
        private void SetUrlBar()
        {
            string path = this.curPath;
            //清除当前项后面的路径
            for (int i = tsUrlBar.Items.Count - 1; i > 1; i--)
            {
                ToolStripItem item = tsUrlBar.Items[i];
                if (item.Name == "tsbHome" || item.Name == "tsbDefaultArrow")
                {
                    continue;//不移除主页和第一个箭头
                }
                else
                {
                    tsUrlBar.Items.Remove(item);
                }
            }
            //设置新路径
            string[] items = path.Split(new string[] { dirSeparator }, StringSplitOptions.RemoveEmptyEntries);
            if (items != null && items.Length > 0)
            {
                string filePath = String.Empty;
                foreach (string fileName in items)
                {
                    ToolStripItem tsItem = new ToolStripButton();
                    tsItem.Text = fileName.Replace("&", "&&");
                    tsItem.ToolTipText = fileName;
                    filePath = String.Concat(filePath, dirSeparator, fileName); //用来保存路径
                    tsItem.Tag = filePath;
                    tsItem.Click += delegate(object sender, EventArgs e)
                    {
                        RefreshList(tsItem.Tag.ToString());
                    };
                    ToolStripItem itemArrow = new ToolStripLabel();
                    itemArrow.Image = tslDefaultArrow.Image;
                    tsUrlBar.Items.Add(tsItem);
                    tsUrlBar.Items.Add(itemArrow);

                    bool isSearch = (fileName.IndexOf('"') > -1);//包含双引号'"',表示搜索状态
                    SetOpenParentDirVisible(isSearch);
                }
            }
        }

        /// <summary>
        /// 是否显示"打开所在目录"项
        /// </summary>
        private void SetOpenParentDirVisible(bool isSearch)
        {
            itemMenuItem_OpenParentDir.Visible = isSearch;
        }

        /// <summary>
        /// 设置状态栏是否可见
        /// </summary>
        /// <param name="isVisible">是否可见</param>
        private void SetStatusBarVisible(bool isVisible)
        {
            pnlStatus.Visible = isVisible;
            tsStatusProgressBar.Value = 0;
            tsStatusMsg.Text = String.Empty;
        }

        /// <summary>
        /// 获取文件大小(带单位)
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private string GetSizeWithUnit(double size)
        {
            string result = size.ToString("N2");

            if (size >= Math.Pow(1024, 3) && size < Math.Pow(1024, 4))
            {
                result = (size / Math.Pow(1024, 3)).ToString("N2") + "G";
            }
            else if (size >= Math.Pow(1024, 2))
            {
                result = (size / Math.Pow(1024, 2)).ToString("N2") + "M";
            }
            else if (size >= 1024)
            {
                result = (size / 1024).ToString("N2") + "K";
            }

            result += "B";
            return result;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        private void UploadFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] fileNames = dlg.FileNames;
                if (fileNames != null && fileNames.Length > 0)
                {
                    string largeList = String.Empty;
                    foreach (string fileName in fileNames)
                    {
                        bool isTooLarge = CheckFileIsTooLarge(fileName);
                        if (isTooLarge)
                        {
                            largeList += fileName + "\r\n";
                        }
                    }
                    if (largeList != String.Empty)
                    {
                        MessageUtil.Warning("无法上传以下超过100M的文件:\r\n" + largeList);
                        return;
                    }
                    ThreadArgument arg = new ThreadArgument()
                    {
                        WorkType = WorkType.Upload,
                        Data = fileNames
                    };
                    panLoading.Visible = true;
                    SetStatusBarVisible(true);
                    bgWorker.RunWorkerAsync(arg);
                }
            }
        }

        /// <summary>
        /// 验证文件是否大于100M
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool CheckFileIsTooLarge(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);
            return (fi.Length / Math.Pow(1024, 2)) > 100;
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        private void NewDir()
        {
            string defaltDir = "新建文件夹";
            string modiDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ListViewItem item = new ListViewItem(new string[] { defaltDir, "-", modiDate });
            int number = (int)(DateTime.UtcNow - Jan1st1970).TotalSeconds;
            item.Tag = new FolderItem("新建文件夹", FileType.Dir, 0, number, this.curPath);

            string ext = "DIR";
            if (!imgSmallList.Images.ContainsKey(ext))
            {
                imgSmallList.Images.Add(ext, IconUtils.GetIconForFolder(false, false));
            }
            item.ImageKey = ext;
            lvList.Items.Add(item);
            item.Selected = true;
            RenameItem(true);
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="url">路径</param>
        private void RefreshList(string url)
        {
            RefreshList(url, true);
        }

        /// <summary>
        /// 刷新列表，指定是否加入到后退栈
        /// </summary>
        /// <param name="url">路径</param>
        /// <param name="pushToBack">否加入到后退栈</param>
        private void RefreshList(string url, bool pushToBack)
        {
            if (pushToBack)
            {
                PushToBackStack(this.curPath);
            }
            this.curPath = url;
            int start = url.IndexOf('"') + 1;
            if (start > 0)//包含双引号'"',表示搜索状态
            {
                int end = url.IndexOf('"', start + 1);
                string text = url.Substring(start, end - start);
                Search(text);
            }
            else
            {
                ShowFileList(url);
            }
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        private void OpenDir()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem item = lvList.SelectedItems[0];
                FolderItem fItem = item.Tag as FolderItem;
                if (fItem != null && fItem.FileType == FileType.Dir)
                {
                    string url = String.Concat(fItem.Url, dirSeparator, fItem.FileName);
                    RefreshList(url);
                }
            }
        }

        /// <summary>
        /// 打开所在目录
        /// </summary>
        private void OpenParentDir()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem item = lvList.SelectedItems[0];
                FolderItem fItem = item.Tag as FolderItem;
                if (fItem != null)
                {
                    RefreshList(fItem.Url);
                }
            }

        }

        /// <summary>
        /// 添加到后退栈
        /// </summary>
        /// <param name="url"></param>
        private void PushToBackStack(string url)
        {
            if (urlBackStack.Count == 0 || urlBackStack.Peek() != url)
            {
                urlBackStack.Push(url);
                if (tsbBack.Enabled == false)
                {
                    tsbBack.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 添加到前进栈
        /// </summary>
        /// <param name="url"></param>
        private void PushToForwardStack(string url)
        {
            if (urlForwardStack.Count == 0 || urlForwardStack.Peek() != url)
            {
                urlForwardStack.Push(url);
                if (tsbForward.Enabled == false)
                {
                    tsbForward.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 重命名当前选中的项
        /// </summary>
        private void RenameItem(bool isNewDir)
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem item = lvList.SelectedItems[0];
                string fileName = item.Text;
                TextBox textBox = new TextBox();
                if (lvList.View == View.Details)
                {
                    textBox.Location = new Point(item.Bounds.Left + 35, item.Bounds.Top);
                }
                else
                {
                    textBox.Location = new Point(item.Bounds.Left + 10, item.Bounds.Bottom - textBox.Height);
                }
                textBox.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        this.Focus();
                    }
                };
                textBox.KeyPress += Text_KeyPress;
                textBox.TextChanged += Text_TextChanged;
                textBox.LostFocus += delegate(object sender, EventArgs e)
                {
                    string newFileName = textBox.Text.Trim();
                    if (newFileName == String.Empty)
                    {
                        newFileName = fileName;
                    }
                    newFileName = GetValidFileName(newFileName, item);

                    item.Text = newFileName;
                    lvList.Controls.Remove(textBox);
                    if (isNewDir)
                    {
                        ThreadArgument arg = new ThreadArgument()
                        {
                            WorkType = WorkType.NewDir,
                            Data = item
                        };
                        panLoading.Visible = true;
                        bgWorker.RunWorkerAsync(arg);
                    }
                };
                textBox.Text = fileName;
                //计算文字宽度
                Graphics g = CreateGraphics();
                SizeF sf = g.MeasureString(fileName, item.Font);
                textBox.Width = (int)sf.Width + 10;
                lvList.Controls.Add(textBox);
                textBox.Focus();
                textBox.Select(0, fileName.Length - Path.GetExtension(fileName).Length);
            }
        }

        /// <summary>
        /// 获取可用文件名
        /// 如果存在则返回带数字的名称,如:新建文件夹(2)
        /// 如果不存在，则直接返回
        /// </summary>
        /// <param name="fileName">新文件名</param>
        /// <param name="curItem">当前正在编辑的项</param>
        /// <returns></returns>
        private string GetValidFileName(string fileName, ListViewItem curItem)
        {
            if (lvList.Items != null && lvList.Items.Count > 0)
            {
                foreach (ListViewItem item in lvList.Items)
                {
                    if (curItem == item) continue;
                    if (item.Text == fileName)
                    {
                        fileName = GetFileNameWithNumber(fileName);
                        return GetValidFileName(fileName, curItem);
                    }
                }
            }
            return fileName;
        }

        /// <summary>
        /// 获取带数字的文件名,如果原来就带数字，则数字+1
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string GetFileNameWithNumber(string fileName)
        {
            Match m = Regex.Match(fileName, @"(\d+)", RegexOptions.RightToLeft | RegexOptions.IgnoreCase);
            if (m.Success)
            {
                string text = m.Value;
                int index = m.Index;
                if (index == fileName.Length - m.Value.Length - 1)//只匹配最后
                {
                    int number = int.Parse(m.Value);
                    number++;
                    string newFileName = fileName.Remove(index) + number + ")";
                    return newFileName;
                }
            }
            return fileName + "(1)";
        }

        /// <summary>
        /// 删除选中项(多选)
        /// </summary>
        private void DeleteSelectedItems()
        {
            System.Collections.IList selectedList = null;
            if (lvList.View == View.Details)
            {
                selectedList = lvList.CheckedItems;
            }
            else
            {
                selectedList = lvList.SelectedItems;
            }
            if (selectedList != null && selectedList.Count > 0)
            {
                if (MessageUtil.Confirm("您确认要删除吗？删除后将无法恢复!") == DialogResult.OK)
                {
                    List<FolderItem> deleteList = new List<FolderItem>();
                    foreach (ListViewItem item in selectedList)
                    {
                        FolderItem fItem = item.Tag as FolderItem;
                        if (fItem != null)
                        {
                            deleteList.Add(fItem);
                        }
                    }
                    if (!bgWorker.IsBusy)
                    {
                        ThreadArgument arg = new ThreadArgument()
                        {
                            WorkType = WorkType.Delete,
                            Data = deleteList
                        };
                        panLoading.Visible = true;
                        SetStatusBarVisible(true);
                        bgWorker.RunWorkerAsync(arg);
                    }
                }
            }
            else
            {
                MessageUtil.Warning("请选择要删除的文件或目录");
            }
        }

        /// <summary>
        /// 删除文件或目录
        /// </summary>
        /// <param name="fItem"></param>
        private void DeleteItem(FolderItem fItem)
        {
            string fileType = fItem.FileType;
            string path = String.Concat(fItem.Url, dirSeparator, fItem.FileName);
            if (fileType == FileType.File)//删除文件
            {
                this.upYun.deleteFile(path);
            }
            else if (fileType == FileType.Dir)//删除目录
            {
                DeleteDir(path);
            }
        }

        /// <summary>
        /// 递归删除目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private void DeleteDir(string path)
        {
            List<FolderItem> itemList = this.upYun.readDir(path);
            if (itemList == null || itemList.Count == 0)
            {
                //删除空目录
                this.upYun.rmDir(path);
            }
            else
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    DeleteItem(itemList[i]);
                    //删除所有文件后，删除文件夹
                    if (i == itemList.Count - 1)
                    {
                        this.upYun.rmDir(path);
                    }
                }
            }

        }

        /// <summary>
        /// 下载文件或目录
        /// </summary>
        /// <param name="path">服务器路径</param>
        /// <param name="localPath">本地路径</param>
        /// <param name="fileType">F:目录，N:文件</param>
        private void DownloadItem(FolderItem item, string localPath)
        {
            string fileType = item.FileType;
            string path = String.Concat(item.Url, dirSeparator, item.FileName);
            if (fileType == FileType.File)//文件直接下载
            {
                byte[] data = this.upYun.readFile(path);
                File.WriteAllBytes(localPath, data);
            }
            else if (fileType == FileType.Dir)//目录递归下载
            {
                DownloadDir(path, localPath);
            }
        }

        /// <summary>
        /// 目录递归下载
        /// </summary>
        /// <param name="path">服务器路径</param>
        /// <param name="localPath">本地下载路径</param>
        private void DownloadDir(string path, string localPath)
        {
            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }
            List<FolderItem> itemList = this.upYun.readDir(path);
            if (itemList != null && itemList.Count > 0)
            {
                foreach (FolderItem item in itemList)
                {
                    string sublocalPath = String.Concat(localPath, dirSeparator, item.FileName);
                    DownloadItem(item, sublocalPath);
                }
            }
        }

        /// <summary>
        /// 下载选择项(多选)
        /// </summary>
        private void DownloadSelectedItems()
        {
            IList selectedList = null;
            if (lvList.View == View.Details)
            {
                selectedList = lvList.CheckedItems;
            }
            else
            {
                selectedList = lvList.SelectedItems;
            }
            if (selectedList != null && selectedList.Count > 0)
            {
                List<FolderItem> downLoadList = new List<FolderItem>();
                foreach (ListViewItem item in selectedList)
                {
                    FolderItem fItem = item.Tag as FolderItem;
                    if (fItem != null)
                    {
                        downLoadList.Add(fItem);
                    }
                }
                ThreadArgument arg = new ThreadArgument()
                {
                    WorkType = WorkType.Download,
                    Data = downLoadList
                };
                panLoading.Visible = true;
                SetStatusBarVisible(true);
                bgWorker.RunWorkerAsync(arg);
            }
        }

        /// <summary>
        /// 清除排序
        /// </summary>
        private void ClearSort()
        {
            foreach (ColumnHeader header in lvList.Columns)
            {
                header.Text = header.Text.TrimEnd(sortAsc).TrimEnd(sortDesc).TrimEnd();
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        private void Search()
        {
            string text = tsbSearchText.Text.Trim();
            if (text == String.Empty)
            {
                MessageUtil.Warning("请输入搜索内容！");
                tsbSearchText.Text = String.Empty;
                tsbSearchText.Focus();
                return;
            }
            Search(text.ToLower());
        }

        /// <summary>
        /// 搜索匹配指定的字符串
        /// </summary>
        private void Search(string text)
        {
            if (!bgWorker.IsBusy)
            {
                ThreadArgument arg = new ThreadArgument()
                {
                    WorkType = WorkType.Search,
                    Data = text
                };
                panLoading.Visible = true;
                bgWorker.RunWorkerAsync(arg);
            }
        }

        /// <summary>
        /// 搜索匹配指定的字符串
        /// </summary>
        /// <param name="text"></param>
        private List<FolderItem> SearchText(string text, string path)
        {
            List<FolderItem> resultList = new List<FolderItem>();
            List<FolderItem> itemList = GetDirFiles(path);
            if (itemList != null && itemList.Count > 0)
            {
                foreach (FolderItem item in itemList)
                {
                    string fileType = item.FileType;
                    string fileName = item.FileName;
                    if (fileName.ToLower().IndexOf(text) > -1)
                    {
                        resultList.Add(item);
                    }
                    if (fileType == FileType.Dir)//对目录递归搜索
                    {
                        string subpath = String.Concat(path, dirSeparator, item.FileName);
                        List<FolderItem> list = SearchText(text, subpath);
                        if (list != null && list.Count > 0)
                        {
                            resultList.AddRange(list);
                        }
                    }
                }
            }
            return resultList;
        }

        /// <summary>
        /// 移除无效字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string RemoveInvalidChars(string text)
        {
            foreach (char c in invalidChars)
            {
                if (text.IndexOf(c) > -1)
                {
                    text = text.Replace(c.ToString(), "");
                }
            }
            return text;
        }

        /// <summary>
        /// 切换列表显示模式
        /// </summary>
        private void SwichListView()
        {
            bool hasSelectedItems = false;
            if (this.lvList.View == View.Details)
            {
                this.lvList.View = View.LargeIcon;
                this.lvList.MultiSelect = true;
                if (this.lvList.CheckedItems != null && this.lvList.CheckedItems.Count > 0)
                {
                    hasSelectedItems = true;
                    //勾选-->选中
                    foreach (ListViewItem item in this.lvList.CheckedItems)
                    {
                        item.Selected = true;
                        item.Checked = false;
                    }
                }
                this.lvList.CheckBoxes = false;
                this.chkAll.Visible = false;
                tsbSwichIcon.Image = Filemanager_for_UPYUN.Properties.Resources.Thumbnails;
                tsbSwichIcon.Text = "切换到列表模式";
            }
            else
            {
                this.lvList.View = View.Details;
                this.lvList.CheckBoxes = true;
                //选中-->勾选
                if (this.lvList.SelectedItems != null && this.lvList.SelectedItems.Count > 0)
                {
                    hasSelectedItems = true;
                    foreach (ListViewItem item in this.lvList.SelectedItems)
                    {
                        item.Checked = true;
                        item.Selected = false;
                    }
                }
                this.chkAll.Visible = true;
                this.lvList.MultiSelect = false;
                tsbSwichIcon.Image = Filemanager_for_UPYUN.Properties.Resources.List;
                tsbSwichIcon.Text = "切换到缩略图模式";
            }
            SetButtonEnabled(hasSelectedItems);
        }

        #endregion

        #region Form Events

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            InitInvalidChars();
            RefreshList(this.curPath, false);
            lvList.ListViewItemSorter = new ListViewColumnSorter();
            this.lblLoading.Image = Filemanager_for_UPYUN.Properties.Resources.loading;
        }

        private void panLoading_Resize(object sender, EventArgs e)
        {
            int x = (panLoading.Width - lblLoading.Width) / 2;
            int y = (panLoading.Height - lblLoading.Height) / 2;
            lblLoading.Location = new Point(x, y);
        }
        #endregion

        #region Menu Events

        //新建文件夹
        private void MenuItem_NewDir_Click(object sender, System.EventArgs e)
        {
            NewDir();
        }

        //上传文件
        private void MenuItem_Upload_Click(object sender, EventArgs e)
        {
            UploadFile();
        }

        //刷新
        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshList(this.curPath);
        }

        //删除
        private void MenuItem_Delete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        //打开文件夹
        private void MenuItem_OpenDir_Click(object sender, EventArgs e)
        {
            OpenDir();
        }

        //下载
        private void MenuItem_Download_Click(object sender, EventArgs e)
        {
            DownloadSelectedItems();
        }

        //打开所在目录
        private void MenuItem_OpenParentDir_Click(object sender, EventArgs e)
        {
            OpenParentDir();
        }

        #endregion

        #region Button Events

        //显示主页按钮
        private void btnHome_Click(object sender, EventArgs e)
        {
            PushToBackStack(this.curPath);
            string url = "";
            RefreshList(url);
        }

        //删除按钮
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedItems();
        }

        //下载按钮
        private void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadSelectedItems();
        }

        //上传按钮
        private void btnUpload_Click(object sender, EventArgs e)
        {
            UploadFile();
        }

        //新建文件夹
        private void btnNewDir_Click(object sender, EventArgs e)
        {
            NewDir();
        }

        //前进
        private void tsbForward_Click(object sender, EventArgs e)
        {
            string url = urlForwardStack.Pop();
            if (urlForwardStack.Count == 0)
            {
                tsbForward.Enabled = false;
            }
            RefreshList(url);
        }

        //后退
        private void tsbBack_Click(object sender, EventArgs e)
        {
            PushToForwardStack(this.curPath);
            string url = urlBackStack.Pop();
            if (urlBackStack.Count == 0)
            {
                tsbBack.Enabled = false;
            }
            RefreshList(url, false);
        }

        //点击搜索
        private void tsbSearchButton_Click(object sender, EventArgs e)
        {
            Search();
        }

        //回车搜索
        private void tsbSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Search();
            }
        }

        //屏蔽无效字符
        private void Text_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 8 &&//允许退格
                invalidChars.Contains(ch))
            {
                e.Handled = true;
            }
        }

        //防止粘贴无效字符
        private void Text_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = null;
            if (sender is TextBox)
            {
                textBox = sender as TextBox;
            }
            else if (sender is ToolStripTextBox)
            {
                textBox = (sender as ToolStripTextBox).TextBox;
            }
            if (textBox == null) return;
            string text = RemoveInvalidChars(textBox.Text);
            textBox.Text = text;

        }

        //切换列表视图模式
        private void tsbSwichIcon_Click(object sender, EventArgs e)
        {
            SwichListView();
        }

        #endregion

        #region ListView Events
        //双击打开文件夹
        private void lvList_DoubleClick(object sender, EventArgs e)
        {
            OpenDir();
        }

        //选中项改变时，切换右键菜单
        private void lvList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                FolderItem fItem = e.Item.Tag as FolderItem;
                if (fItem != null)
                {
                    string fileType = fItem.FileType;
                    if (fileType == FileType.File)
                    {
                        SetContextMenu(MenuType.File);
                    }
                    else if (fileType == FileType.Dir)
                    {
                        SetContextMenu(MenuType.Dir);
                    }
                    else
                    {
                        SetContextMenu(MenuType.Default);
                    }
                }
            }
            else
            {
                SetContextMenu(MenuType.Default);
            }
            bool hasSelectedItems = false;
            if (lvList.View == View.Details)
            {
                hasSelectedItems = (lvList.CheckedItems.Count > 0);
            }
            else
            {
                hasSelectedItems = (lvList.SelectedItems.Count > 0);
            }
            SetButtonEnabled(hasSelectedItems);
        }

        //列头排序
        private void lvList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv.Items != null && lv.Items.Count > 1)
            {
                ListViewColumnSorter lvColSorter = lv.ListViewItemSorter as ListViewColumnSorter;
                int sortColumn = lvColSorter.SortColumn;
                if (e.Column == sortColumn)//当前为排序列
                {
                    if (lvColSorter.Order == SortOrder.Ascending)
                    {
                        lvColSorter.Order = SortOrder.Descending;
                    }
                    else
                    {
                        lvColSorter.Order = SortOrder.Ascending;
                    }
                }
                else
                {
                    //设置当前列为排序列
                    lvColSorter.SortColumn = e.Column;
                    lvColSorter.Order = SortOrder.Ascending;
                }

                ClearSort();
                lvList.Columns[e.Column].Text += ((lvColSorter.Order == SortOrder.Ascending) ? sortAsc : sortDesc);
                lv.Sort();

            }

        }

        //全选(反选)
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkAll.Checked;
            foreach (ListViewItem item in lvList.Items)
            {
                item.Checked = isChecked;
            }
        }

        //当列表中某一项取消选择时，取消"全选"，某一项选中后列表所有项都选中时，选中"全选"
        private void lvList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (chkAll.Checked && !e.Item.Checked)
            {
                chkAll.CheckedChanged -= chkAll_CheckedChanged;
                chkAll.Checked = false;
                chkAll.CheckedChanged += chkAll_CheckedChanged;
            }
            else if (e.Item.Checked && lvList.Items.Count == lvList.CheckedItems.Count)
            {
                chkAll.Checked = true;
            }
            bool hasCheckedItem = (lvList.CheckedItems.Count > 0);
            SetButtonEnabled(hasCheckedItem);
        }

        /// <summary>
        /// 设置下载和删除按钮是否可用
        /// </summary>
        /// <param name="isEnabled"></param>
        private void SetButtonEnabled(bool isEnabled)
        {
            tsbDownload.Enabled = isEnabled;
            tsbDelete.Enabled = isEnabled;
        }

        //取消双击时勾选的默认效果
        private void lvList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks > 1)
            {
                ListViewItem item = this.lvList.GetItemAt(e.X, e.Y);
                if (item == null)
                    return;
                item.Checked = !item.Checked;
            }
        }

        //回车打开文件夹
        private void lvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenDir();
            }

        }

        #endregion

        #region Thread Backgroundworker Events

        //线程工作区
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ThreadArgument arg = (ThreadArgument)e.Argument;
            if (arg != null)
            {
                ThreadResult result = new ThreadResult();
                result.ThreadArg = arg;
                result.IsSuccess = true;
                try
                {
                    switch (arg.WorkType)
                    {
                        case WorkType.Default:
                            string path = arg.Data.ToString();
                            result.DataList = GetDirFiles(path);
                            break;
                        case WorkType.Upload:
                            string[] fileNames = arg.Data as string[];
                            if (fileNames != null && fileNames.Length > 0)
                            {
                                int count = fileNames.Length;
                                for (int i = 0; i < count; i++)
                                {
                                    string fullName = fileNames[i];
                                    byte[] data = File.ReadAllBytes(fullName);
                                    this.upYun.setContentMD5(UpYun.md5_file(fullName));
                                    string fName = Path.GetFileName(fullName);
                                    string serverPath = String.Concat(this.curPath, dirSeparator, fName);

                                    StateInfo sInfo = new StateInfo();
                                    sInfo.WorkType = WorkType.Upload;
                                    sInfo.FileName = fName;
                                    int percent = 100 * (i + 1) / count;
                                    bgWorker.ReportProgress(percent, sInfo);//前置通知

                                    this.upYun.writeFile(serverPath, data, false);//上传

                                    sInfo.IsComplete = true;
                                    bgWorker.ReportProgress(percent, sInfo);//后置通知
                                }
                            }
                            break;
                        case WorkType.Download:
                            List<FolderItem> downLoadList = arg.Data as List<FolderItem>;
                            if (downLoadList != null && downLoadList.Count > 0)
                            {
                                int count = downLoadList.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    FolderItem fItem = downLoadList[i];
                                    string fileName = fItem.FileName;
                                    string localpath = String.Concat(this.downPath, dirSeparator, fileName);

                                    StateInfo sInfo = new StateInfo();
                                    sInfo.WorkType = WorkType.Download;
                                    sInfo.FileName = fileName;
                                    int percent = 100 * (i + 1) / count;
                                    bgWorker.ReportProgress(percent, sInfo);//前置通知

                                    DownloadItem(fItem, localpath);//下载

                                    sInfo.IsComplete = true;
                                    bgWorker.ReportProgress(percent, sInfo);//后置通知
                                }
                            }
                            break;
                        case WorkType.NewDir:
                            ListViewItem item = arg.Data as ListViewItem;
                            if (item != null)
                            {
                                string newDirPath = String.Concat(this.curPath, dirSeparator, item.Text);
                                result.IsSuccess = this.upYun.mkDir(newDirPath, false);
                            }
                            break;
                        case WorkType.Delete:
                            List<FolderItem> deleteList = arg.Data as List<FolderItem>;
                            if (deleteList != null && deleteList.Count > 0)
                            {
                                int count = deleteList.Count;
                                for (int i = 0; i < count; i++)
                                {
                                    FolderItem fItem = deleteList[i];

                                    StateInfo sInfo = new StateInfo();
                                    sInfo.WorkType = WorkType.Delete;
                                    sInfo.FileName = fItem.FileName;
                                    int percent = 100 * (i + 1) / count;
                                    bgWorker.ReportProgress(percent, sInfo);//前置通知

                                    DeleteItem(fItem);//删除

                                    sInfo.IsComplete = true;
                                    bgWorker.ReportProgress(percent, sInfo);//后置通知
                                }
                            }
                            break;
                        case WorkType.Search:
                            string searchText = arg.Data.ToString();
                            result.DataList = SearchText(searchText, String.Empty);
                            break;
                        default:
                            break;
                    }
                }
                catch
                {
                    result.IsSuccess = false;
                }
                e.Result = result;
            }
        }

        //线程工作进度通知
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            StateInfo info = e.UserState as StateInfo;
            if (info != null)
            {
                WorkType workType = info.WorkType;
                string infoType = String.Empty;
                if (workType == WorkType.Upload)
                {
                    infoType = "上传";
                }
                else if (workType == WorkType.Download)
                {
                    infoType = "下载";
                }
                else if (workType == WorkType.Delete)
                {
                    infoType = "删除";
                }
                if (info.IsComplete)
                {
                    infoType += "完成";
                }
                else
                {
                    infoType = String.Concat("正在", infoType, "...");
                }
                tsStatusMsg.Text = String.Format("{0}：{1}", info.FileName, infoType);
            }
            tsStatusProgressBar.Value = e.ProgressPercentage;
        }

        //线程处理完成
        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            panLoading.Visible = false;
            ThreadResult result = e.Result as ThreadResult;
            if (result != null)
            {
                #region  执行完后进行界面处理
                ThreadArgument arg = result.ThreadArg;
                if (arg != null)
                {
                    switch (arg.WorkType)
                    {
                        case WorkType.Default:
                            BindToListView(result.DataList);
                            break;
                        case WorkType.Upload:
                            RefreshBucketUsage();
                            RefreshList(this.curPath);
                            break;
                        case WorkType.Download:
                            MessageUtil.Show("下载完成！");
                            break;
                        case WorkType.NewDir:
                            ListViewItem item = arg.Data as ListViewItem;
                            if (item != null)
                            {
                                FolderItem fItem = item.Tag as FolderItem;
                                if (fItem != null)
                                {
                                    fItem.FileName = item.Text;
                                }
                            }
                            break;
                        case WorkType.Delete:
                            RefreshBucketUsage();
                            RefreshList(this.curPath);
                            break;
                        case WorkType.Search:
                            string tempText = String.Format("\"{0}\"的搜索结果", arg.Data.ToString());
                            this.curPath = String.Concat(dirSeparator, tempText);
                            BindToListView(result.DataList);
                            break;
                        default:
                            break;
                    }
                }
                #endregion
            }
            SetStatusBarVisible(false);
        }

        #endregion


    }
}
