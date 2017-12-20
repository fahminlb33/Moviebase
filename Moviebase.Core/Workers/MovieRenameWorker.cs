using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class MovieRenameWorker : WorkerBase, IMovieRenameWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        #region Properties
        public List<MovieEntryFacade> MovieEntries { get; set; }
        public string FileRenamePattern { get; set; }
        public string FolderRenamePattern { get; set; }
        public bool SwapThe { get; set; }
        #endregion


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
            var entry = (MovieEntryFacade)arg;

            try
            {
                _log.Info("Processing: " + entry.Title);
                using (var fileInfo = new PowerPath(entry.FullPath))
                {
                    RenameFile(fileInfo, entry);
                    RenameDirectory(fileInfo, entry);
                }

                _log.Info("Processed: " + entry.Title);
                IncrementWorkDone();
                OnProgressChanged(this, new ProgressChangedEventArgs(GetPercentage(), null));
            }
            catch (Exception e)
            {
                _log.Error(e, "Error processing: " + entry.Title);
            }
        }

        private void RenameFile(PowerPath fileInfo, MovieEntryFacade entry)
        {
            var originalFilePath = fileInfo.GetFullPath();
            var renamedFile = fileInfo.RenameFileByPattern(FileRenamePattern, entry);

            if (SwapThe) renamedFile.SwapFileName(Commons.TheName);
            var name = renamedFile.GetFileName();
                    
            var destFilePath = Path.Combine(fileInfo.GetDirectoryPath(), name);

            if (originalFilePath == destFilePath) return;
            File.Move(originalFilePath, destFilePath);
            entry.FullPath = destFilePath;
        }

        private void RenameDirectory(PowerPath fileInfo, MovieEntryFacade entry)
        {
            var originalFilePath = fileInfo.GetDirectoryPath();
            var renamedPath = fileInfo.RenameLastDirectoryByPattern(FolderRenamePattern, entry);

            if (SwapThe) renamedPath.SwapLastDirectoryName(Commons.TheName);
            var directoryPath = renamedPath.GetDirectoryPath();

            if (originalFilePath == directoryPath) return;
            Directory.Move(originalFilePath, directoryPath);

            var name = Path.GetFileName(entry.FullPath);
            Debug.Assert(name != null);
            entry.FullPath = Path.Combine(directoryPath, name);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                MovieEntries?.Clear();
                MovieEntries = null;
            }
            base.Dispose(disposing);
        }
    }
}
