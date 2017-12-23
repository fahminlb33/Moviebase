namespace Moviebase.Views
{
    partial class MainView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.grdMovies = new System.Windows.Forms.DataGridView();
            this.ctDatagrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuReSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIgnore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSelectPoster = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAlternativeName = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.ctActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuFetchAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDownloadAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRenameAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFolderThumbnail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSavePresistData = new System.Windows.Forms.ToolStripMenuItem();
            this.ctTools = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuMovieToFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.prgStatus = new System.Windows.Forms.ProgressBar();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblExtraInfo = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPlot = new System.Windows.Forms.RichTextBox();
            this.cmdAbout = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cmdFolderRecent = new System.Windows.Forms.Button();
            this.cmdFolderClose = new System.Windows.Forms.Button();
            this.cmdFolderOpen = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdActions = new Moviebase.Views.DropDownButton();
            this.cmdTools = new Moviebase.Views.DropDownButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdMovies)).BeginInit();
            this.ctDatagrid.SuspendLayout();
            this.ctActions.SuspendLayout();
            this.ctTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMovies
            // 
            this.grdMovies.AllowUserToAddRows = false;
            this.grdMovies.AllowUserToDeleteRows = false;
            this.grdMovies.BackgroundColor = System.Drawing.Color.White;
            this.grdMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMovies.ContextMenuStrip = this.ctDatagrid;
            this.grdMovies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMovies.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.grdMovies.Location = new System.Drawing.Point(0, 13);
            this.grdMovies.Name = "grdMovies";
            this.grdMovies.Size = new System.Drawing.Size(532, 302);
            this.grdMovies.TabIndex = 0;
            this.grdMovies.SelectionChanged += new System.EventHandler(this.grdMovies_SelectionChanged);
            this.grdMovies.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdMovies_MouseDown);
            // 
            // ctDatagrid
            // 
            this.ctDatagrid.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctDatagrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReSearch,
            this.mnuIgnore,
            this.toolStripSeparator5,
            this.mnuSelectPoster,
            this.mnuAlternativeName});
            this.ctDatagrid.Name = "ctDatagrid";
            this.ctDatagrid.Size = new System.Drawing.Size(231, 98);
            // 
            // mnuReSearch
            // 
            this.mnuReSearch.Name = "mnuReSearch";
            this.mnuReSearch.Size = new System.Drawing.Size(230, 22);
            this.mnuReSearch.Text = "Search movie metadata again";
            this.mnuReSearch.Click += new System.EventHandler(this.mnuReSearch_Click);
            // 
            // mnuIgnore
            // 
            this.mnuIgnore.Name = "mnuIgnore";
            this.mnuIgnore.Size = new System.Drawing.Size(230, 22);
            this.mnuIgnore.Text = "Ignore this file from fetch";
            this.mnuIgnore.Click += new System.EventHandler(this.mnuIgnore_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(227, 6);
            // 
            // mnuSelectPoster
            // 
            this.mnuSelectPoster.Name = "mnuSelectPoster";
            this.mnuSelectPoster.Size = new System.Drawing.Size(230, 22);
            this.mnuSelectPoster.Text = "Select poster";
            this.mnuSelectPoster.Click += new System.EventHandler(this.mnuSelectPoster_Click);
            // 
            // mnuAlternativeName
            // 
            this.mnuAlternativeName.Name = "mnuAlternativeName";
            this.mnuAlternativeName.Size = new System.Drawing.Size(230, 22);
            this.mnuAlternativeName.Text = "Find alternative name(s)";
            this.mnuAlternativeName.Click += new System.EventHandler(this.mnuAlternativeNames_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Movie list:";
            // 
            // ctActions
            // 
            this.ctActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ctActions.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFetchAll,
            this.toolStripSeparator2,
            this.mnuDownloadAll,
            this.mnuRenameAll,
            this.mnuFolderThumbnail,
            this.toolStripSeparator3,
            this.mnuSavePresistData});
            this.ctActions.Name = "ctActions";
            this.ctActions.Size = new System.Drawing.Size(192, 126);
            // 
            // mnuFetchAll
            // 
            this.mnuFetchAll.ForeColor = System.Drawing.Color.White;
            this.mnuFetchAll.Name = "mnuFetchAll";
            this.mnuFetchAll.Size = new System.Drawing.Size(191, 22);
            this.mnuFetchAll.Text = "Fetch from internet all";
            this.mnuFetchAll.Click += new System.EventHandler(this.mnuFetchAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.Color.White;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuDownloadAll
            // 
            this.mnuDownloadAll.ForeColor = System.Drawing.Color.White;
            this.mnuDownloadAll.Name = "mnuDownloadAll";
            this.mnuDownloadAll.Size = new System.Drawing.Size(191, 22);
            this.mnuDownloadAll.Text = "Download poster all";
            this.mnuDownloadAll.Click += new System.EventHandler(this.mnuDownloadAll_Click);
            // 
            // mnuRenameAll
            // 
            this.mnuRenameAll.ForeColor = System.Drawing.Color.White;
            this.mnuRenameAll.Name = "mnuRenameAll";
            this.mnuRenameAll.Size = new System.Drawing.Size(191, 22);
            this.mnuRenameAll.Text = "Rename all";
            this.mnuRenameAll.Click += new System.EventHandler(this.mnuRenameAll_Click);
            // 
            // mnuFolderThumbnail
            // 
            this.mnuFolderThumbnail.ForeColor = System.Drawing.Color.White;
            this.mnuFolderThumbnail.Name = "mnuFolderThumbnail";
            this.mnuFolderThumbnail.Size = new System.Drawing.Size(191, 22);
            this.mnuFolderThumbnail.Text = "Folder thumbnail all";
            this.mnuFolderThumbnail.Click += new System.EventHandler(this.mnuFolderThumbnail_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.ForeColor = System.Drawing.Color.White;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuSavePresistData
            // 
            this.mnuSavePresistData.ForeColor = System.Drawing.Color.White;
            this.mnuSavePresistData.Name = "mnuSavePresistData";
            this.mnuSavePresistData.Size = new System.Drawing.Size(191, 22);
            this.mnuSavePresistData.Text = "Save presist data";
            this.mnuSavePresistData.Click += new System.EventHandler(this.mnuSavePresistData_Click);
            // 
            // ctTools
            // 
            this.ctTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.ctTools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.ctTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMovieToFolder,
            this.mnuExportCsv});
            this.ctTools.Name = "ctTools";
            this.ctTools.Size = new System.Drawing.Size(165, 48);
            // 
            // mnuMovieToFolder
            // 
            this.mnuMovieToFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mnuMovieToFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuMovieToFolder.ForeColor = System.Drawing.Color.White;
            this.mnuMovieToFolder.Name = "mnuMovieToFolder";
            this.mnuMovieToFolder.Size = new System.Drawing.Size(164, 22);
            this.mnuMovieToFolder.Text = "Movie to folder...";
            this.mnuMovieToFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.mnuMovieToFolder.Click += new System.EventHandler(this.mnuMoveVies_Click);
            // 
            // mnuExportCsv
            // 
            this.mnuExportCsv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mnuExportCsv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuExportCsv.ForeColor = System.Drawing.Color.White;
            this.mnuExportCsv.Name = "mnuExportCsv";
            this.mnuExportCsv.Size = new System.Drawing.Size(164, 22);
            this.mnuExportCsv.Text = "Export to CSV...";
            this.mnuExportCsv.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.mnuExportCsv.Click += new System.EventHandler(this.mnuExportCsv_Click);
            // 
            // prgStatus
            // 
            this.prgStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.prgStatus.Location = new System.Drawing.Point(0, 439);
            this.prgStatus.Name = "prgStatus";
            this.prgStatus.Size = new System.Drawing.Size(786, 5);
            this.prgStatus.TabIndex = 14;
            // 
            // lblPercentage
            // 
            this.lblPercentage.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPercentage.ForeColor = System.Drawing.Color.White;
            this.lblPercentage.Location = new System.Drawing.Point(425, 0);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(361, 21);
            this.lblPercentage.TabIndex = 17;
            this.lblPercentage.Text = "0%";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(291, 21);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 20);
            this.lblTitle.TabIndex = 23;
            this.lblTitle.Text = "...";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblExtraInfo
            // 
            this.lblExtraInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblExtraInfo.Location = new System.Drawing.Point(0, 20);
            this.lblExtraInfo.Name = "lblExtraInfo";
            this.lblExtraInfo.Size = new System.Drawing.Size(232, 27);
            this.lblExtraInfo.TabIndex = 24;
            this.lblExtraInfo.Text = "...";
            this.lblExtraInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 109);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(13);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdMovies);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(768, 315);
            this.splitContainer1.SplitterDistance = 532;
            this.splitContainer1.TabIndex = 26;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtPlot);
            this.splitContainer2.Panel2.Controls.Add(this.lblExtraInfo);
            this.splitContainer2.Panel2.Controls.Add(this.lblTitle);
            this.splitContainer2.Size = new System.Drawing.Size(232, 315);
            this.splitContainer2.SplitterDistance = 160;
            this.splitContainer2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(232, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // txtPlot
            // 
            this.txtPlot.BackColor = System.Drawing.Color.White;
            this.txtPlot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPlot.Location = new System.Drawing.Point(0, 47);
            this.txtPlot.Margin = new System.Windows.Forms.Padding(2);
            this.txtPlot.Name = "txtPlot";
            this.txtPlot.ReadOnly = true;
            this.txtPlot.ShowSelectionMargin = true;
            this.txtPlot.Size = new System.Drawing.Size(232, 104);
            this.txtPlot.TabIndex = 25;
            this.txtPlot.Text = "No Summary";
            // 
            // cmdAbout
            // 
            this.cmdAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAbout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.cmdAbout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdAbout.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.cmdAbout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdAbout.ForeColor = System.Drawing.Color.White;
            this.cmdAbout.Location = new System.Drawing.Point(734, 9);
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new System.Drawing.Size(43, 43);
            this.cmdAbout.TabIndex = 27;
            this.cmdAbout.Text = "?";
            this.cmdAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.cmdAbout, "About Moviebase");
            this.cmdAbout.UseVisualStyleBackColor = false;
            this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panel1.Controls.Add(this.lblStatus);
            this.panel1.Controls.Add(this.lblPercentage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 444);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 21);
            this.panel1.TabIndex = 28;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.cmdAbout);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 57);
            this.panel2.TabIndex = 29;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(660, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 43);
            this.button1.TabIndex = 32;
            this.button1.Text = "Settings";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.button1, "Settings Window");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(20, 21);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 31;
            this.label2.Text = "Moviebase";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.panel4.Controls.Add(this.cmdFolderRecent);
            this.panel4.Controls.Add(this.cmdFolderClose);
            this.panel4.Controls.Add(this.cmdFolderOpen);
            this.panel4.Controls.Add(this.cmdStop);
            this.panel4.Controls.Add(this.cmdActions);
            this.panel4.Controls.Add(this.cmdTools);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 57);
            this.panel4.Margin = new System.Windows.Forms.Padding(2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(786, 43);
            this.panel4.TabIndex = 31;
            // 
            // cmdFolderRecent
            // 
            this.cmdFolderRecent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.cmdFolderRecent.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdFolderRecent.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.cmdFolderRecent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdFolderRecent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdFolderRecent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdFolderRecent.ForeColor = System.Drawing.Color.White;
            this.cmdFolderRecent.Location = new System.Drawing.Point(148, 10);
            this.cmdFolderRecent.Name = "cmdFolderRecent";
            this.cmdFolderRecent.Size = new System.Drawing.Size(68, 26);
            this.cmdFolderRecent.TabIndex = 35;
            this.cmdFolderRecent.Text = "Recent";
            this.cmdFolderRecent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.cmdFolderRecent, "Open Recent Directory");
            this.cmdFolderRecent.UseVisualStyleBackColor = false;
            this.cmdFolderRecent.Click += new System.EventHandler(this.cmdFolderRecent_Click);
            // 
            // cmdFolderClose
            // 
            this.cmdFolderClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.cmdFolderClose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdFolderClose.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.cmdFolderClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdFolderClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdFolderClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdFolderClose.ForeColor = System.Drawing.Color.White;
            this.cmdFolderClose.Location = new System.Drawing.Point(71, 10);
            this.cmdFolderClose.Name = "cmdFolderClose";
            this.cmdFolderClose.Size = new System.Drawing.Size(55, 26);
            this.cmdFolderClose.TabIndex = 34;
            this.cmdFolderClose.Text = "Close";
            this.cmdFolderClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.cmdFolderClose, "Close Directory");
            this.cmdFolderClose.UseVisualStyleBackColor = false;
            this.cmdFolderClose.Click += new System.EventHandler(this.cmdFolderClose_Click);
            // 
            // cmdFolderOpen
            // 
            this.cmdFolderOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.cmdFolderOpen.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdFolderOpen.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.cmdFolderOpen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdFolderOpen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdFolderOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdFolderOpen.ForeColor = System.Drawing.Color.White;
            this.cmdFolderOpen.Location = new System.Drawing.Point(9, 10);
            this.cmdFolderOpen.Name = "cmdFolderOpen";
            this.cmdFolderOpen.Size = new System.Drawing.Size(54, 26);
            this.cmdFolderOpen.TabIndex = 33;
            this.cmdFolderOpen.Text = "Open";
            this.cmdFolderOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.cmdFolderOpen, "Open Directory");
            this.cmdFolderOpen.UseVisualStyleBackColor = false;
            this.cmdFolderOpen.Click += new System.EventHandler(this.cmdFolderOpen_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.cmdStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdStop.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.cmdStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdStop.ForeColor = System.Drawing.Color.White;
            this.cmdStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdStop.Location = new System.Drawing.Point(703, 10);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(75, 26);
            this.cmdStop.TabIndex = 21;
            this.cmdStop.Text = "Stop";
            this.toolTip1.SetToolTip(this.cmdStop, "Stop current Operation");
            this.cmdStop.UseVisualStyleBackColor = false;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdActions
            // 
            this.cmdActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(38)))));
            this.cmdActions.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdActions.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(55)))));
            this.cmdActions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdActions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdActions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdActions.ForeColor = System.Drawing.Color.White;
            this.cmdActions.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdActions.Location = new System.Drawing.Point(581, 10);
            this.cmdActions.Menu = this.ctActions;
            this.cmdActions.Name = "cmdActions";
            this.cmdActions.Size = new System.Drawing.Size(116, 26);
            this.cmdActions.TabIndex = 10;
            this.cmdActions.Text = "Actions ";
            this.toolTip1.SetToolTip(this.cmdActions, "Actions List");
            this.cmdActions.UseVisualStyleBackColor = false;
            // 
            // cmdTools
            // 
            this.cmdTools.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdTools.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdTools.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdTools.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdTools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdTools.ForeColor = System.Drawing.Color.White;
            this.cmdTools.Location = new System.Drawing.Point(334, 10);
            this.cmdTools.Menu = this.ctTools;
            this.cmdTools.Name = "cmdTools";
            this.cmdTools.Size = new System.Drawing.Size(118, 26);
            this.cmdTools.TabIndex = 12;
            this.cmdTools.Text = "Tools ";
            this.cmdTools.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.cmdTools, "Tool Collections");
            this.cmdTools.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.toolTip1.ForeColor = System.Drawing.Color.White;
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.toolTip1_Draw);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(786, 465);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.prgStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Moviebase";
            ((System.ComponentModel.ISupportInitialize)(this.grdMovies)).EndInit();
            this.ctDatagrid.ResumeLayout(false);
            this.ctActions.ResumeLayout(false);
            this.ctTools.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdMovies;
        private System.Windows.Forms.Label label1;
        private DropDownButton cmdActions;
        private System.Windows.Forms.ContextMenuStrip ctActions;
        private System.Windows.Forms.ToolStripMenuItem mnuRenameAll;
        private System.Windows.Forms.ToolStripMenuItem mnuDownloadAll;
        private DropDownButton cmdTools;
        private System.Windows.Forms.ContextMenuStrip ctTools;
        private System.Windows.Forms.ProgressBar prgStatus;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuMovieToFolder;
        private System.Windows.Forms.ToolStripMenuItem mnuFetchAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button cmdStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuSavePresistData;
        private System.Windows.Forms.ContextMenuStrip ctDatagrid;
        private System.Windows.Forms.ToolStripMenuItem mnuSelectPoster;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblExtraInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem mnuAlternativeName;
        private System.Windows.Forms.ToolStripMenuItem mnuIgnore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuFolderThumbnail;
        private System.Windows.Forms.ToolStripMenuItem mnuReSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuExportCsv;
        private System.Windows.Forms.Button cmdAbout;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RichTextBox txtPlot;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdFolderRecent;
        private System.Windows.Forms.Button cmdFolderClose;
        private System.Windows.Forms.Button cmdFolderOpen;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

