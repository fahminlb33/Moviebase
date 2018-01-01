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
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
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
                        Log.Info("Processing: " + entry.Title);
                        _persistentDataManager.SaveData(entry.InternalMovieData, Path.GetDirectoryName(entry.FullPath));

                        Log.Info("Processed: " + entry.Title);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, "Error processing: " + entry.Title);
                    }
                });
            }
        }
    }
}
