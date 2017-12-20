using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class DirectoryAnalyzeWorker : WorkerBase, IDirectoryAnalyzeWorker
    {
        private readonly ITmdb _tmdb;
        private readonly IPersistentDataManager _persistentDataManager;
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public string AnalyzePath { get; set; }

        public DirectoryAnalyzeWorker(ITmdb tmdb, IPersistentDataManager persistentDataManager)
        {
            _tmdb = tmdb;
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
                    var options = new ParallelOptions
                    {
                        CancellationToken = CancellationToken.Token,
                        MaxDegreeOfParallelism = Commons.MaxDegreeOfParallelism
                    };
                    var dirEnumbEnumerable = Directory.EnumerateDirectories(AnalyzePath, "*", SearchOption.TopDirectoryOnly);
                    
                    Parallel.ForEach(dirEnumbEnumerable, options, InternalRunWorker);

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
            try
            {
                _log.Info("Processing: " + arg);
                var currentFolder = new PowerPath(arg.ToString());

                // check for ignore pattern
                var lastName = currentFolder.GetLastDirectoryName();
                if (lastName.StartsWith("[") && lastName.EndsWith("]"))
                {
                    _log.Debug("Process skipped due to directory name.");
                    return;
                }

                // find first movie
                var currentMoviePath = _persistentDataManager.FindFirstMovieFile(arg.ToString());
                if (currentMoviePath == null)
                {
                    _log.Debug("Process skipped due to unavaliable movie file.");
                    return;
                }

                // find metadata
                TmdbResult entry;
                if (_persistentDataManager.HasPersistentData(currentMoviePath.GetDirectoryPath()))
                {
                    _log.Debug("Using saved presist data.");
                    entry = _persistentDataManager.LoadData(currentMoviePath);
                }
                else
                {
                    _log.Debug("Creating new data using GetByFilename.");
                    entry = _tmdb.GetByFilename(currentMoviePath.GetFileNameWithoutExtension());
                }

                // pop to event
                var result = new MovieEntryFacade(entry, currentMoviePath);
                OnProgressChanged(this, new ProgressChangedEventArgs(-1, new DirectoryAnalyzeWorkerState
                {
                    Entry = result
                }));

                _log.Info("Processed: " + arg);
            }
            catch (Exception e)
            {
                _log.Error(e, "Process error. Path: " + arg);
            }
        }
    }
}
