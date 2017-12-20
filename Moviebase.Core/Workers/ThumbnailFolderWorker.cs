using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using NLog;

namespace Moviebase.Core.Workers
{
    public class ThumbnailFolderWorker : WorkerBase, IThumbnailFolderWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IThumbnailFolder _thumbnailFolder;

        public List<string> MovieDirectories { get; set; }
        public string PosterName { get; set; }

        public ThumbnailFolderWorker(IThumbnailFolder thumbnailFolder)
        {
            _thumbnailFolder = thumbnailFolder;
        }

        public override void RunWorker()
        {
            Task.Run(() =>
            {
                _log.Debug("Task started.");
                OnRunWorkerStarted(this, EventArgs.Empty);

                try
                {
                    OnRunWorkerStarted(this, EventArgs.Empty);
                    TotalWork = MovieDirectories.Count;
                    ProcessedWork = 0;

                    var options = new ParallelOptions
                    {
                        CancellationToken = CancellationToken.Token,
                        MaxDegreeOfParallelism = Commons.MaxDegreeOfParallelism
                    };

                    Parallel.ForEach(MovieDirectories, options, InternalRunWorker);

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
            var path = arg.ToString();
            _log.Info("Processing: " + path);

            try
            {
                var posterPath = Path.Combine(path, PosterName + Commons.JpgFileExtension);
                if (!File.Exists(posterPath)) return;

                _thumbnailFolder.GenerateIcon(posterPath);
                _thumbnailFolder.WriteDesktopIni(path);

                _log.Info("Processed: " + path);
                IncrementWorkDone();
                OnProgressChanged(this, new ProgressChangedEventArgs(GetPercentage(), null));
            }
            catch (Exception e)
            {
                _log.Error(e, "Error processing: " + path);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MovieDirectories?.Clear();
                MovieDirectories = null;
            }
            base.Dispose(disposing);
        }
    }
}
