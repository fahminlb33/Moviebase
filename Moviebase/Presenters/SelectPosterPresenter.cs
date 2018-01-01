using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Moviebase.Core;
using Moviebase.Entities;
using Moviebase.Entities.Web;
using Moviebase.Models;
using Moviebase.Views;
using Ninject;

namespace Moviebase.Presenters
{
    class SelectPosterPresenter
    {
        private string _tempDir;
        private readonly ITmdbWebRequest _tmdbWebRequest;
        private readonly ITmdb _tmdb;

        public Action<string[]> FindFinishedCallback;
        public SelectPosterView View { get; }
        public SelectPosterModel Model { get; }

        public SelectPosterPresenter(SelectPosterView view)
        {
            View = view;
            Model = new SelectPosterModel(SynchronizationContext.Current);

            _tmdbWebRequest = Program.AppKernel.Get<ITmdbWebRequest>();
            _tmdb = Program.AppKernel.Get<ITmdb>();
        }

        public void FindPoster(string id)
        {
            Task.Run(async () => await InternalFindPoster(id));
        }

        public void FormClosing()
        {
            Commons.RunNoException(() => Directory.Delete(_tempDir, true));
        }

        private async Task InternalFindPoster(string id)
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

                Model.LblStatusText = string.Format(StringResources.PercentagePattern, percent);
                Model.PrgStatusValue = percent;
            }
            Model.LblStatusText = StringResources.LiteralCompletedText;

            var files = Directory.GetFiles(_tempDir, Commons.JpgSearchPattern, SearchOption.TopDirectoryOnly);
            Model.Invoke(() => FindFinishedCallback.Invoke(files));
        }
    }
}
