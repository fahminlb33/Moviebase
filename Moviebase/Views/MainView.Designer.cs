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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.mnuRenameAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDownloadAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFolderThumbnail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSavePresistData = new System.Windows.Forms.ToolStripMenuItem();
            this.ctTools = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuMovieToFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.prgStatus = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.ctOpens = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuOpenDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpenLast = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblExtraInfo = new System.Windows.Forms.Label();
            this.txtPlot = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmdAbout = new System.Windows.Forms.Button();
            this.cmdStop = new System.Windows.Forms.Button();
            this.cmdDirectories = new Moviebase.Views.DropDownButton();
            this.cmdTools = new Moviebase.Views.DropDownButton();
            this.cmdActions = new Moviebase.Views.DropDownButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdMovies)).BeginInit();
            this.ctDatagrid.SuspendLayout();
            this.ctActions.SuspendLayout();
            this.ctTools.SuspendLayout();
            this.ctOpens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMovies
            // 
            this.grdMovies.AllowUserToAddRows = false;
            this.grdMovies.AllowUserToDeleteRows = false;
            this.grdMovies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdMovies.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdMovies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdMovies.ContextMenuStrip = this.ctDatagrid;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdMovies.DefaultCellStyle = dataGridViewCellStyle2;
            this.grdMovies.Location = new System.Drawing.Point(3, 3);
            this.grdMovies.Name = "grdMovies";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdMovies.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grdMovies.Size = new System.Drawing.Size(492, 294);
            this.grdMovies.TabIndex = 0;
            this.grdMovies.SelectionChanged += new System.EventHandler(this.grdMovies_SelectionChanged);
            this.grdMovies.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grdMovies_MouseDown);
            // 
            // ctDatagrid
            // 
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
            this.mnuReSearch.Image = global::Moviebase.Properties.Resources.film_edit;
            this.mnuReSearch.Name = "mnuReSearch";
            this.mnuReSearch.Size = new System.Drawing.Size(230, 22);
            this.mnuReSearch.Text = "Search movie metadata again";
            this.mnuReSearch.Click += new System.EventHandler(this.mnuReSearch_Click);
            // 
            // mnuIgnore
            // 
            this.mnuIgnore.Image = global::Moviebase.Properties.Resources.cross;
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
            this.mnuSelectPoster.Image = global::Moviebase.Properties.Resources.image;
            this.mnuSelectPoster.Name = "mnuSelectPoster";
            this.mnuSelectPoster.Size = new System.Drawing.Size(230, 22);
            this.mnuSelectPoster.Text = "Select poster";
            this.mnuSelectPoster.Click += new System.EventHandler(this.mnuSelectPoster_Click);
            // 
            // mnuAlternativeName
            // 
            this.mnuAlternativeName.Image = global::Moviebase.Properties.Resources.textfield_rename;
            this.mnuAlternativeName.Name = "mnuAlternativeName";
            this.mnuAlternativeName.Size = new System.Drawing.Size(230, 22);
            this.mnuAlternativeName.Text = "Find alternative name(s)";
            this.mnuAlternativeName.Click += new System.EventHandler(this.mnuAlternativeNames_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Movie list:";
            // 
            // ctActions
            // 
            this.ctActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFetchAll,
            this.toolStripSeparator2,
            this.mnuRenameAll,
            this.mnuDownloadAll,
            this.mnuFolderThumbnail,
            this.toolStripSeparator3,
            this.mnuSavePresistData});
            this.ctActions.Name = "ctActions";
            this.ctActions.Size = new System.Drawing.Size(192, 126);
            // 
            // mnuFetchAll
            // 
            this.mnuFetchAll.Image = global::Moviebase.Properties.Resources.world;
            this.mnuFetchAll.Name = "mnuFetchAll";
            this.mnuFetchAll.Size = new System.Drawing.Size(191, 22);
            this.mnuFetchAll.Text = "Fetch from internet all";
            this.mnuFetchAll.Click += new System.EventHandler(this.mnuFetchAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuRenameAll
            // 
            this.mnuRenameAll.Image = global::Moviebase.Properties.Resources.textfield_rename;
            this.mnuRenameAll.Name = "mnuRenameAll";
            this.mnuRenameAll.Size = new System.Drawing.Size(191, 22);
            this.mnuRenameAll.Text = "Rename all";
            this.mnuRenameAll.Click += new System.EventHandler(this.mnuRenameAll_Click);
            // 
            // mnuDownloadAll
            // 
            this.mnuDownloadAll.Image = global::Moviebase.Properties.Resources.picture_go;
            this.mnuDownloadAll.Name = "mnuDownloadAll";
            this.mnuDownloadAll.Size = new System.Drawing.Size(191, 22);
            this.mnuDownloadAll.Text = "Download poster all";
            this.mnuDownloadAll.Click += new System.EventHandler(this.mnuDownloadAll_Click);
            // 
            // mnuFolderThumbnail
            // 
            this.mnuFolderThumbnail.Image = global::Moviebase.Properties.Resources.folder_image;
            this.mnuFolderThumbnail.Name = "mnuFolderThumbnail";
            this.mnuFolderThumbnail.Size = new System.Drawing.Size(191, 22);
            this.mnuFolderThumbnail.Text = "Folder thumbnail all";
            this.mnuFolderThumbnail.Click += new System.EventHandler(this.mnuFolderThumbnail_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuSavePresistData
            // 
            this.mnuSavePresistData.Image = global::Moviebase.Properties.Resources.database_save;
            this.mnuSavePresistData.Name = "mnuSavePresistData";
            this.mnuSavePresistData.Size = new System.Drawing.Size(191, 22);
            this.mnuSavePresistData.Text = "Save presist data";
            this.mnuSavePresistData.Click += new System.EventHandler(this.mnuSavePresistData_Click);
            // 
            // ctTools
            // 
            this.ctTools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSettings,
            this.toolStripSeparator4,
            this.mnuMovieToFolder,
            this.mnuExportCsv});
            this.ctTools.Name = "ctTools";
            this.ctTools.Size = new System.Drawing.Size(165, 76);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Image = global::Moviebase.Properties.Resources.wrench;
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(164, 22);
            this.mnuSettings.Text = "Settings";
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
            // 
            // mnuMovieToFolder
            // 
            this.mnuMovieToFolder.Image = global::Moviebase.Properties.Resources.film_go;
            this.mnuMovieToFolder.Name = "mnuMovieToFolder";
            this.mnuMovieToFolder.Size = new System.Drawing.Size(164, 22);
            this.mnuMovieToFolder.Text = "Movie to folder...";
            this.mnuMovieToFolder.Click += new System.EventHandler(this.mnuMoveVies_Click);
            // 
            // mnuExportCsv
            // 
            this.mnuExportCsv.Image = global::Moviebase.Properties.Resources.table_go;
            this.mnuExportCsv.Name = "mnuExportCsv";
            this.mnuExportCsv.Size = new System.Drawing.Size(164, 22);
            this.mnuExportCsv.Text = "Export to CSV...";
            this.mnuExportCsv.Click += new System.EventHandler(this.mnuExportCsv_Click);
            // 
            // prgStatus
            // 
            this.prgStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.prgStatus.Location = new System.Drawing.Point(412, 383);
            this.prgStatus.Name = "prgStatus";
            this.prgStatus.Size = new System.Drawing.Size(237, 13);
            this.prgStatus.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 367);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Status:";
            // 
            // lblPercentage
            // 
            this.lblPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPercentage.Location = new System.Drawing.Point(554, 367);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(95, 13);
            this.lblPercentage.TabIndex = 17;
            this.lblPercentage.Text = "0%";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(58, 367);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(38, 13);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Ready";
            // 
            // ctOpens
            // 
            this.ctOpens.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenDirectory,
            this.mnuCloseFolder,
            this.toolStripSeparator1,
            this.mnuOpenLast});
            this.ctOpens.Name = "ctOpens";
            this.ctOpens.Size = new System.Drawing.Size(218, 76);
            // 
            // mnuOpenDirectory
            // 
            this.mnuOpenDirectory.Image = global::Moviebase.Properties.Resources.folder_explore;
            this.mnuOpenDirectory.Name = "mnuOpenDirectory";
            this.mnuOpenDirectory.Size = new System.Drawing.Size(217, 22);
            this.mnuOpenDirectory.Text = "Open directory...";
            this.mnuOpenDirectory.Click += new System.EventHandler(this.mnuOpenDirectory_Click);
            // 
            // mnuCloseFolder
            // 
            this.mnuCloseFolder.Image = global::Moviebase.Properties.Resources.door_in;
            this.mnuCloseFolder.Name = "mnuCloseFolder";
            this.mnuCloseFolder.Size = new System.Drawing.Size(217, 22);
            this.mnuCloseFolder.Text = "Close folder";
            this.mnuCloseFolder.Click += new System.EventHandler(this.mnuCloseFolder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(214, 6);
            // 
            // mnuOpenLast
            // 
            this.mnuOpenLast.Image = global::Moviebase.Properties.Resources.folder_star;
            this.mnuOpenLast.Name = "mnuOpenLast";
            this.mnuOpenLast.Size = new System.Drawing.Size(217, 22);
            this.mnuOpenLast.Text = "Open last opened directory";
            this.mnuOpenLast.Click += new System.EventHandler(this.mnuOpenLast_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(23, 18);
            this.lblTitle.TabIndex = 23;
            this.lblTitle.Text = "...";
            // 
            // lblExtraInfo
            // 
            this.lblExtraInfo.AutoSize = true;
            this.lblExtraInfo.Location = new System.Drawing.Point(4, 20);
            this.lblExtraInfo.Name = "lblExtraInfo";
            this.lblExtraInfo.Size = new System.Drawing.Size(16, 13);
            this.lblExtraInfo.TabIndex = 24;
            this.lblExtraInfo.Text = "...";
            // 
            // txtPlot
            // 
            this.txtPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlot.Location = new System.Drawing.Point(3, 46);
            this.txtPlot.Multiline = true;
            this.txtPlot.Name = "txtPlot";
            this.txtPlot.ReadOnly = true;
            this.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPlot.Size = new System.Drawing.Size(129, 93);
            this.txtPlot.TabIndex = 25;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 58);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdMovies);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(637, 300);
            this.splitContainer1.SplitterDistance = 498;
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
            this.splitContainer2.Panel2.Controls.Add(this.lblTitle);
            this.splitContainer2.Panel2.Controls.Add(this.lblExtraInfo);
            this.splitContainer2.Size = new System.Drawing.Size(135, 300);
            this.splitContainer2.SplitterDistance = 154;
            this.splitContainer2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(129, 148);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBox1_LoadCompleted);
            // 
            // cmdAbout
            // 
            this.cmdAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAbout.Image = global::Moviebase.Properties.Resources.information;
            this.cmdAbout.Location = new System.Drawing.Point(574, 12);
            this.cmdAbout.Name = "cmdAbout";
            this.cmdAbout.Size = new System.Drawing.Size(75, 23);
            this.cmdAbout.TabIndex = 27;
            this.cmdAbout.Text = "About";
            this.cmdAbout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdAbout.UseVisualStyleBackColor = true;
            this.cmdAbout.Click += new System.EventHandler(this.cmdAbout_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdStop.Image = global::Moviebase.Properties.Resources.stop;
            this.cmdStop.Location = new System.Drawing.Point(493, 12);
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(75, 23);
            this.cmdStop.TabIndex = 21;
            this.cmdStop.Text = "Stop";
            this.cmdStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdStop.UseVisualStyleBackColor = true;
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // cmdDirectories
            // 
            this.cmdDirectories.Image = global::Moviebase.Properties.Resources.folder;
            this.cmdDirectories.Location = new System.Drawing.Point(12, 12);
            this.cmdDirectories.Menu = this.ctOpens;
            this.cmdDirectories.Name = "cmdDirectories";
            this.cmdDirectories.Size = new System.Drawing.Size(106, 23);
            this.cmdDirectories.TabIndex = 20;
            this.cmdDirectories.Text = "Directories ";
            this.cmdDirectories.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdDirectories.UseVisualStyleBackColor = true;
            // 
            // cmdTools
            // 
            this.cmdTools.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTools.Image = global::Moviebase.Properties.Resources.wand;
            this.cmdTools.Location = new System.Drawing.Point(331, 12);
            this.cmdTools.Menu = this.ctTools;
            this.cmdTools.Name = "cmdTools";
            this.cmdTools.Size = new System.Drawing.Size(75, 23);
            this.cmdTools.TabIndex = 12;
            this.cmdTools.Text = "Tools ";
            this.cmdTools.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdTools.UseVisualStyleBackColor = true;
            // 
            // cmdActions
            // 
            this.cmdActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdActions.Image = global::Moviebase.Properties.Resources.lightning;
            this.cmdActions.Location = new System.Drawing.Point(412, 12);
            this.cmdActions.Menu = this.ctActions;
            this.cmdActions.Name = "cmdActions";
            this.cmdActions.Size = new System.Drawing.Size(75, 23);
            this.cmdActions.TabIndex = 10;
            this.cmdActions.Text = "Actions ";
            this.cmdActions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cmdActions.UseVisualStyleBackColor = true;
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 408);
            this.Controls.Add(this.cmdAbout);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cmdStop);
            this.Controls.Add(this.cmdDirectories);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.prgStatus);
            this.Controls.Add(this.cmdTools);
            this.Controls.Add(this.cmdActions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainView";
            this.Text = "Moviebase";
            ((System.ComponentModel.ISupportInitialize)(this.grdMovies)).EndInit();
            this.ctDatagrid.ResumeLayout(false);
            this.ctActions.ResumeLayout(false);
            this.ctTools.ResumeLayout(false);
            this.ctOpens.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label lblStatus;
        private DropDownButton cmdDirectories;
        private System.Windows.Forms.ContextMenuStrip ctOpens;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenDirectory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenLast;
        private System.Windows.Forms.ToolStripMenuItem mnuMovieToFolder;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseFolder;
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
        private System.Windows.Forms.TextBox txtPlot;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem mnuAlternativeName;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuIgnore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuFolderThumbnail;
        private System.Windows.Forms.ToolStripMenuItem mnuReSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuExportCsv;
        private System.Windows.Forms.Button cmdAbout;
    }
}

