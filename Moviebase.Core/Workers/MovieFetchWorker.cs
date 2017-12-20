using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class MovieFetchWorker : WorkerBase, IMovieFetchWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ITmdb _tmdb;
        private readonly IGuessit _guessit;
        
        public List<string> AnalyzeItems { get; set; }

        public MovieFetchWorker(ITmdb tmdb, IGuessit guessit)
        {
            _tmdb = tmdb;
            _guessit = guessit;
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
                    TotalWork = AnalyzeItems.Count;
                    var options = new ParallelOptions
                    {
                        CancellationToken = CancellationToken.Token,
                        MaxDegreeOfParallelism = Commons.MaxDegreeOfParallelism
                    };
                    
                    Parallel.ForEach(AnalyzeItems, options, InternalRunWorker);
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
            _log.Info("Processing: " + arg);
            TmdbResult newData = null;
            try
            {
                var name = _guessit.RealGuessName(Path.GetFileName(arg.ToString()));
                if (name?.ImdbId != null)
                {
                    newData = _tmdb.GetByImdbId(name.ImdbId);
                }
                else if (name?.Title != null)
                {
                    newData = _tmdb.GetByTmdbId(_tmdb.SearchMovies(name.Title, name.Year).First());
                }
            }
            catch (Exception e)
            {
                _log.Error(e, "Process error: " + arg);
            }

            newData = newData ?? _tmdb.GetByFilename(Path.GetFileName(arg.ToString()));
            var result = new MovieEntryFacade(newData, arg.ToString());

            _log.Info("Processed: " + arg);
            IncrementWorkDone();
            OnProgressChanged(this, new ProgressChangedEventArgs(GetPercentage(), new  DirectoryAnalyzeWorkerState
            {
                Entry = result
            }));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AnalyzeItems?.Clear();
                AnalyzeItems = null;
            }
            base.Dispose(disposing);
        }
    }
}
