using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class MovieFetchWorker : IMovieFetchWorker, IDisposable
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
        
        public IEnumerable<Task<MovieEntryState>> CreateTasks()
        {
            foreach (var analyzeItem in AnalyzeItems)
            {
                yield return new Task<MovieEntryState>(() =>
                {
                    _log.Info("Processing: " + analyzeItem);
                    TmdbResult newData = null;
                    try
                    {
                        var name = _guessit.RealGuessName(Path.GetFileName(analyzeItem));
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
                        _log.Error(e, "Process error: " + analyzeItem);
                    }

                    newData = newData ?? _tmdb.GetByFilename(Path.GetFileName(analyzeItem));
                    var result = new MovieEntryFacade(newData, analyzeItem);

                    _log.Info("Processed: " + analyzeItem);
                    return new MovieEntryState
                    {
                        Entry = result
                    };
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
                if (AnalyzeItems != null) AnalyzeItems.Clear();
            }

            AnalyzeItems = null;

            _disposedValue = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
