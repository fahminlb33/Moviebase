using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlastMVP;
using Moviebase.Core;
using Moviebase.Entities;
using Moviebase.Entities.Web;
using Ninject;

namespace Moviebase.Views
{
    public partial class SelectPosterView : Form
    {
        private string _tempDir;

        public string SelectedPath { get; set; }

        public SelectPosterView(string id)
        {
            InitializeComponent();
            FindPoster(id);
        }

        private void FindPoster(string id)
        {
            var wc = new WebClient();
            var tmdb = Program.AppKernel.Get<ITmdb>();
            var synchronizationContext = SynchronizationContext.Current;
            _tempDir = Path.Combine(Path.GetTempPath(), Commons.TempFolderName, DateTime.Now.Ticks.ToString());
            Directory.CreateDirectory(_tempDir);

            Task.Run(() =>
            {
                var posterUris = tmdb.GetPosterUris(id);
                var total = posterUris.Length;
                var processed = 0;

                for (var i = 0; i < total; i++)
                {
                    var currentPath = tmdb.GetPosterUrl(posterUris[i], PosterSize.w154);
                    var currentSavePath = Path.Combine(_tempDir, posterUris[i].Remove(0, 1));
                    wc.DownloadFile(currentPath, currentSavePath);

                    ++processed;
                    var percent = (int)(processed / (double)total * 100);
                    synchronizationContext.Post(x =>
                    {
                        lblStatus.Text = string.Format(StringResources.PercentagePattern, percent);
                        prgDownload.Value = percent;
                    }, null);
                }
                synchronizationContext.Post(x => lblStatus.Text = StringResources.CompletedText, null);

                var files = Directory.GetFiles(_tempDir, Commons.JpgSearchPattern, SearchOption.TopDirectoryOnly);
                synchronizationContext.Post(x => lvPosters.Items.AddRange((string[])x), files);
            });
        }

        private void SelectPosterView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Directory.Delete(_tempDir, true);
            }
            catch
            {
              // ignore   
            }
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
