using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Filemanager_for_UPYUN
{
    public partial class MainForm : Form
    {
        #region Variable
        private const char sortAsc = '↑';// (char)0x25b2;
        private const char sortDesc = '↓';//(char)0x25bc;
        private readonly string dirSeparator = Path.DirectorySeparatorChar.ToString();//目录分隔符
        private UpYun upYun;
        private string curPath = "";//当前路径
        private string downPath = "D:";//下载路径
        private DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);//用于计算最后修改时间
        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(UpYun upY)
        {
            InitializeComponent();
            this.upYun = upY;
        }

        #endregion

        #region Private Mothods

        private void SetMenuType(MenuType mt)
        {
            switch (mt)
            {
                case MenuType.File:
                    lvList.ContextMenuStrip = fileContextMenu;
                    break;
                case MenuType.Dir:
                    lvList.ContextMenuStrip = dirContextMenu;
                    break;
                case MenuType.Default:
                    lvList.ContextMenuStrip = defaultContextMenu;
                    break;
                default:
                    lvList.ContextMenuStrip = defaultContextMenu;
                    break;
            }
        }

        /// <summary>
        /// 列举当前目录下的内容
        /// </summary>
        /// <param name="path"></param>
        private void ListFileInfo(string path)
        {
            lvList.Items.Clear();
            List<FolderItem> itemList = this.upYun.readDir(path);
            if (itemList != null && itemList.Count > 0)
            {
                //按类型和扩展名排序
                var tempList = itemList.OrderBy(p => p.filetype).ThenBy(p => Path.GetExtension(p.filename));
                foreach (var item in tempList)
                {
                    string modiDate = new DateTime(TimeSpan.FromSeconds(item.number).Add(new TimeSpan(Jan1st1970.Ticks)).Ticks).AddHours(8).ToString("yyyy-MM-dd HH:mm");
                    ListViewItem lvItem = new ListViewItem(new string[] { 
                        item.filename,
                        //GetSizeWithUnit(item.size),
                        item.size.ToString("N0"),
                        modiDate
                    });
                    lvItem.Tag = item.filetype;//用Tag来保存文件类型，便于切换右键菜单
                    lvList.Items.Add(lvItem);
                    //获取默认打开图片
                    string ext = "";
                    if (item.filetype == FileType.File)
                    {
                        ext = Path.GetExtension(item.filename);
                    }
                    else if (item.filetype == FileType.Dir)
                    {
                        ext = "DIR";
                    }
                    //绑定到ImageList中
                    if (!imgList.Images.ContainsKey(ext))
                    {
                        if (item.filetype == FileType.File)
                        {
                            Icon icon = IconUtils.GetIconForFileExtension(ext, false, false);
                            imgList.Images.Add(ext, icon);
                        }
                        else if (item.filetype == FileType.Dir)
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
        /// 获取文件大小(带单位)
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private string GetSizeWithUnit(int size)
        {
            string result = size.ToString();

            if (size >= Math.Pow(1024, 3) && size < Math.Pow(1024, 4))
            {
                result = (size / Math.Pow(1024, 3)) + "G";
            }
            else if (size >= Math.Pow(1024, 2))
            {
                result = (size / Math.Pow(1024, 2)) + "M";
            }
            else if (size >= 1024)
            {
                result = (size / 1024) + "K";
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
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] fileNames = dlg.FileNames;
                foreach (string fName in fileNames)
                {
                    byte[] data = File.ReadAllBytes(fName);
                    this.upYun.setContentMD5(UpYun.md5_file(fName));
                    bool res = false;
                    try
                    {
                        string path = String.Concat(this.curPath, dirSeparator, Path.GetFileName(fName));
                        res = this.upYun.writeFile(path, data, false);
                        if (res)
                        {
                            RefreshList();
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                    if (!res)
                    {
                        Debug.WriteLine(fName + ":上传失败!");
                    }
                }
            }
        }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        private void NewDir()
        {
            string defaltDir = "新建文件夹";
            string modiDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            ListViewItem lvItem = new ListViewItem(new string[] { defaltDir, "-", modiDate });
            lvItem.Tag = "F";
            string ext = "DIR";
            if (!imgList.Images.ContainsKey(ext))
            {
                imgList.Images.Add(ext, IconUtils.GetIconForFolder(false, false));
            }
            lvItem.ImageKey = ext;
            lvList.Items.Add(lvItem);
            lvItem.Selected = true;
            RenameItem(true);
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        private void RefreshList()
        {
            ListFileInfo(this.curPath);
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        private void OpenDir()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem item = lvList.SelectedItems[0];
                this.curPath = String.Concat(this.curPath, dirSeparator, item.Text);
                RefreshList();
                SetMenuType(MenuType.Default);
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
                TextBox textBox = new TextBox();
                textBox.Location = new System.Drawing.Point(item.Bounds.Left + 35, item.Bounds.Top);
                textBox.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        this.Focus();
                    }
                };
                textBox.LostFocus += delegate(object sender, EventArgs e)
                {
                    if (item.Text != textBox.Text)
                    {
                        item.Text = textBox.Text;
                    }
                    lvList.Controls.Remove(textBox);
                    if (isNewDir)
                    {
                        string path = String.Concat(this.curPath, dirSeparator, item.Text);
                        bool res = this.upYun.mkDir(path, false);
                        if (!res)
                        {
                            Debug.WriteLine(path + ":新建失败!");
                        }
                    }
                };
                string fileName = item.Text;
                textBox.Text = fileName;
                //计算文字宽度
                Graphics g = CreateGraphics();
                SizeF sf = g.MeasureString(fileName, item.Font);
                textBox.Width = (int)sf.Width + 10;
                //textBox.Height = 16;
                lvList.Controls.Add(textBox);
                textBox.Focus();
                textBox.Select(0, fileName.Length - Path.GetExtension(fileName).Length);
            }
        }

        /// <summary>
        /// 删除当前选中的项
        /// </summary>
        private void DeleteSelectedItem()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem lvItem = lvList.SelectedItems[0];
                string path = String.Concat(this.curPath, dirSeparator, lvItem.Text);
                DeleteItem(path, lvItem.Tag.ToString());
                lvList.Items.Remove(lvItem);
            }
        }

        /// <summary>
        /// 删除选中项(多选)
        /// </summary>
        private void DeleteCheckedItems()
        {
            if (lvList.CheckedItems != null && lvList.CheckedItems.Count > 0)
            {
                foreach (ListViewItem lvItem in lvList.CheckedItems)
                {
                    string path = String.Concat(this.curPath, dirSeparator, lvItem.Text);
                    string fileType = lvItem.Tag.ToString();
                    DeleteItem(path, fileType);
                }
                RefreshList();
            }
        }

        /// <summary>
        /// 删除文件或目录
        /// </summary>
        /// <param name="path">服务器路径</param>
        /// <param name="fileType">F:目录，N:文件</param>
        private void DeleteItem(string path, string fileType)
        {
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
                    FolderItem item = itemList[i];
                    string subpath = String.Concat(path, dirSeparator, item.filename);
                    DeleteItem(subpath, item.filetype);
                    //删除所有文件后，删除文件夹
                    if (i == itemList.Count - 1)
                    {
                        this.upYun.rmDir(path);
                    }
                }
            }

        }

        /// <summary>
        /// 下载当前选中项
        /// </summary>
        private void DownloadSelectedItem()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem lvItem = lvList.SelectedItems[0];
                string path = String.Concat(this.curPath, dirSeparator, lvItem.Text);
                string fileType = lvItem.Tag.ToString();
                DownloadItem(path, fileType);
            }
        }
        /// <summary>
        /// 下载文件或目录
        /// </summary>
        /// <param name="path">服务器路径</param>
        /// <param name="fileType">F:目录，N:文件</param>
        private void DownloadItem(string path, string fileType)
        {
            string localPath = String.Concat(this.downPath, path);
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
                    string subPath = String.Concat(path, dirSeparator, item.filename);
                    DownloadItem(subPath, item.filetype);
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
                foreach (ListViewItem lvItem in lvList.CheckedItems)
                {
                    string path = String.Concat(this.curPath, dirSeparator, lvItem.Text);
                    string fileType = lvItem.Tag.ToString();
                    DownloadItem(path, fileType);
                }
            }
        }
        #endregion

        #region Form Events

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            RefreshList();
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
        private void MenuItem_Refresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        //重命名
        private void MenuItem_Rename_Click(object sender, EventArgs e)
        {
            RenameItem(false);
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

        #endregion

        #region Button Events

        //显示主页按钮
        private void btnHome_Click(object sender, EventArgs e)
        {
            this.curPath = "";
            RefreshList();
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

        #endregion

        #region ListView Events
        //双击打开文件夹
        private void lvList_DoubleClick(object sender, EventArgs e)
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem lvItem = lvList.SelectedItems[0];
                if (lvItem.Tag.ToString() == "F")
                {
                    OpenDir();
                }
            }
        }


        //选中项改变时，切换右键菜单
        private void lvList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                string fileType = e.Item.Tag.ToString();
                if (fileType == FileType.File)
                {
                    SetMenuType(MenuType.File);
                }
                else if (fileType == FileType.Dir)
                {
                    SetMenuType(MenuType.Dir);
                }
                else
                {
                    SetMenuType(MenuType.Default);
                }
            }
            else
            {
                SetMenuType(MenuType.Default);
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

                //清除排序标记
                foreach (ColumnHeader header in lvList.Columns)
                {
                    header.Text = header.Text.TrimEnd(sortAsc).TrimEnd(sortDesc).TrimEnd();
                }
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
        }

        //F2重命名
        private void lvList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                //RenameItem();
            }
        }

        #endregion

    }
}
