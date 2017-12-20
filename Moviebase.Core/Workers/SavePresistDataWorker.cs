using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class SavePresistDataWorker : WorkerBase, ISavePresistDataWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IPersistentDataManager _persistentDataManager;

        public List<MovieEntryFacade> SaveItems { get; set; }

        public SavePresistDataWorker(IPersistentDataManager persistentDataManager)
        {
            _persistentDataManager = persistentDataManager;
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
                    TotalWork = SaveItems.Count;
                    var options = new ParallelOptions
                    {
                        CancellationToken = CancellationToken.Token,
                        MaxDegreeOfParallelism = Commons.MaxDegreeOfParallelism
                    };
                    
                    Parallel.ForEach(SaveItems, options, InternalRunWorker);

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
            _log.Info("Processing: " + entry.Title);

            try
            {
                _persistentDataManager.SaveData(entry.InternalMovieData, Path.GetDirectoryName(entry.FullPath));

                _log.Info("Processed: " + entry.Title);
                IncrementWorkDone();
                OnProgressChanged(this, new ProgressChangedEventArgs(GetPercentage(), null));
            }
            catch (Exception e)
            {
                _log.Error(e, "Error processing: " + entry.Title);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                SaveItems?.Clear();
                SaveItems = null;
            }
            base.Dispose(disposing);
        }
    }
}
