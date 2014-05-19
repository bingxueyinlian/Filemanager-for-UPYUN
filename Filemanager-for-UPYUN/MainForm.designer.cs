namespace Filemanager_for_UPYUN
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lvList = new System.Windows.Forms.ListView();
            this.colFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colModiDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.defaultContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.defaultMenuItem_Upload = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultMenuItem_NewDir = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultMenuItem_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.toolStripTop = new System.Windows.Forms.ToolStrip();
            this.tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDownload = new System.Windows.Forms.ToolStripButton();
            this.tsbNewDir = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.fileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileMenuItem_Download = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuItem_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.dirContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dirMenuItem_OpenDir = new System.Windows.Forms.ToolStripMenuItem();
            this.dirMenuItem_Download = new System.Windows.Forms.ToolStripMenuItem();
            this.dirMenuItem_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.dirMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tsUrlBar = new System.Windows.Forms.ToolStrip();
            this.tsbHome = new System.Windows.Forms.ToolStripButton();
            this.tsbDefaultArrow = new System.Windows.Forms.ToolStripLabel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbSearch = new System.Windows.Forms.ToolStripTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBucketUsage = new System.Windows.Forms.Label();
            this.defaultContextMenu.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            this.fileContextMenu.SuspendLayout();
            this.dirContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tsUrlBar.SuspendLayout();
            this.panel6.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvList
            // 
            this.lvList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvList.CheckBoxes = true;
            this.lvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colSize,
            this.colModiDate});
            this.lvList.ContextMenuStrip = this.defaultContextMenu;
            this.lvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvList.FullRowSelect = true;
            this.lvList.Location = new System.Drawing.Point(0, 0);
            this.lvList.MultiSelect = false;
            this.lvList.Name = "lvList";
            this.lvList.Size = new System.Drawing.Size(586, 401);
            this.lvList.SmallImageList = this.imgList;
            this.lvList.TabIndex = 0;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            this.lvList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvList_ColumnClick);
            this.lvList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvList_ItemChecked);
            this.lvList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvList_ItemSelectionChanged);
            this.lvList.DoubleClick += new System.EventHandler(this.lvList_DoubleClick);
            this.lvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvList_KeyDown);
            this.lvList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvList_MouseDown);
            // 
            // colFileName
            // 
            this.colFileName.Text = "   文件名";
            this.colFileName.Width = 200;
            // 
            // colSize
            // 
            this.colSize.Text = "文件大小";
            this.colSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSize.Width = 150;
            // 
            // colModiDate
            // 
            this.colModiDate.Text = "修改时间";
            this.colModiDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.colModiDate.Width = 180;
            // 
            // defaultContextMenu
            // 
            this.defaultContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultMenuItem_Upload,
            this.defaultMenuItem_NewDir,
            this.defaultMenuItem_Refresh});
            this.defaultContextMenu.Name = "contextMenu";
            this.defaultContextMenu.Size = new System.Drawing.Size(137, 70);
            // 
            // defaultMenuItem_Upload
            // 
            this.defaultMenuItem_Upload.Image = ((System.Drawing.Image)(resources.GetObject("defaultMenuItem_Upload.Image")));
            this.defaultMenuItem_Upload.Name = "defaultMenuItem_Upload";
            this.defaultMenuItem_Upload.Size = new System.Drawing.Size(136, 22);
            this.defaultMenuItem_Upload.Text = "上传";
            this.defaultMenuItem_Upload.Click += new System.EventHandler(this.MenuItem_Upload_Click);
            // 
            // defaultMenuItem_NewDir
            // 
            this.defaultMenuItem_NewDir.Image = ((System.Drawing.Image)(resources.GetObject("defaultMenuItem_NewDir.Image")));
            this.defaultMenuItem_NewDir.Name = "defaultMenuItem_NewDir";
            this.defaultMenuItem_NewDir.Size = new System.Drawing.Size(136, 22);
            this.defaultMenuItem_NewDir.Text = "新建文件夹";
            this.defaultMenuItem_NewDir.Click += new System.EventHandler(this.MenuItem_NewDir_Click);
            // 
            // defaultMenuItem_Refresh
            // 
            this.defaultMenuItem_Refresh.Image = ((System.Drawing.Image)(resources.GetObject("defaultMenuItem_Refresh.Image")));
            this.defaultMenuItem_Refresh.Name = "defaultMenuItem_Refresh";
            this.defaultMenuItem_Refresh.Size = new System.Drawing.Size(136, 22);
            this.defaultMenuItem_Refresh.Text = "刷新";
            this.defaultMenuItem_Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imgList.ImageSize = new System.Drawing.Size(21, 21);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.BackColor = System.Drawing.Color.Transparent;
            this.chkAll.Location = new System.Drawing.Point(6, 7);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(15, 14);
            this.chkAll.TabIndex = 1;
            this.chkAll.UseVisualStyleBackColor = false;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // toolStripTop
            // 
            this.toolStripTop.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUpload,
            this.tsbDownload,
            this.tsbNewDir,
            this.tsbDelete});
            this.toolStripTop.Location = new System.Drawing.Point(0, 1);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.toolStripTop.Size = new System.Drawing.Size(257, 25);
            this.toolStripTop.TabIndex = 1;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // tsbUpload
            // 
            this.tsbUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsbUpload.Image")));
            this.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpload.Name = "tsbUpload";
            this.tsbUpload.Size = new System.Drawing.Size(52, 22);
            this.tsbUpload.Text = "上传";
            this.tsbUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // tsbDownload
            // 
            this.tsbDownload.Image = ((System.Drawing.Image)(resources.GetObject("tsbDownload.Image")));
            this.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownload.Name = "tsbDownload";
            this.tsbDownload.Size = new System.Drawing.Size(52, 22);
            this.tsbDownload.Text = "下载";
            this.tsbDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // tsbNewDir
            // 
            this.tsbNewDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbNewDir.Image")));
            this.tsbNewDir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewDir.Name = "tsbNewDir";
            this.tsbNewDir.Size = new System.Drawing.Size(88, 22);
            this.tsbNewDir.Text = "新建文件夹";
            this.tsbNewDir.Click += new System.EventHandler(this.btnNewDir_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelete.Image")));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(52, 22);
            this.tsbDelete.Text = "删除";
            this.tsbDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // fileContextMenu
            // 
            this.fileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem_Download,
            this.fileMenuItem_Rename,
            this.fileMenuItem_Delete});
            this.fileContextMenu.Name = "fileContextMenu";
            this.fileContextMenu.Size = new System.Drawing.Size(113, 70);
            // 
            // fileMenuItem_Download
            // 
            this.fileMenuItem_Download.Image = ((System.Drawing.Image)(resources.GetObject("fileMenuItem_Download.Image")));
            this.fileMenuItem_Download.Name = "fileMenuItem_Download";
            this.fileMenuItem_Download.Size = new System.Drawing.Size(112, 22);
            this.fileMenuItem_Download.Text = "下载";
            this.fileMenuItem_Download.Click += new System.EventHandler(this.MenuItem_Download_Click);
            // 
            // fileMenuItem_Rename
            // 
            this.fileMenuItem_Rename.Image = ((System.Drawing.Image)(resources.GetObject("fileMenuItem_Rename.Image")));
            this.fileMenuItem_Rename.Name = "fileMenuItem_Rename";
            this.fileMenuItem_Rename.Size = new System.Drawing.Size(112, 22);
            this.fileMenuItem_Rename.Text = "重命名";
            this.fileMenuItem_Rename.Click += new System.EventHandler(this.MenuItem_Rename_Click);
            // 
            // fileMenuItem_Delete
            // 
            this.fileMenuItem_Delete.Image = ((System.Drawing.Image)(resources.GetObject("fileMenuItem_Delete.Image")));
            this.fileMenuItem_Delete.Name = "fileMenuItem_Delete";
            this.fileMenuItem_Delete.Size = new System.Drawing.Size(112, 22);
            this.fileMenuItem_Delete.Text = "删除";
            this.fileMenuItem_Delete.Click += new System.EventHandler(this.MenuItem_Delete_Click);
            // 
            // dirContextMenu
            // 
            this.dirContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dirMenuItem_OpenDir,
            this.dirMenuItem_Download,
            this.dirMenuItem_Rename,
            this.dirMenuItem_Delete});
            this.dirContextMenu.Name = "dirContextMenu";
            this.dirContextMenu.Size = new System.Drawing.Size(113, 92);
            // 
            // dirMenuItem_OpenDir
            // 
            this.dirMenuItem_OpenDir.Image = ((System.Drawing.Image)(resources.GetObject("dirMenuItem_OpenDir.Image")));
            this.dirMenuItem_OpenDir.Name = "dirMenuItem_OpenDir";
            this.dirMenuItem_OpenDir.Size = new System.Drawing.Size(112, 22);
            this.dirMenuItem_OpenDir.Text = "打开";
            this.dirMenuItem_OpenDir.Click += new System.EventHandler(this.MenuItem_OpenDir_Click);
            // 
            // dirMenuItem_Download
            // 
            this.dirMenuItem_Download.Image = ((System.Drawing.Image)(resources.GetObject("dirMenuItem_Download.Image")));
            this.dirMenuItem_Download.Name = "dirMenuItem_Download";
            this.dirMenuItem_Download.Size = new System.Drawing.Size(112, 22);
            this.dirMenuItem_Download.Text = "下载";
            this.dirMenuItem_Download.Click += new System.EventHandler(this.MenuItem_Download_Click);
            // 
            // dirMenuItem_Rename
            // 
            this.dirMenuItem_Rename.Image = ((System.Drawing.Image)(resources.GetObject("dirMenuItem_Rename.Image")));
            this.dirMenuItem_Rename.Name = "dirMenuItem_Rename";
            this.dirMenuItem_Rename.Size = new System.Drawing.Size(112, 22);
            this.dirMenuItem_Rename.Text = "重命名";
            this.dirMenuItem_Rename.Click += new System.EventHandler(this.MenuItem_Rename_Click);
            // 
            // dirMenuItem_Delete
            // 
            this.dirMenuItem_Delete.Image = ((System.Drawing.Image)(resources.GetObject("dirMenuItem_Delete.Image")));
            this.dirMenuItem_Delete.Name = "dirMenuItem_Delete";
            this.dirMenuItem_Delete.Size = new System.Drawing.Size(112, 22);
            this.dirMenuItem_Delete.Text = "删除";
            this.dirMenuItem_Delete.Click += new System.EventHandler(this.MenuItem_Delete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 60);
            this.panel1.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(586, 30);
            this.panel4.TabIndex = 5;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tsUrlBar);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(400, 30);
            this.panel5.TabIndex = 5;
            // 
            // tsUrlBar
            // 
            this.tsUrlBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsUrlBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsUrlBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHome,
            this.tsbDefaultArrow});
            this.tsUrlBar.Location = new System.Drawing.Point(0, 0);
            this.tsUrlBar.Name = "tsUrlBar";
            this.tsUrlBar.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.tsUrlBar.Size = new System.Drawing.Size(398, 28);
            this.tsUrlBar.TabIndex = 4;
            this.tsUrlBar.Text = "toolStrip1";
            // 
            // tsbHome
            // 
            this.tsbHome.Image = ((System.Drawing.Image)(resources.GetObject("tsbHome.Image")));
            this.tsbHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHome.Name = "tsbHome";
            this.tsbHome.Size = new System.Drawing.Size(52, 25);
            this.tsbHome.Text = "主页";
            this.tsbHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // tsbDefaultArrow
            // 
            this.tsbDefaultArrow.Image = ((System.Drawing.Image)(resources.GetObject("tsbDefaultArrow.Image")));
            this.tsbDefaultArrow.Name = "tsbDefaultArrow";
            this.tsbDefaultArrow.Size = new System.Drawing.Size(16, 25);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.toolStrip2);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(400, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(186, 30);
            this.panel6.TabIndex = 6;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbSearch});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(186, 30);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 27);
            this.tsbRefresh.Text = "刷新";
            this.tsbRefresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // tsbSearch
            // 
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.Size = new System.Drawing.Size(150, 30);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(586, 30);
            this.panel3.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkAll);
            this.panel2.Controls.Add(this.lvList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(586, 401);
            this.panel2.TabIndex = 5;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.lblBucketUsage);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(406, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(180, 30);
            this.panel7.TabIndex = 2;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.toolStripTop);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(406, 30);
            this.panel8.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "已用空间：";
            // 
            // lblBucketUsage
            // 
            this.lblBucketUsage.AutoSize = true;
            this.lblBucketUsage.Location = new System.Drawing.Point(77, 9);
            this.lblBucketUsage.Name = "lblBucketUsage";
            this.lblBucketUsage.Size = new System.Drawing.Size(0, 12);
            this.lblBucketUsage.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 461);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Up云文件管理";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.defaultContextMenu.ResumeLayout(false);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.fileContextMenu.ResumeLayout(false);
            this.dirContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tsUrlBar.ResumeLayout(false);
            this.tsUrlBar.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvList;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colModiDate;
        private System.Windows.Forms.ContextMenuStrip defaultContextMenu;
        private System.Windows.Forms.ToolStripMenuItem defaultMenuItem_Upload;
        private System.Windows.Forms.ToolStripMenuItem defaultMenuItem_NewDir;
        private System.Windows.Forms.ToolStripMenuItem defaultMenuItem_Refresh;
        private System.Windows.Forms.ContextMenuStrip fileContextMenu;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem_Rename;
        private System.Windows.Forms.ContextMenuStrip dirContextMenu;
        private System.Windows.Forms.ToolStripMenuItem dirMenuItem_Rename;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem_Delete;
        private System.Windows.Forms.ToolStripMenuItem dirMenuItem_Delete;
        private System.Windows.Forms.ToolStripMenuItem dirMenuItem_OpenDir;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem_Download;
        private System.Windows.Forms.ToolStripMenuItem dirMenuItem_Download;
        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStripButton tsbUpload;
        private System.Windows.Forms.ToolStripButton tsbDownload;
        private System.Windows.Forms.ToolStripButton tsbNewDir;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStrip tsUrlBar;
        private System.Windows.Forms.ToolStripButton tsbHome;
        private System.Windows.Forms.ToolStripLabel tsbDefaultArrow;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripTextBox tsbSearch;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblBucketUsage;
        private System.Windows.Forms.Label label1;
    }
}