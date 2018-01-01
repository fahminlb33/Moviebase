using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using NLog;

namespace Moviebase.Core.Workers
{
    public class ThumbnailFolderWorker : IThumbnailFolderWorker
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
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
                yield return Task.Run(() =>
                {
                    try
                    {
                        Log.Info("Processing: " + dirPath);
                        var posterPath = Path.Combine(dirPath, PosterName + Commons.JpgFileExtension);
                        if (!File.Exists(posterPath)) return;

                        _thumbnailFolder.GenerateIcon(posterPath);
                        _thumbnailFolder.WriteDesktopIni(dirPath);

                        Log.Info("Processed: " + dirPath);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, "Error processing: " + dirPath);
                    }
                });
            }
        }
    }
}
