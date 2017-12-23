namespace Moviebase.Views
{
    partial class SettingsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsView));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkHidePresist = new System.Windows.Forms.CheckBox();
            this.chkOverwritePoster = new System.Windows.Forms.CheckBox();
            this.chkSwapThe = new System.Windows.Forms.CheckBox();
            this.cmdClearLastDir = new System.Windows.Forms.LinkLabel();
            this.txtLastOpenDir = new System.Windows.Forms.TextBox();
            this.txtTmdbApiKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtExt = new System.Windows.Forms.TextBox();
            this.cmdRemove = new System.Windows.Forms.Button();
            this.cmdAdd = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lstExtensions = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPatternOutput = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtFolderRenamePattern = new System.Windows.Forms.TextBox();
            this.txtFileRenamePattern = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.cmdDetect = new System.Windows.Forms.Button();
            this.picGuessit = new System.Windows.Forms.PictureBox();
            this.picPython = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGuessit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPython)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(434, 322);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkHidePresist);
            this.tabPage1.Controls.Add(this.chkOverwritePoster);
            this.tabPage1.Controls.Add(this.chkSwapThe);
            this.tabPage1.Controls.Add(this.cmdClearLastDir);
            this.tabPage1.Controls.Add(this.txtLastOpenDir);
            this.tabPage1.Controls.Add(this.txtTmdbApiKey);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(426, 295);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkHidePresist
            // 
            this.chkHidePresist.AutoSize = true;
            this.chkHidePresist.Location = new System.Drawing.Point(114, 144);
            this.chkHidePresist.Name = "chkHidePresist";
            this.chkHidePresist.Size = new System.Drawing.Size(97, 17);
            this.chkHidePresist.TabIndex = 21;
            this.chkHidePresist.Text = "Hide persist file";
            this.chkHidePresist.UseVisualStyleBackColor = true;
            // 
            // chkOverwritePoster
            // 
            this.chkOverwritePoster.AutoSize = true;
            this.chkOverwritePoster.Location = new System.Drawing.Point(114, 121);
            this.chkOverwritePoster.Name = "chkOverwritePoster";
            this.chkOverwritePoster.Size = new System.Drawing.Size(195, 17);
            this.chkOverwritePoster.TabIndex = 20;
            this.chkOverwritePoster.Text = "Overwrite poster when downloading";
            this.chkOverwritePoster.UseVisualStyleBackColor = true;
            // 
            // chkSwapThe
            // 
            this.chkSwapThe.AutoSize = true;
            this.chkSwapThe.Location = new System.Drawing.Point(114, 99);
            this.chkSwapThe.Name = "chkSwapThe";
            this.chkSwapThe.Size = new System.Drawing.Size(139, 17);
            this.chkSwapThe.TabIndex = 19;
            this.chkSwapThe.Text = "Swap \'The\' on file name";
            this.chkSwapThe.UseVisualStyleBackColor = true;
            // 
            // cmdClearLastDir
            // 
            this.cmdClearLastDir.AutoSize = true;
            this.cmdClearLastDir.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.cmdClearLastDir.Location = new System.Drawing.Point(358, 59);
            this.cmdClearLastDir.Name = "cmdClearLastDir";
            this.cmdClearLastDir.Size = new System.Drawing.Size(31, 13);
            this.cmdClearLastDir.TabIndex = 18;
            this.cmdClearLastDir.TabStop = true;
            this.cmdClearLastDir.Text = "Clear";
            this.cmdClearLastDir.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdClearLastDir_LinkClicked);
            // 
            // txtLastOpenDir
            // 
            this.txtLastOpenDir.Location = new System.Drawing.Point(114, 57);
            this.txtLastOpenDir.Name = "txtLastOpenDir";
            this.txtLastOpenDir.ReadOnly = true;
            this.txtLastOpenDir.Size = new System.Drawing.Size(238, 20);
            this.txtLastOpenDir.TabIndex = 17;
            // 
            // txtTmdbApiKey
            // 
            this.txtTmdbApiKey.Location = new System.Drawing.Point(114, 25);
            this.txtTmdbApiKey.Name = "txtTmdbApiKey";
            this.txtTmdbApiKey.Size = new System.Drawing.Size(275, 20);
            this.txtTmdbApiKey.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Recent Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "TMDb API Key";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtExt);
            this.tabPage2.Controls.Add(this.cmdRemove);
            this.tabPage2.Controls.Add(this.cmdAdd);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.lstExtensions);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(426, 296);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Detection";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtExt
            // 
            this.txtExt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExt.BackColor = System.Drawing.Color.White;
            this.txtExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtExt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtExt.Location = new System.Drawing.Point(296, 32);
            this.txtExt.Margin = new System.Windows.Forms.Padding(2);
            this.txtExt.Name = "txtExt";
            this.txtExt.Size = new System.Drawing.Size(123, 21);
            this.txtExt.TabIndex = 11;
            // 
            // cmdRemove
            // 
            this.cmdRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdRemove.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdRemove.Location = new System.Drawing.Point(296, 110);
            this.cmdRemove.Margin = new System.Windows.Forms.Padding(2);
            this.cmdRemove.Name = "cmdRemove";
            this.cmdRemove.Size = new System.Drawing.Size(123, 27);
            this.cmdRemove.TabIndex = 10;
            this.cmdRemove.Text = "Remove";
            this.cmdRemove.UseVisualStyleBackColor = true;
            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
            // 
            // cmdAdd
            // 
            this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdAdd.Location = new System.Drawing.Point(296, 55);
            this.cmdAdd.Margin = new System.Windows.Forms.Padding(2);
            this.cmdAdd.Name = "cmdAdd";
            this.cmdAdd.Size = new System.Drawing.Size(123, 27);
            this.cmdAdd.TabIndex = 9;
            this.cmdAdd.Text = "Add";
            this.cmdAdd.UseVisualStyleBackColor = true;
            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 204);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(278, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "Folder which surrounded by [ ] are ignored from detection.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Detect these files:";
            // 
            // lstExtensions
            // 
            this.lstExtensions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstExtensions.FormattingEnabled = true;
            this.lstExtensions.Location = new System.Drawing.Point(17, 32);
            this.lstExtensions.Name = "lstExtensions";
            this.lstExtensions.Size = new System.Drawing.Size(274, 160);
            this.lstExtensions.TabIndex = 4;
            this.lstExtensions.SelectedIndexChanged += new System.EventHandler(this.lstExtensions_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.lblPatternOutput);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.txtFolderRenamePattern);
            this.tabPage3.Controls.Add(this.txtFileRenamePattern);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.ImageIndex = 2;
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(426, 296);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Rename";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(176, 97);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(192, 26);
            this.label11.TabIndex = 13;
            this.label11.Text = "{Title},  {Year},  {ImdbId}\r\nBe aware of long path is not supported!";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(85, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Possible values:";
            // 
            // lblPatternOutput
            // 
            this.lblPatternOutput.Location = new System.Drawing.Point(3, 158);
            this.lblPatternOutput.Name = "lblPatternOutput";
            this.lblPatternOutput.Size = new System.Drawing.Size(404, 38);
            this.lblPatternOutput.TabIndex = 11;
            this.lblPatternOutput.Text = "...";
            this.lblPatternOutput.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(189, 145);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Try it:";
            // 
            // txtFolderRenamePattern
            // 
            this.txtFolderRenamePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolderRenamePattern.AutoCompleteCustomSource.AddRange(new string[] {
            "{Title}",
            "{Year}",
            "{ImdbId}"});
            this.txtFolderRenamePattern.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFolderRenamePattern.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFolderRenamePattern.Location = new System.Drawing.Point(179, 61);
            this.txtFolderRenamePattern.Name = "txtFolderRenamePattern";
            this.txtFolderRenamePattern.Size = new System.Drawing.Size(188, 20);
            this.txtFolderRenamePattern.TabIndex = 7;
            // 
            // txtFileRenamePattern
            // 
            this.txtFileRenamePattern.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFileRenamePattern.AutoCompleteCustomSource.AddRange(new string[] {
            "{Title}",
            "{Year}",
            "{ImdbId}"});
            this.txtFileRenamePattern.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFileRenamePattern.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFileRenamePattern.Location = new System.Drawing.Point(179, 30);
            this.txtFileRenamePattern.Name = "txtFileRenamePattern";
            this.txtFileRenamePattern.Size = new System.Drawing.Size(188, 20);
            this.txtFileRenamePattern.TabIndex = 6;
            this.txtFileRenamePattern.TextChanged += new System.EventHandler(this.txtFileRenamePattern_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "File rename pattern:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Folder rename pattern:";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label13);
            this.tabPage4.Controls.Add(this.cmdDetect);
            this.tabPage4.Controls.Add(this.picGuessit);
            this.tabPage4.Controls.Add(this.picPython);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.ImageIndex = 3;
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(426, 296);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Components";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(69, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(296, 13);
            this.label13.TabIndex = 8;
            this.label13.Text = "After installation, reopen Moviebase to take effect!";
            // 
            // cmdDetect
            // 
            this.cmdDetect.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmdDetect.Location = new System.Drawing.Point(174, 28);
            this.cmdDetect.Name = "cmdDetect";
            this.cmdDetect.Size = new System.Drawing.Size(87, 28);
            this.cmdDetect.TabIndex = 7;
            this.cmdDetect.Text = "Detect";
            this.cmdDetect.UseVisualStyleBackColor = true;
            this.cmdDetect.Click += new System.EventHandler(this.cmdDetect_Click);
            // 
            // picGuessit
            // 
            this.picGuessit.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picGuessit.Image = global::Moviebase.Properties.Resources.cross;
            this.picGuessit.Location = new System.Drawing.Point(172, 152);
            this.picGuessit.Name = "picGuessit";
            this.picGuessit.Size = new System.Drawing.Size(16, 16);
            this.picGuessit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picGuessit.TabIndex = 4;
            this.picGuessit.TabStop = false;
            // 
            // picPython
            // 
            this.picPython.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picPython.Image = global::Moviebase.Properties.Resources.cross;
            this.picPython.Location = new System.Drawing.Point(172, 117);
            this.picPython.Name = "picPython";
            this.picPython.Size = new System.Drawing.Size(16, 16);
            this.picPython.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picPython.TabIndex = 3;
            this.picPython.TabStop = false;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "GuessIt";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Python 2.7";
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cmdOk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOk.ForeColor = System.Drawing.Color.White;
            this.cmdOk.Location = new System.Drawing.Point(267, 8);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 1;
            this.cmdOk.Text = "&OK";
            this.cmdOk.UseVisualStyleBackColor = false;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cmdCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(45)))));
            this.cmdCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(60)))));
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.ForeColor = System.Drawing.Color.White;
            this.cmdCancel.Location = new System.Drawing.Point(349, 8);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdOk);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 322);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(434, 39);
            this.panel1.TabIndex = 3;
            // 
            // SettingsView
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(434, 361);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsView_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGuessit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPython)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox lstExtensions;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtFolderRenamePattern;
        private System.Windows.Forms.TextBox txtFileRenamePattern;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkSwapThe;
        private System.Windows.Forms.LinkLabel cmdClearLastDir;
        private System.Windows.Forms.TextBox txtLastOpenDir;
        private System.Windows.Forms.TextBox txtTmdbApiKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdDetect;
        private System.Windows.Forms.PictureBox picGuessit;
        private System.Windows.Forms.PictureBox picPython;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkHidePresist;
        private System.Windows.Forms.CheckBox chkOverwritePoster;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblPatternOutput;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtExt;
        private System.Windows.Forms.Button cmdRemove;
        private System.Windows.Forms.Button cmdAdd;
    }
}