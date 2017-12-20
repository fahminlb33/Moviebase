using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using System.Windows.Forms;
using BlastMVP;
using NLog;

namespace Moviebase.Core.Workers
{
    public class ResearchMovieWorker : WorkerBase, IResearchMovieWorker
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

        public override void RunWorker()
        {
            Task.Run(() => InternalRunWorker(null));
        }

        protected override void InternalRunWorker(object arg)
        {
            _log.Debug("Task started.");
            OnRunWorkerStarted(this, EventArgs.Empty);

            try
            {
                OnProgressChanged(this, new ProgressChangedEventArgs(-1, null));
                var name = _guessit.RealGuessName(Path.GetFileName(FullPath));
                var found = _tmdb.SearchMovies(name.Title, 0);

                var movieTitles = found.Select(movieId => _tmdb.GetByTmdbId(movieId))
                    .ToDictionary(result => result.Id.ToString());
                var movieTitleSelection = movieTitles.Values.Select(x => $"{x.Id}:   {x.Title} ({x.Year})").ToArray();

                var choose = View.ShowComboBoxInput("Select alternative.", movieTitleSelection, out string choosenName);
                if (choose != DialogResult.OK) goto finish;

                foreach (var movieTitle in movieTitles)
                {
                    if (movieTitle.Value.Id.ToString() == choosenName.Split(':')[0])
                    {
                        OnProgressChanged(this, new ProgressChangedEventArgs(-1, new ResearchMovieWorkerState
                        {
                            Entry = new MovieEntryFacade(movieTitle.Value, FullPath),
                            Index = Index
                        }));
                    }
                }
                
                finish:
                _log.Debug("Task finished.");
                OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(null, null, false));
            }
            catch (Exception e)
            {
                _log.Error(e, "Task finished with error.");
                OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(null, e, true));
            }
        }
    }
}
