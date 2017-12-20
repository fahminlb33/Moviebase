using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using Moviebase.Entities.Web;
using NLog;

namespace Moviebase.Core.Workers
{
    public class PosterDownloadWorker : WorkerBase, IPosterDownloadWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ITmdb _tmdb;
        private readonly WebClient _wc;

        public List<MovieEntryFacade> MovieEntries { get; set; }
        public string FileName { get; set; }
        public bool OverwritePoster { get; set; }

        public PosterDownloadWorker(ITmdb tmdb)
        {
            _tmdb = tmdb;
            _wc = new WebClient();
        }

        public override void RunWorker()
        {
            Task.Run(() =>
            {
                _log.Debug("Task started.");
                OnRunWorkerStarted(this, EventArgs.Empty);

                try
                {
                    ProcessedWork = 0;
                    TotalWork = MovieEntries.Count;
                    var options = new ParallelOptions
                    {
                        CancellationToken = CancellationToken.Token,
                        MaxDegreeOfParallelism = Commons.MaxDegreeOfParallelism
                    };

                    Parallel.ForEach(MovieEntries, options, InternalRunWorker);

                    _log.Debug("Task finished.");
                    OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(null, null, false));
                }
                catch (Exception e)
                {
                    _log.Error(e, "Task finished with error.");
                    OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(null, e, true));
                }
            });
        }

        protected override void InternalRunWorker(object arg)
        {
            var movieEntry = (MovieEntryFacade)arg;
            _log.Info("Processing: " + movieEntry.Title);

            try
            {
                var url = _tmdb.GetPosterUrl(movieEntry.InternalMovieData.PosterPath, PosterSize.original);

                var destFolder = new PowerPath(movieEntry.FullPath).GetDirectoryPath();
                var destFile = Path.Combine(destFolder, FileName);

                var isExist = File.Exists(destFile);
                if (isExist && OverwritePoster) File.Delete(destFile);
                if (!isExist || OverwritePoster) _wc.DownloadFile(url, destFile);

                _log.Info("Processed: " + movieEntry.Title);
                IncrementWorkDone();
                OnProgressChanged(this, new ProgressChangedEventArgs(GetPercentage(), null));
            }
            catch (Exception e)
            {
                _log.Error(e, "Error processing: " + movieEntry.Title);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _wc?.Dispose();
                MovieEntries?.Clear();
                MovieEntries = null;
            }
            base.Dispose(disposing);
        }
    }
}
