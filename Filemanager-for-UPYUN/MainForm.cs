using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        string downPath = "D:";//下载路径
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
            List<FolderItem> itemList = GetFiles(path);
            BindToListView(itemList);
        }

        /// <summary>
        /// 显示前进行清理操作
        /// </summary>
        private void ClearListViewBeforeShow()
        {
            lvList.Items.Clear();
            SetUrlBar();
            chkAll.Checked = false;
            SetContextMenu(MenuType.Default);
        }

        /// <summary>
        /// 获取指定目录下的所有文件和目录
        /// </summary>
        /// <param name="path">目录名</param>
        /// <returns></returns>
        private List<FolderItem> GetFiles(string path)
        {
            try
            {
                return this.upYun.readDir(path);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 绑定到ListView
        /// </summary>
        /// <param name="itemList"></param>
        private void BindToListView(List<FolderItem> itemList)
        {
            ClearListViewBeforeShow();
            if (itemList != null && itemList.Count > 0)
            {
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
                    if (!imgList.Images.ContainsKey(ext))
                    {
                        if (fileType == FileType.File)
                        {
                            Icon icon = IconUtils.GetIconForFileExtension(ext, false, false);
                            imgList.Images.Add(ext, icon);
                        }
                        else if (fileType == FileType.Dir)
                        {
                            Icon icon = IconUtils.GetIconForFolder(false, false);
                            imgList.Images.Add(ext, icon);
                        }
                    }

                    lvItem.ImageKey = ext;
                }
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
                foreach (string fileName in fileNames)
                {
                    byte[] data = File.ReadAllBytes(fileName);
                    this.upYun.setContentMD5(UpYun.md5_file(fileName));
                    string path = String.Concat(this.curPath, dirSeparator, Path.GetFileName(fileName));
                    this.upYun.writeFile(path, data, false);
                }
                RefreshList(this.curPath);
            }
        }

        /// <summary>
        /// 验证文件是否大于100M
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool CheckFileIsTooLarge(string fileName)
        {
            return true;
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
            if (!imgList.Images.ContainsKey(ext))
            {
                imgList.Images.Add(ext, IconUtils.GetIconForFolder(false, false));
            }
            item.ImageKey = ext;
            lvList.Items.Add(item);
            item.Selected = true;
            RenameItem(true);
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        /// <param name="url"></param>
        private void RefreshList(string url)
        {
            if (this.curPath != url)
            {
                PushToBackStack(this.curPath);
                this.curPath = url;
            }
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
                textBox.Location = new System.Drawing.Point(item.Bounds.Left + 35, item.Bounds.Top);
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
                        string path = String.Concat(this.curPath, dirSeparator, newFileName);
                        bool res = this.upYun.mkDir(path, false);
                        if (!res)
                        {
                            MessageUtil.Show("创建目录失败!");
                        }
                        else
                        {
                            FolderItem fItem = item.Tag as FolderItem;
                            if (fItem != null)
                            {
                                fItem.FileName = newFileName;
                            }
                            //RefreshList(this.curPath);
                        }
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
        /// 删除当前选中的项
        /// </summary>
        private void DeleteSelectedItem()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                if (MessageUtil.Confirm("您确认要删除吗？删除后将无法恢复!") == DialogResult.OK)
                {
                    ListViewItem item = lvList.SelectedItems[0];
                    FolderItem fItem = item.Tag as FolderItem;
                    if (fItem != null)
                    {
                        DeleteItem(fItem);
                        RefreshList(this.curPath);
                    }
                }
            }
            else
            {
                MessageUtil.Show("请选择要删除的文件或目录");
            }
        }

        /// <summary>
        /// 删除选中项(多选)
        /// </summary>
        private void DeleteCheckedItems()
        {
            if (lvList.CheckedItems != null && lvList.CheckedItems.Count > 0)
            {
                if (MessageUtil.Confirm("您确认要删除吗？删除后将无法恢复!") == DialogResult.OK)
                {
                    foreach (ListViewItem item in lvList.CheckedItems)
                    {
                        FolderItem fItem = item.Tag as FolderItem;
                        if (fItem != null)
                        {
                            DeleteItem(fItem);
                        }
                    }
                    RefreshList(this.curPath);
                }
            }
            else
            {
                MessageUtil.Show("请勾选要删除的文件或目录");
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
        /// 下载当前选中项[右键]
        /// </summary>
        private void DownloadSelectedItem()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem item = lvList.SelectedItems[0];
                FolderItem fItem = item.Tag as FolderItem;
                if (fItem != null)
                {
                    string localPath = String.Concat(this.downPath, dirSeparator, fItem.FileName);
                    DownloadItem(fItem, localPath);
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
        /// 下载选中项(多选)
        /// </summary>
        private void DownloadCheckedItems()
        {
            if (lvList.CheckedItems != null && lvList.CheckedItems.Count > 0)
            {
                foreach (ListViewItem item in lvList.CheckedItems)
                {
                    FolderItem fItem = item.Tag as FolderItem;
                    if (fItem != null)
                    {
                        string localpath = String.Concat(this.downPath, dirSeparator, fItem.FileName);
                        DownloadItem(fItem, localpath);
                    }
                }
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
                MessageUtil.Show("请输入搜索内容!");
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
            string path = "";
            List<FolderItem> itemList = SearchText(text, path);
            string tempText = String.Format("\"{0}\"的搜索结果", text);
            this.curPath = String.Concat(path, dirSeparator, tempText);
            BindToListView(itemList);
        }

        /// <summary>
        /// 搜索匹配指定的字符串
        /// </summary>
        /// <param name="text"></param>
        private List<FolderItem> SearchText(string text, string path)
        {
            List<FolderItem> resultList = new List<FolderItem>();
            List<FolderItem> itemList = GetFiles(path);
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

        #endregion

        #region Form Events

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            InitInvalidChars();
            RefreshList(this.curPath);
            lvList.ListViewItemSorter = new ListViewColumnSorter();
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
            DeleteSelectedItem();
        }

        //打开文件夹
        private void MenuItem_OpenDir_Click(object sender, EventArgs e)
        {
            OpenDir();
        }

        //下载
        private void MenuItem_Download_Click(object sender, EventArgs e)
        {
            DownloadSelectedItem();
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
            DeleteCheckedItems();
        }

        //下载按钮
        private void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadCheckedItems();
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
            this.curPath = url;
            RefreshList(url);
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

        //屏蔽非法字符
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
            tsbDownload.Enabled = hasCheckedItem;
            tsbDelete.Enabled = hasCheckedItem;
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
    }
}
