using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class SavePresistDataWorker : ISavePresistDataWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly IPersistentDataManager _persistentDataManager;

        public List<MovieEntry> SaveItems { get; set; }

        public SavePresistDataWorker(IPersistentDataManager persistentDataManager)
        {
            _persistentDataManager = persistentDataManager;
        }
        
        public IEnumerable<Task> CreateTasks()
        {
            foreach (var entry in SaveItems)
            {
                yield return Task.Run(() =>
                {
                    try
                    {
                        _log.Info("Processing: " + entry.Title);
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
    }
}
