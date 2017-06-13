using Manina.Windows.Forms;

namespace Moviebase.Views
{
    partial class PosterSelectorView
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
            this.lvPosters = new Manina.Windows.Forms.ImageListView();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.prgProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lvPosters
            // 
            this.lvPosters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvPosters.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lvPosters.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lvPosters.Location = new System.Drawing.Point(12, 12);
            this.lvPosters.MultiSelect = false;
            this.lvPosters.Name = "lvPosters";
            this.lvPosters.PersistentCacheDirectory = "";
            this.lvPosters.PersistentCacheSize = ((long)(100));
            this.lvPosters.ShowCheckBoxes = true;
            this.lvPosters.Size = new System.Drawing.Size(445, 271);
            this.lvPosters.TabIndex = 0;
            this.lvPosters.ItemCheckBoxClick += new Manina.Windows.Forms.ItemCheckBoxClickEventHandler(this.lvPosters_ItemCheckBoxClick);
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.Location = new System.Drawing.Point(301, 298);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 1;
            this.cmdOk.Text = "OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Location = new System.Drawing.Point(382, 298);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 2;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // prgProgress
            // 
            this.prgProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prgProgress.Location = new System.Drawing.Point(12, 298);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Size = new System.Drawing.Size(157, 23);
            this.prgProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prgProgress.TabIndex = 3;
            // 
            // PosterSelectorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 335);
            this.Controls.Add(this.prgProgress);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.lvPosters);
            this.Name = "PosterSelectorView";
            this.Text = "PosterSelectorView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PosterSelectorView_FormClosing);
            this.Load += new System.EventHandler(this.PosterSelectorView_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageListView lvPosters;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.ProgressBar prgProgress;
    }
}