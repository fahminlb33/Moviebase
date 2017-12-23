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
        private ITmdbWebRequest _tmdbWebRequest;
        private ITmdb _tmdb;
        private SynchronizationContext _synchronizationContext;

        public string SelectedPath { get; set; }

        public SelectPosterView(string id)
        {
            InitializeComponent();

            _tmdbWebRequest = Program.AppKernel.Get<ITmdbWebRequest>();
            _tmdb = Program.AppKernel.Get<ITmdb>();
            _synchronizationContext = SynchronizationContext.Current;

            Task.Run(() => FindPoster(id));
        }

        private async void FindPoster(string id)
        {
            _tempDir = Path.Combine(Path.GetTempPath(), Commons.TempFolderName, DateTime.Now.Ticks.ToString());
            Directory.CreateDirectory(_tempDir);

            var posterUris = await _tmdb.GetPosterUris(id);
            var total = posterUris.Count;
            var processed = 0;

            for (var i = 0; i < total; i++)
            {
                var currentPath = _tmdbWebRequest.BuildPosterUrl(posterUris[i], PosterSize.w154);
                var currentSavePath = Path.Combine(_tempDir, posterUris[i].Remove(0, 1));
                await _tmdbWebRequest.DownloadFile(currentPath, currentSavePath);

                ++processed;
                var percent = (int) (processed / (double) total * 100);
                _synchronizationContext.Post(x =>
                {
                    lblStatus.Text = string.Format(StringResources.PercentagePattern, percent);
                    prgDownload.Value = percent;
                }, null);
            }
            _synchronizationContext.Post(x => lblStatus.Text = StringResources.CompletedText, null);

            var files = Directory.GetFiles(_tempDir, Commons.JpgSearchPattern, SearchOption.TopDirectoryOnly);
            _synchronizationContext.Post(x => lvPosters.Items.AddRange((string[]) x), files);
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
