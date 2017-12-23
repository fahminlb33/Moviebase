using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class DirectoryAnalyzeWorker : IDirectoryAnalyzeWorker
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

        public IEnumerable<Task<MovieEntryState>> CreateTasks()
        {
            var dirEnumbEnumerable = Directory.EnumerateDirectories(AnalyzePath, "*", SearchOption.TopDirectoryOnly);
            foreach (var dirPath in dirEnumbEnumerable)
            {
                yield return Task.Run(async () =>
                {
                    _log.Info("Processing: " + dirPath);
                    var currentFolder = new PowerPath(dirPath);

                    // check for ignore pattern
                    var lastName = currentFolder.GetLastDirectoryName();
                    if (lastName.StartsWith("[") && lastName.EndsWith("]"))
                    {
                        _log.Debug("Process skipped due to directory name.");
                        return null;
                    }

                    // find first movie
                    var currentMoviePath = _persistentDataManager.FindFirstMovieFile(dirPath);
                    if (currentMoviePath == null)
                    {
                        _log.Debug("Process skipped due to unavaliable movie file.");
                        return null;
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
                        entry = await _tmdb.GetByFilename(currentMoviePath.GetFileNameWithoutExtension());
                    }

                    // pop to event
                    var result = new MovieEntryFacade(entry, currentMoviePath);
                    return new MovieEntryState
                    {
                        Entry = result
                    };
                });
            }
        }
    }
}