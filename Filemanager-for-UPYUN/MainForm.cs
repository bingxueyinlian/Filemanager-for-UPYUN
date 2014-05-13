﻿using System;
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
        private const char sortAsc = '↑';// (char)0x25b2;;
        private const char sortDesc = '↓';//(char)0x25bc;;
        private UpYun upYun;
        private string curPath = "";//当前路径
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

        #region Private Mothod(s)

        /// <summary>
        /// 列举当前目录下的内容
        /// </summary>
        /// <param name="path"></param>
        private void ListFileInfo(string path)
        {
            lvList.Items.Clear();
            List<FolderItem> itemList = upYun.readDir(path);
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
                    if (item.filetype == "N")
                    {
                        ext = Path.GetExtension(item.filename);
                    }
                    else if (item.filetype == "F")
                    {
                        ext = "DIR";
                    }
                    //绑定到ImageList中
                    if (!imgList.Images.ContainsKey(ext))
                    {
                        if (item.filetype == "N")
                        {
                            Icon icon = IconUtils.GetIconForFileExtension(ext, false, false);
                            imgList.Images.Add(ext, icon);
                        }
                        else if (item.filetype == "F")
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
        private void FileUpload()
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
                        string path = this.curPath + Path.DirectorySeparatorChar + Path.GetFileName(fName);
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
            RenameItem();
        }

        /// <summary>
        /// 刷新列表
        /// </summary>
        private void RefreshList()
        {
            ListFileInfo(curPath);
        }

        /// <summary>
        /// 重命名当前选中的项
        /// </summary>
        private void RenameItem()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem item = lvList.SelectedItems[0];
                TextBox textBox = new TextBox();
                textBox.Location = new System.Drawing.Point(item.Bounds.Left + 35, item.Bounds.Top);
                textBox.LostFocus += delegate(object sender, EventArgs e)
                {
                    if (item.Text != textBox.Text)
                    {
                        item.Text = textBox.Text;
                    }
                    lvList.Controls.Remove(textBox);
                };
                string fileName = item.Text;
                textBox.Text = fileName;
                //计算文字宽度
                Graphics g = CreateGraphics();
                SizeF sf = g.MeasureString(fileName, item.Font);
                textBox.Width = (int)sf.Width + 10;
                textBox.Height = 16;
                lvList.Controls.Add(textBox);
                textBox.Focus();
                textBox.Select(0, fileName.Length - Path.GetExtension(fileName).Length);
            }
        }

        /// <summary>
        /// 删除当前选中的项
        /// </summary>
        private void DeleteItem()
        {
            if (lvList.SelectedItems != null && lvList.SelectedItems.Count > 0)
            {
                ListViewItem lvItem = lvList.SelectedItems[0];
                string path = this.curPath + Path.DirectorySeparatorChar + lvItem.Text;
                bool res = false;
                try
                {
                    res = this.upYun.deleteFile(path);
                    if (res)
                    {
                        lvList.Items.Remove(lvItem);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                if (!res)
                {
                    Debug.WriteLine(path + ":删除失败!");
                }
            }
        }
        #endregion

        #region Event(s)

        #region 普通事件

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            ListFileInfo(curPath);
            //lvList.ContextMenuStrip = fileContextMenu;
            lvList.ListViewItemSorter = new ListViewColumnSorter();

        }

        //选中项改变时，切换右键菜单
        private void lvList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                if (e.Item.Tag.ToString() == "N")
                {
                    lvList.ContextMenuStrip = fileContextMenu;
                }
                else if (e.Item.Tag.ToString() == "F")
                {
                    lvList.ContextMenuStrip = dirContextMenu;
                }
                else
                {
                    lvList.ContextMenuStrip = defaultContextMenu;
                }
            }
            else
            {
                lvList.ContextMenuStrip = defaultContextMenu;
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

        //新建文件夹
        private void MenuItem_NewDir_Click(object sender, System.EventArgs e)
        {
            NewDir();
        }

        //上传文件
        private void MenuItem_Upload_Click(object sender, EventArgs e)
        {
            FileUpload();
        }

        //刷新
        private void MenuItem_Refresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        //重命名
        private void MenuItem_Rename_Click(object sender, EventArgs e)
        {
            RenameItem();
        }

        //删除
        private void MenuItem_Delete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        #endregion

    }
}
