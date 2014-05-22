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
            this.colFileName = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colModiDate = new System.Windows.Forms.ColumnHeader();
            this.defaultContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.defaultMenuItem_Upload = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultMenuItem_NewDir = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultMenuItem_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.tsOperationBar = new System.Windows.Forms.ToolStrip();
            this.tsbUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDownload = new System.Windows.Forms.ToolStripButton();
            this.tsbNewDir = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.itemContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemMenuItem_OpenDir = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMenuItem_Download = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.itemMenuItem_OpenParentDir = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.panel11 = new System.Windows.Forms.Panel();
            this.tsUrlBar = new System.Windows.Forms.ToolStrip();
            this.tsbHome = new System.Windows.Forms.ToolStripButton();
            this.tslDefaultArrow = new System.Windows.Forms.ToolStripLabel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.tsSearchBar = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbSearchText = new System.Windows.Forms.ToolStripTextBox();
            this.tsbSearchButton = new System.Windows.Forms.ToolStripButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tsHistoryBar = new System.Windows.Forms.ToolStrip();
            this.tsbBack = new System.Windows.Forms.ToolStripButton();
            this.tsbForward = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.tsBucketUsageBar = new System.Windows.Forms.ToolStrip();
            this.tslBucketUsageDesc = new System.Windows.Forms.ToolStripLabel();
            this.tslBucketUsage = new System.Windows.Forms.ToolStripLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.defaultContextMenu.SuspendLayout();
            this.tsOperationBar.SuspendLayout();
            this.itemContextMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.tsUrlBar.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tsSearchBar.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tsHistoryBar.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.tsBucketUsageBar.SuspendLayout();
            this.panel2.SuspendLayout();
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
            this.lvList.Size = new System.Drawing.Size(584, 352);
            this.lvList.SmallImageList = this.imgList;
            this.lvList.TabIndex = 0;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            this.lvList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvList_ItemChecked);
            this.lvList.DoubleClick += new System.EventHandler(this.lvList_DoubleClick);
            this.lvList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvList_ColumnClick);
            this.lvList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvList_MouseDown);
            this.lvList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvList_ItemSelectionChanged);
            this.lvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvList_KeyDown);
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
            this.colModiDate.Width = 150;
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
            this.defaultMenuItem_Upload.Image = global::Filemanager_for_UPYUN.Properties.Resources.Upload;
            this.defaultMenuItem_Upload.Name = "defaultMenuItem_Upload";
            this.defaultMenuItem_Upload.Size = new System.Drawing.Size(136, 22);
            this.defaultMenuItem_Upload.Text = "上传";
            this.defaultMenuItem_Upload.Click += new System.EventHandler(this.MenuItem_Upload_Click);
            // 
            // defaultMenuItem_NewDir
            // 
            this.defaultMenuItem_NewDir.Image = global::Filemanager_for_UPYUN.Properties.Resources.NewFolder;
            this.defaultMenuItem_NewDir.Name = "defaultMenuItem_NewDir";
            this.defaultMenuItem_NewDir.Size = new System.Drawing.Size(136, 22);
            this.defaultMenuItem_NewDir.Text = "新建文件夹";
            this.defaultMenuItem_NewDir.Click += new System.EventHandler(this.MenuItem_NewDir_Click);
            // 
            // defaultMenuItem_Refresh
            // 
            this.defaultMenuItem_Refresh.Image = global::Filemanager_for_UPYUN.Properties.Resources.Refresh;
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
            // tsOperationBar
            // 
            this.tsOperationBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsOperationBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsOperationBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbUpload,
            this.tsbDownload,
            this.tsbNewDir,
            this.tsbDelete});
            this.tsOperationBar.Location = new System.Drawing.Point(0, 0);
            this.tsOperationBar.Name = "tsOperationBar";
            this.tsOperationBar.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.tsOperationBar.Size = new System.Drawing.Size(370, 30);
            this.tsOperationBar.TabIndex = 1;
            // 
            // tsbUpload
            // 
            this.tsbUpload.Image = global::Filemanager_for_UPYUN.Properties.Resources.Upload;
            this.tsbUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUpload.Name = "tsbUpload";
            this.tsbUpload.Size = new System.Drawing.Size(52, 27);
            this.tsbUpload.Text = "上传";
            this.tsbUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // tsbDownload
            // 
            this.tsbDownload.Enabled = false;
            this.tsbDownload.Image = global::Filemanager_for_UPYUN.Properties.Resources.Download;
            this.tsbDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownload.Name = "tsbDownload";
            this.tsbDownload.Size = new System.Drawing.Size(52, 27);
            this.tsbDownload.Text = "下载";
            this.tsbDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // tsbNewDir
            // 
            this.tsbNewDir.Image = global::Filemanager_for_UPYUN.Properties.Resources.NewFolder;
            this.tsbNewDir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewDir.Name = "tsbNewDir";
            this.tsbNewDir.Size = new System.Drawing.Size(88, 27);
            this.tsbNewDir.Text = "新建文件夹";
            this.tsbNewDir.Click += new System.EventHandler(this.btnNewDir_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Enabled = false;
            this.tsbDelete.Image = global::Filemanager_for_UPYUN.Properties.Resources.Delete;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(52, 27);
            this.tsbDelete.Text = "删除";
            this.tsbDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // itemContextMenu
            // 
            this.itemContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemMenuItem_OpenDir,
            this.itemMenuItem_Download,
            this.itemMenuItem_Delete,
            this.itemMenuItem_OpenParentDir});
            this.itemContextMenu.Name = "dirContextMenu";
            this.itemContextMenu.Size = new System.Drawing.Size(149, 92);
            // 
            // itemMenuItem_OpenDir
            // 
            this.itemMenuItem_OpenDir.Image = global::Filemanager_for_UPYUN.Properties.Resources.OpenFolder;
            this.itemMenuItem_OpenDir.Name = "itemMenuItem_OpenDir";
            this.itemMenuItem_OpenDir.Size = new System.Drawing.Size(148, 22);
            this.itemMenuItem_OpenDir.Text = "打开";
            this.itemMenuItem_OpenDir.Click += new System.EventHandler(this.MenuItem_OpenDir_Click);
            // 
            // itemMenuItem_Download
            // 
            this.itemMenuItem_Download.Image = global::Filemanager_for_UPYUN.Properties.Resources.Download;
            this.itemMenuItem_Download.Name = "itemMenuItem_Download";
            this.itemMenuItem_Download.Size = new System.Drawing.Size(148, 22);
            this.itemMenuItem_Download.Text = "下载";
            this.itemMenuItem_Download.Click += new System.EventHandler(this.MenuItem_Download_Click);
            // 
            // itemMenuItem_Delete
            // 
            this.itemMenuItem_Delete.Image = global::Filemanager_for_UPYUN.Properties.Resources.Delete;
            this.itemMenuItem_Delete.Name = "itemMenuItem_Delete";
            this.itemMenuItem_Delete.Size = new System.Drawing.Size(148, 22);
            this.itemMenuItem_Delete.Text = "删除";
            this.itemMenuItem_Delete.Click += new System.EventHandler(this.MenuItem_Delete_Click);
            // 
            // itemMenuItem_OpenParentDir
            // 
            this.itemMenuItem_OpenParentDir.Image = global::Filemanager_for_UPYUN.Properties.Resources.Folder;
            this.itemMenuItem_OpenParentDir.Name = "itemMenuItem_OpenParentDir";
            this.itemMenuItem_OpenParentDir.Size = new System.Drawing.Size(148, 22);
            this.itemMenuItem_OpenParentDir.Text = "打开所在目录";
            this.itemMenuItem_OpenParentDir.Visible = false;
            this.itemMenuItem_OpenParentDir.Click += new System.EventHandler(this.MenuItem_OpenParentDir_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 60);
            this.panel1.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(584, 30);
            this.panel4.TabIndex = 5;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel10);
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(584, 30);
            this.panel6.TabIndex = 1;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Controls.Add(this.panel9);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(67, 0);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(517, 30);
            this.panel10.TabIndex = 1;
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.tsUrlBar);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(303, 30);
            this.panel11.TabIndex = 1;
            // 
            // tsUrlBar
            // 
            this.tsUrlBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsUrlBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsUrlBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbHome,
            this.tslDefaultArrow});
            this.tsUrlBar.Location = new System.Drawing.Point(0, 0);
            this.tsUrlBar.Name = "tsUrlBar";
            this.tsUrlBar.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.tsUrlBar.Size = new System.Drawing.Size(303, 30);
            this.tsUrlBar.TabIndex = 4;
            this.tsUrlBar.Text = "tsUrlBar";
            // 
            // tsbHome
            // 
            this.tsbHome.Image = global::Filemanager_for_UPYUN.Properties.Resources.Home;
            this.tsbHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHome.Name = "tsbHome";
            this.tsbHome.Size = new System.Drawing.Size(52, 27);
            this.tsbHome.Text = "主页";
            this.tsbHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // tslDefaultArrow
            // 
            this.tslDefaultArrow.Image = global::Filemanager_for_UPYUN.Properties.Resources.Arrow;
            this.tslDefaultArrow.Name = "tslDefaultArrow";
            this.tslDefaultArrow.Size = new System.Drawing.Size(16, 27);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.tsSearchBar);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel9.Location = new System.Drawing.Point(303, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(214, 30);
            this.panel9.TabIndex = 0;
            // 
            // tsSearchBar
            // 
            this.tsSearchBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsSearchBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsSearchBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh,
            this.tsbSearchText,
            this.tsbSearchButton});
            this.tsSearchBar.Location = new System.Drawing.Point(0, 0);
            this.tsSearchBar.Name = "tsSearchBar";
            this.tsSearchBar.Size = new System.Drawing.Size(214, 30);
            this.tsSearchBar.TabIndex = 0;
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRefresh.Image = global::Filemanager_for_UPYUN.Properties.Resources.Refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(23, 27);
            this.tsbRefresh.Text = "刷新";
            this.tsbRefresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // tsbSearchText
            // 
            this.tsbSearchText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tsbSearchText.MaxLength = 20;
            this.tsbSearchText.Name = "tsbSearchText";
            this.tsbSearchText.Size = new System.Drawing.Size(150, 30);
            this.tsbSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tsbSearchText_KeyDown);
            this.tsbSearchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Text_KeyPress);
            this.tsbSearchText.TextChanged += new System.EventHandler(this.Text_TextChanged);
            // 
            // tsbSearchButton
            // 
            this.tsbSearchButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSearchButton.Image = global::Filemanager_for_UPYUN.Properties.Resources.Search;
            this.tsbSearchButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSearchButton.Name = "tsbSearchButton";
            this.tsbSearchButton.Size = new System.Drawing.Size(23, 27);
            this.tsbSearchButton.Text = "搜索";
            this.tsbSearchButton.Click += new System.EventHandler(this.tsbSearchButton_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.tsHistoryBar);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(67, 30);
            this.panel5.TabIndex = 0;
            // 
            // tsHistoryBar
            // 
            this.tsHistoryBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsHistoryBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsHistoryBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbBack,
            this.tsbForward});
            this.tsHistoryBar.Location = new System.Drawing.Point(0, 0);
            this.tsHistoryBar.Name = "tsHistoryBar";
            this.tsHistoryBar.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.tsHistoryBar.Size = new System.Drawing.Size(67, 30);
            this.tsHistoryBar.TabIndex = 0;
            // 
            // tsbBack
            // 
            this.tsbBack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbBack.Enabled = false;
            this.tsbBack.Image = global::Filemanager_for_UPYUN.Properties.Resources.Back;
            this.tsbBack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbBack.Name = "tsbBack";
            this.tsbBack.Size = new System.Drawing.Size(23, 27);
            this.tsbBack.Text = "后退";
            this.tsbBack.Click += new System.EventHandler(this.tsbBack_Click);
            // 
            // tsbForward
            // 
            this.tsbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbForward.Enabled = false;
            this.tsbForward.Image = global::Filemanager_for_UPYUN.Properties.Resources.Forward;
            this.tsbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbForward.Name = "tsbForward";
            this.tsbForward.Size = new System.Drawing.Size(23, 27);
            this.tsbForward.Text = "前进";
            this.tsbForward.Click += new System.EventHandler(this.tsbForward_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel8);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(584, 30);
            this.panel3.TabIndex = 4;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.tsOperationBar);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(370, 30);
            this.panel8.TabIndex = 3;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.tsBucketUsageBar);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(370, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(214, 30);
            this.panel7.TabIndex = 2;
            // 
            // tsBucketUsageBar
            // 
            this.tsBucketUsageBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsBucketUsageBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslBucketUsageDesc,
            this.tslBucketUsage});
            this.tsBucketUsageBar.Location = new System.Drawing.Point(0, 0);
            this.tsBucketUsageBar.Name = "tsBucketUsageBar";
            this.tsBucketUsageBar.Padding = new System.Windows.Forms.Padding(0, 8, 1, 0);
            this.tsBucketUsageBar.Size = new System.Drawing.Size(214, 28);
            this.tsBucketUsageBar.TabIndex = 0;
            // 
            // tslBucketUsageDesc
            // 
            this.tslBucketUsageDesc.Name = "tslBucketUsageDesc";
            this.tslBucketUsageDesc.Size = new System.Drawing.Size(68, 17);
            this.tslBucketUsageDesc.Text = "已用空间：";
            // 
            // tslBucketUsage
            // 
            this.tslBucketUsage.Name = "tslBucketUsage";
            this.tslBucketUsage.Size = new System.Drawing.Size(15, 17);
            this.tslBucketUsage.Text = "0";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkAll);
            this.panel2.Controls.Add(this.lvList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(584, 352);
            this.panel2.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 412);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UP云管家";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.defaultContextMenu.ResumeLayout(false);
            this.tsOperationBar.ResumeLayout(false);
            this.tsOperationBar.PerformLayout();
            this.itemContextMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.tsUrlBar.ResumeLayout(false);
            this.tsUrlBar.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.tsSearchBar.ResumeLayout(false);
            this.tsSearchBar.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tsHistoryBar.ResumeLayout(false);
            this.tsHistoryBar.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.tsBucketUsageBar.ResumeLayout(false);
            this.tsBucketUsageBar.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.ContextMenuStrip itemContextMenu;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.ToolStripMenuItem itemMenuItem_Delete;
        private System.Windows.Forms.ToolStripMenuItem itemMenuItem_OpenDir;
        private System.Windows.Forms.ToolStripMenuItem itemMenuItem_Download;
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
        private System.Windows.Forms.ToolStripLabel tslDefaultArrow;
        private System.Windows.Forms.ToolStrip tsSearchBar;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ToolStripTextBox tsbSearchText;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.ToolStrip tsHistoryBar;
        private System.Windows.Forms.ToolStripButton tsbBack;
        private System.Windows.Forms.ToolStripButton tsbForward;
        private System.Windows.Forms.ToolStripButton tsbSearchButton;
        private System.Windows.Forms.ToolStrip tsBucketUsageBar;
        private System.Windows.Forms.ToolStripLabel tslBucketUsageDesc;
        private System.Windows.Forms.ToolStripLabel tslBucketUsage;
        private System.Windows.Forms.ToolStrip tsOperationBar;
        private System.Windows.Forms.ToolStripMenuItem itemMenuItem_OpenParentDir;
    }
}