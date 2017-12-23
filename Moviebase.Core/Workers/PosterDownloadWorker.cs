using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using Moviebase.Entities.Web;
using NLog;

namespace Moviebase.Core.Workers
{
    public class PosterDownloadWorker : IPosterDownloadWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ITmdbWebRequest _tmdbWebRequest;

        public List<MovieEntryFacade> MovieEntries { get; set; }
        public string FileName { get; set; }
        public bool OverwritePoster { get; set; }

        public PosterDownloadWorker(ITmdbWebRequest tmdbWebRequest)
        {
            _tmdbWebRequest = tmdbWebRequest;
        }
        
        public IEnumerable<Task> CreateTasks()
        {
            foreach (var entry in MovieEntries)
            {
                yield return Task.Run(() =>
                {
                    try
                    {
                        _log.Info("Processing: " + entry.Title);
                        var url = _tmdbWebRequest.BuildPosterUrl(entry.InternalMovieData.PosterPath, PosterSize.original);

                        var destFolder = new PowerPath(entry.FullPath).GetDirectoryPath();
                        var destFile = Path.Combine(destFolder, FileName);

                        var isExist = File.Exists(destFile);
                        if (isExist && OverwritePoster) File.Delete(destFile);
                        if (!isExist || OverwritePoster) _tmdbWebRequest.DownloadFile(url, destFile);

                        _log.Info("Processed: " + entry.Title);
                    }
                    catch (Exception e)
                    {
                        _log.Error(e, "Error processing: " + entry.Title);
                    }
                });
            }
        }
    }
}
