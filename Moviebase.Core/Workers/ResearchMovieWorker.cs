using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using System.Windows.Forms;
using BlastMVP;
using NLog;
using System.Collections.Generic;

namespace Moviebase.Core.Workers
{
    public class ResearchMovieWorker : IResearchMovieWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ITmdb _tmdb;
        private readonly IGuessit _guessit;

        public int Index { get; set; }
        public string FullPath { get; set; }
        public Form View { get; set; }

        public ResearchMovieWorker(IGuessit guessit, ITmdb tmdb)
        {
            _tmdb = tmdb;
            _guessit = guessit;
        }

        public IEnumerable<Task<ResearchMovieEntryState>> CreateTasks()
        {
            yield return new Task<ResearchMovieEntryState>(() =>
            {
                _log.Debug("Task started.");

                try
                {
                    var name = _guessit.RealGuessName(Path.GetFileName(FullPath));
                    var found = _tmdb.SearchMovies(name.Title, 0);

                    var movieTitles = found.Select(movieId => _tmdb.GetByTmdbId(movieId))
                        .ToDictionary(result => result.Id.ToString());
                    var movieTitleSelection = movieTitles.Values.Select(x => $"{x.Id}:   {x.Title} ({x.Year})").ToArray();

                    var choose = View.ShowComboBoxInput("Select alternative.", movieTitleSelection, out string choosenName);
                    if (choose != DialogResult.OK) return null;

                    return (from movieTitle in movieTitles
                        where movieTitle.Value.Id.ToString() == choosenName.Split(':')[0]
                        select new ResearchMovieEntryState
                        {
                            Entry = new MovieEntryFacade(movieTitle.Value, FullPath),
                            Index = Index
                        }).FirstOrDefault();
                }
                catch (Exception e)
                {
                    _log.Error(e, "Task finished with error.");
                    return null;
                }
            });
        }
    }
}
