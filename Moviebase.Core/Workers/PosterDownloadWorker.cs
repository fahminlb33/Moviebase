using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using Moviebase.Entities.Web;
using NLog;

namespace Moviebase.Core.Workers
{
    public class PosterDownloadWorker : IPosterDownloadWorker, IDisposable
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
        
        public IEnumerable<Task> CreateTasks()
        {
            foreach (var entry in MovieEntries)
            {
                yield return new Task(() =>
                {
                    _log.Info("Processing: " + entry.Title);

                    try
                    {
                        var url = _tmdb.GetPosterUrl(entry.InternalMovieData.PosterPath, PosterSize.original);

                        var destFolder = new PowerPath(entry.FullPath).GetDirectoryPath();
                        var destFile = Path.Combine(destFolder, FileName);

                        var isExist = File.Exists(destFile);
                        if (isExist && OverwritePoster) File.Delete(destFile);
                        if (!isExist || OverwritePoster) _wc.DownloadFile(url, destFile);

                        _log.Info("Processed: " + entry.Title);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e, "Error processing: " + entry.Title);
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
                if (_wc != null) _wc.Dispose();
                if (MovieEntries != null) MovieEntries.Clear();
                    
            }

            MovieEntries = null;

            _disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
