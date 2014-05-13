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
            this.lvList = new System.Windows.Forms.ListView();
            this.colFileName = new System.Windows.Forms.ColumnHeader("(无)");
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.colModiDate = new System.Windows.Forms.ColumnHeader();
            this.defaultContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.defaultMenuItem_Upload = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultMenuItem_NewDir = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultMenuItem_Refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.fileContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fileMenuItem_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.dirContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dirMenuItem_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.dirMenuItem_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultContextMenu.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.fileContextMenu.SuspendLayout();
            this.dirContextMenu.SuspendLayout();
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
            this.lvList.Size = new System.Drawing.Size(586, 413);
            this.lvList.SmallImageList = this.imgList;
            this.lvList.TabIndex = 0;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            this.lvList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lvList_ItemChecked);
            this.lvList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvList_ColumnClick);
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
            this.colSize.Text = "文件大小(B)";
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
            this.defaultMenuItem_Upload.Name = "defaultMenuItem_Upload";
            this.defaultMenuItem_Upload.Size = new System.Drawing.Size(136, 22);
            this.defaultMenuItem_Upload.Text = "上传";
            this.defaultMenuItem_Upload.Click += new System.EventHandler(this.MenuItem_Upload_Click);
            // 
            // defaultMenuItem_NewDir
            // 
            this.defaultMenuItem_NewDir.Name = "defaultMenuItem_NewDir";
            this.defaultMenuItem_NewDir.Size = new System.Drawing.Size(136, 22);
            this.defaultMenuItem_NewDir.Text = "新建文件夹";
            this.defaultMenuItem_NewDir.Click += new System.EventHandler(this.MenuItem_NewDir_Click);
            // 
            // defaultMenuItem_Refresh
            // 
            this.defaultMenuItem_Refresh.Name = "defaultMenuItem_Refresh";
            this.defaultMenuItem_Refresh.Size = new System.Drawing.Size(136, 22);
            this.defaultMenuItem_Refresh.Text = "刷新";
            this.defaultMenuItem_Refresh.Click += new System.EventHandler(this.MenuItem_Refresh_Click);
            // 
            // imgList
            // 
            this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgList.ImageSize = new System.Drawing.Size(16, 16);
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.chkAll);
            this.splitContainer1.Panel2.Controls.Add(this.lvList);
            this.splitContainer1.Size = new System.Drawing.Size(586, 461);
            this.splitContainer1.SplitterDistance = 44;
            this.splitContainer1.TabIndex = 2;
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
            // fileContextMenu
            // 
            this.fileContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem_Rename,
            this.fileMenuItem_Delete});
            this.fileContextMenu.Name = "fileContextMenu";
            this.fileContextMenu.Size = new System.Drawing.Size(113, 48);
            // 
            // fileMenuItem_Rename
            // 
            this.fileMenuItem_Rename.Name = "fileMenuItem_Rename";
            this.fileMenuItem_Rename.Size = new System.Drawing.Size(112, 22);
            this.fileMenuItem_Rename.Text = "重命名";
            this.fileMenuItem_Rename.Visible = false;
            this.fileMenuItem_Rename.Click += new System.EventHandler(this.MenuItem_Rename_Click);
            // 
            // fileMenuItem_Delete
            // 
            this.fileMenuItem_Delete.Name = "fileMenuItem_Delete";
            this.fileMenuItem_Delete.Size = new System.Drawing.Size(112, 22);
            this.fileMenuItem_Delete.Text = "删除";
            this.fileMenuItem_Delete.Click += new System.EventHandler(this.MenuItem_Delete_Click);
            // 
            // dirContextMenu
            // 
            this.dirContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dirMenuItem_Rename,
            this.dirMenuItem_Delete});
            this.dirContextMenu.Name = "dirContextMenu";
            this.dirContextMenu.Size = new System.Drawing.Size(113, 48);
            // 
            // dirMenuItem_Rename
            // 
            this.dirMenuItem_Rename.Name = "dirMenuItem_Rename";
            this.dirMenuItem_Rename.Size = new System.Drawing.Size(112, 22);
            this.dirMenuItem_Rename.Text = "重命名";
            this.dirMenuItem_Rename.Visible = false;
            this.dirMenuItem_Rename.Click += new System.EventHandler(this.MenuItem_Rename_Click);
            // 
            // dirMenuItem_Delete
            // 
            this.dirMenuItem_Delete.Name = "dirMenuItem_Delete";
            this.dirMenuItem_Delete.Size = new System.Drawing.Size(112, 22);
            this.dirMenuItem_Delete.Text = "删除";
            this.dirMenuItem_Delete.Click += new System.EventHandler(this.MenuItem_Delete_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 461);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.defaultContextMenu.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.fileContextMenu.ResumeLayout(false);
            this.dirContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvList;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.ColumnHeader colModiDate;
        private System.Windows.Forms.SplitContainer splitContainer1;
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
    }
}