using System;
using System.IO;
using System.Windows.Forms;
using BlastMVP;
using Moviebase.Entities;
using Moviebase.Presenters;

namespace Moviebase.Views
{
    public partial class SelectPosterView : Form
    {
        private SelectPosterPresenter _presenter;

        public string SelectedPath { get; set; }

        public SelectPosterView(string id)
        {
            InitializeComponent();
            _presenter = new SelectPosterPresenter(this) {FindFinishedCallback = FindFinishedCallback};
            GlueBindings();

            _presenter.FindPoster(id);
        }

        private void FindFinishedCallback(string[] filePaths)
        {
            lvPosters.Items.AddRange(filePaths);
        }

        private void GlueBindings()
        {
            var model = _presenter.Model;

            lblStatus.Bind(c => c.Text).To(model, m => m.LblStatusText);
            prgDownload.Bind(c => c.Value).To(model, m => m.PrgStatusValue);
        }
        
        private void SelectPosterView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _presenter.FormClosing();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (picPoster.Image == null)
            {
                this.ShowMessageBox(StringResources.PosterNotSelectedMessage, StringResources.AppName);
                return;
            }

            SelectedPath = "/" + Path.GetFileName(picPoster.ImageLocation);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvPosters_ItemClick(object sender, Manina.Windows.Forms.ItemClickEventArgs e)
        {
            picPoster.ImageLocation = e.Item.FileName;
        }
    }
}
