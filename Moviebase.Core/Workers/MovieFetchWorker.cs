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
    public class MovieFetchWorker : IMovieFetchWorker
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
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
                yield return Task.Run(async () =>
                {
                    Log.Info("Processing: " + analyzeItem);
                    TmdbResult newData = null;
                    try
                    {
                        var name = await _guessit.RealGuessName(Path.GetFileName(analyzeItem));
                        if (name?.ImdbId != null)
                        {
                            newData = await _tmdb.GetByImdbId(name.ImdbId);
                        }
                        else if (name?.Title != null)
                        {
                            var movies = await _tmdb.SearchMovies(name.Title, name.Year);
                            newData = await _tmdb.GetByTmdbId(movies.First());
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error(e, "Process error: " + analyzeItem);
                    }

                    newData = newData ?? await _tmdb.GetByFilename(Path.GetFileName(analyzeItem));
                    var result = new MovieEntry(newData, analyzeItem);

                    Log.Info("Processed: " + analyzeItem);
                    return new MovieEntryState
                    {
                        Entry = result
                    };
                });
            }
        }
    }
}
