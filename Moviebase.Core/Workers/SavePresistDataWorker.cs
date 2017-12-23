using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class SavePresistDataWorker : ISavePresistDataWorker, IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IPersistentDataManager _persistentDataManager;

        public List<MovieEntryFacade> SaveItems { get; set; }

        public SavePresistDataWorker(IPersistentDataManager persistentDataManager)
        {
            _persistentDataManager = persistentDataManager;
        }
        
        public IEnumerable<Task> CreateTasks()
        {
            foreach (var entry in SaveItems)
            {
                yield return new Task(() =>
                {
                    _log.Info("Processing: " + entry.Title);

                    try
                    {
                        _persistentDataManager.SaveData(entry.InternalMovieData, Path.GetDirectoryName(entry.FullPath));

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
                if (SaveItems != null) SaveItems.Clear();
            }

            SaveItems = null;

            _disposedValue = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
