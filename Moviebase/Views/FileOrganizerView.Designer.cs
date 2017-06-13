namespace Moviebase.Views
{
    partial class FileOrganizerView
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRoot = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.lblTotalSize = new System.Windows.Forms.Label();
            this.cmdMove = new System.Windows.Forms.Button();
            this.dialogFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Root directory";
            // 
            // txtRoot
            // 
            this.txtRoot.Location = new System.Drawing.Point(102, 12);
            this.txtRoot.Name = "txtRoot";
            this.txtRoot.ReadOnly = true;
            this.txtRoot.Size = new System.Drawing.Size(291, 20);
            this.txtRoot.TabIndex = 1;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.AutoSize = true;
            this.cmdBrowse.Location = new System.Drawing.Point(399, 15);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(42, 13);
            this.cmdBrowse.TabIndex = 2;
            this.cmdBrowse.TabStop = true;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdBrowse_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "File count";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Total size";
            // 
            // lblFileCount
            // 
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Location = new System.Drawing.Point(230, 46);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(13, 13);
            this.lblFileCount.TabIndex = 6;
            this.lblFileCount.Text = "0";
            // 
            // lblTotalSize
            // 
            this.lblTotalSize.AutoSize = true;
            this.lblTotalSize.Location = new System.Drawing.Point(230, 63);
            this.lblTotalSize.Name = "lblTotalSize";
            this.lblTotalSize.Size = new System.Drawing.Size(32, 13);
            this.lblTotalSize.TabIndex = 7;
            this.lblTotalSize.Text = "0 MB";
            // 
            // cmdMove
            // 
            this.cmdMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMove.Location = new System.Drawing.Point(112, 96);
            this.cmdMove.Name = "cmdMove";
            this.cmdMove.Size = new System.Drawing.Size(221, 37);
            this.cmdMove.TabIndex = 9;
            this.cmdMove.Text = "Move";
            this.cmdMove.UseVisualStyleBackColor = true;
            this.cmdMove.Click += new System.EventHandler(this.cmdMove_Click);
            // 
            // FileOrganizerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 156);
            this.Controls.Add(this.cmdMove);
            this.Controls.Add(this.lblTotalSize);
            this.Controls.Add(this.lblFileCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.txtRoot);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileOrganizerView";
            this.Text = "Movie Organizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRoot;
        private System.Windows.Forms.LinkLabel cmdBrowse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Label lblTotalSize;
        private System.Windows.Forms.Button cmdMove;
        private System.Windows.Forms.FolderBrowserDialog dialogFolder;
    }
}