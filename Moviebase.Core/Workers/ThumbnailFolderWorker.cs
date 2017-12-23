using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using NLog;

namespace Moviebase.Core.Workers
{
    public class ThumbnailFolderWorker : IThumbnailFolderWorker, IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IThumbnailFolder _thumbnailFolder;

        public List<string> MovieDirectories { get; set; }
        public string PosterName { get; set; }

        public ThumbnailFolderWorker(IThumbnailFolder thumbnailFolder)
        {
            _thumbnailFolder = thumbnailFolder;
        }

        public IEnumerable<Task> CreateTasks()
        {
            foreach (var dirPath in MovieDirectories)
            {
                yield return new Task(() =>
                {
                    _log.Info("Processing: " + dirPath);

                    try
                    {
                        var posterPath = Path.Combine(dirPath, PosterName + Commons.JpgFileExtension);
                        if (!File.Exists(posterPath)) return;

                        _thumbnailFolder.GenerateIcon(posterPath);
                        _thumbnailFolder.WriteDesktopIni(dirPath);

                        _log.Info("Processed: " + dirPath);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e, "Error processing: " + dirPath);
                    }
                });
            }
        }

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                if (MovieDirectories != null) MovieDirectories.Clear();
            }
            MovieDirectories = null;

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
