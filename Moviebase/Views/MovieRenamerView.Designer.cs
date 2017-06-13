namespace Moviebase.Views
{
    partial class MovieRenamerView
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.prgStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdOpenFolder = new System.Windows.Forms.ToolStripButton();
            this.cmdStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRenameAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdMovieOrganizer = new System.Windows.Forms.ToolStripButton();
            this.cmdSubsceneFinder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdSettings = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.picPoster = new System.Windows.Forms.PictureBox();
            this.lblOutputName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.txtPlot = new System.Windows.Forms.TextBox();
            this.lblGenre = new System.Windows.Forms.Label();
            this.ctGridView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmdSelectPoster = new System.Windows.Forms.ToolStripMenuItem();
            this.cmdFindAlternative = new System.Windows.Forms.ToolStripMenuItem();
            this.dialogFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.appToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMovieOrganizer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSubsceneFinder = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).BeginInit();
            this.ctGridView.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prgStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 370);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(610, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // prgStatus
            // 
            this.prgStatus.Name = "prgStatus";
            this.prgStatus.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdOpenFolder,
            this.cmdStop,
            this.toolStripSeparator5,
            this.cmdRenameAll,
            this.toolStripSeparator1,
            this.cmdMovieOrganizer,
            this.cmdSubsceneFinder,
            this.toolStripSeparator3,
            this.cmdSettings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(610, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdOpenFolder
            // 
            this.cmdOpenFolder.Image = global::Moviebase.Properties.Resources.folder_magnify;
            this.cmdOpenFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdOpenFolder.Name = "cmdOpenFolder";
            this.cmdOpenFolder.Size = new System.Drawing.Size(90, 22);
            this.cmdOpenFolder.Text = "Open folder";
            this.cmdOpenFolder.Click += new System.EventHandler(this.cmdOpenFolder_Click);
            // 
            // cmdStop
            // 
            this.cmdStop.Enabled = false;
            this.cmdStop.Image = global::Moviebase.Properties.Resources.cross;
            this.cmdStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdStop.Name = "cmdStop";
            this.cmdStop.Size = new System.Drawing.Size(51, 22);
            this.cmdStop.Text = "Stop";
            this.cmdStop.Click += new System.EventHandler(this.cmdStop_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdRenameAll
            // 
            this.cmdRenameAll.Image = global::Moviebase.Properties.Resources.wand;
            this.cmdRenameAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRenameAll.Name = "cmdRenameAll";
            this.cmdRenameAll.Size = new System.Drawing.Size(52, 22);
            this.cmdRenameAll.Text = "Do it";
            this.cmdRenameAll.Click += new System.EventHandler(this.cmdRenameAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdMovieOrganizer
            // 
            this.cmdMovieOrganizer.Image = global::Moviebase.Properties.Resources.film_go;
            this.cmdMovieOrganizer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdMovieOrganizer.Name = "cmdMovieOrganizer";
            this.cmdMovieOrganizer.Size = new System.Drawing.Size(114, 22);
            this.cmdMovieOrganizer.Text = "Movie Organizer";
            this.cmdMovieOrganizer.Click += new System.EventHandler(this.cmdMovieOrganizer_Click);
            // 
            // cmdSubsceneFinder
            // 
            this.cmdSubsceneFinder.Image = global::Moviebase.Properties.Resources.page_white_text;
            this.cmdSubsceneFinder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSubsceneFinder.Name = "cmdSubsceneFinder";
            this.cmdSubsceneFinder.Size = new System.Drawing.Size(113, 22);
            this.cmdSubsceneFinder.Text = "Subscene Finder";
            this.cmdSubsceneFinder.Click += new System.EventHandler(this.cmdSubsceneFinder_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdSettings
            // 
            this.cmdSettings.Image = global::Moviebase.Properties.Resources.wrench;
            this.cmdSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.Size = new System.Drawing.Size(69, 22);
            this.cmdSettings.Text = "Settings";
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(610, 321);
            this.splitContainer1.SplitterDistance = 391;
            this.splitContainer1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(387, 317);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
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
            this.splitContainer2.Panel1.Controls.Add(this.picPoster);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lblOutputName);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.lblTitle);
            this.splitContainer2.Panel2.Controls.Add(this.lblYear);
            this.splitContainer2.Panel2.Controls.Add(this.txtPlot);
            this.splitContainer2.Panel2.Controls.Add(this.lblGenre);
            this.splitContainer2.Size = new System.Drawing.Size(211, 317);
            this.splitContainer2.SplitterDistance = 132;
            this.splitContainer2.TabIndex = 0;
            // 
            // picPoster
            // 
            this.picPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPoster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPoster.Location = new System.Drawing.Point(0, 0);
            this.picPoster.Name = "picPoster";
            this.picPoster.Size = new System.Drawing.Size(211, 132);
            this.picPoster.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPoster.TabIndex = 0;
            this.picPoster.TabStop = false;
            // 
            // lblOutputName
            // 
            this.lblOutputName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblOutputName.AutoSize = true;
            this.lblOutputName.Location = new System.Drawing.Point(9, 160);
            this.lblOutputName.Name = "lblOutputName";
            this.lblOutputName.Size = new System.Drawing.Size(16, 13);
            this.lblOutputName.TabIndex = 5;
            this.lblOutputName.Text = "...";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 147);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Rename to:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(6, 3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(23, 17);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "...";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(6, 20);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(16, 13);
            this.lblYear.TabIndex = 1;
            this.lblYear.Text = "...";
            // 
            // txtPlot
            // 
            this.txtPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPlot.Location = new System.Drawing.Point(6, 49);
            this.txtPlot.Multiline = true;
            this.txtPlot.Name = "txtPlot";
            this.txtPlot.ReadOnly = true;
            this.txtPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPlot.Size = new System.Drawing.Size(204, 91);
            this.txtPlot.TabIndex = 3;
            // 
            // lblGenre
            // 
            this.lblGenre.AutoSize = true;
            this.lblGenre.Location = new System.Drawing.Point(6, 33);
            this.lblGenre.Name = "lblGenre";
            this.lblGenre.Size = new System.Drawing.Size(16, 13);
            this.lblGenre.TabIndex = 2;
            this.lblGenre.Text = "...";
            // 
            // ctGridView
            // 
            this.ctGridView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdSelectPoster,
            this.cmdFindAlternative});
            this.ctGridView.Name = "ctGridView";
            this.ctGridView.Size = new System.Drawing.Size(203, 48);
            // 
            // cmdSelectPoster
            // 
            this.cmdSelectPoster.Image = global::Moviebase.Properties.Resources.image;
            this.cmdSelectPoster.Name = "cmdSelectPoster";
            this.cmdSelectPoster.Size = new System.Drawing.Size(202, 22);
            this.cmdSelectPoster.Text = "Select poster...";
            this.cmdSelectPoster.Click += new System.EventHandler(this.cmdSelectPoster_Click);
            // 
            // cmdFindAlternative
            // 
            this.cmdFindAlternative.Image = global::Moviebase.Properties.Resources.pencil;
            this.cmdFindAlternative.Name = "cmdFindAlternative";
            this.cmdFindAlternative.Size = new System.Drawing.Size(202, 22);
            this.cmdFindAlternative.Text = "Find alternative names...";
            // 
            // dialogFolder
            // 
            this.dialogFolder.Description = "Select folder to analyze";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(610, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // appToolStripMenuItem
            // 
            this.appToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenFolder,
            this.toolStripSeparator2,
            this.mnuSettings,
            this.toolStripSeparator4,
            this.mnuExit});
            this.appToolStripMenuItem.Name = "appToolStripMenuItem";
            this.appToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.appToolStripMenuItem.Text = "App";
            // 
            // mnuOpenFolder
            // 
            this.mnuOpenFolder.Image = global::Moviebase.Properties.Resources.folder_magnify;
            this.mnuOpenFolder.Name = "mnuOpenFolder";
            this.mnuOpenFolder.Size = new System.Drawing.Size(152, 22);
            this.mnuOpenFolder.Text = "Open folder...";
            this.mnuOpenFolder.Click += new System.EventHandler(this.mnuOpenFolder_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuSettings
            // 
            this.mnuSettings.Image = global::Moviebase.Properties.Resources.wrench;
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(152, 22);
            this.mnuSettings.Text = "Settings";
            this.mnuSettings.Click += new System.EventHandler(this.mnuSettings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Image = global::Moviebase.Properties.Resources.cross;
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(152, 22);
            this.mnuExit.Text = "Exit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMovieOrganizer,
            this.mnuSubsceneFinder});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // mnuMovieOrganizer
            // 
            this.mnuMovieOrganizer.Image = global::Moviebase.Properties.Resources.film_go;
            this.mnuMovieOrganizer.Name = "mnuMovieOrganizer";
            this.mnuMovieOrganizer.Size = new System.Drawing.Size(161, 22);
            this.mnuMovieOrganizer.Text = "Movie Organizer";
            this.mnuMovieOrganizer.Click += new System.EventHandler(this.mnuMovieOrganizer_Click);
            // 
            // mnuSubsceneFinder
            // 
            this.mnuSubsceneFinder.Image = global::Moviebase.Properties.Resources.page_white_text;
            this.mnuSubsceneFinder.Name = "mnuSubsceneFinder";
            this.mnuSubsceneFinder.Size = new System.Drawing.Size(161, 22);
            this.mnuSubsceneFinder.Text = "Subscene Finder";
            this.mnuSubsceneFinder.Click += new System.EventHandler(this.mnuSubsceneFinder_Click);
            // 
            // MovieRenamerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 392);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MovieRenamerView";
            this.Text = "Moviebase";
            this.Load += new System.EventHandler(this.MainView_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPoster)).EndInit();
            this.ctGridView.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripButton cmdOpenFolder;
        private System.Windows.Forms.ToolStripButton cmdRenameAll;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox picPoster;
        private System.Windows.Forms.Label lblOutputName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.TextBox txtPlot;
        private System.Windows.Forms.Label lblGenre;
        private System.Windows.Forms.ContextMenuStrip ctGridView;
        private System.Windows.Forms.ToolStripMenuItem cmdSelectPoster;
        private System.Windows.Forms.ToolStripMenuItem cmdFindAlternative;
        private System.Windows.Forms.FolderBrowserDialog dialogFolder;
        private System.Windows.Forms.ToolStripButton cmdStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdMovieOrganizer;
        private System.Windows.Forms.ToolStripButton cmdSubsceneFinder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton cmdSettings;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem appToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuMovieOrganizer;
        private System.Windows.Forms.ToolStripMenuItem mnuSubsceneFinder;
        private System.Windows.Forms.ToolStripProgressBar prgStatus;
    }
}

