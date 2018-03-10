using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Core.MVP;
using Moviebase.Core.Natives;
using Moviebase.Core.Services;
using Moviebase.Entities;
using Ninject;

namespace Moviebase.Presenters
{
    partial class MainPresenter
    {
        private void HelperRenameFile(string path, MovieEntry entry)
        {
            // store original path
            var fileInfo = new PowerPath(path);
            var originalFilePath = fileInfo.GetFullPath();

            // rename path
            var renamedFile = fileInfo.RenameFileByPattern(_settings.FileRenamePattern, entry);
            if (_settings.SwapThe) renamedFile.SwapFileName(Commons.TheName);

            // rename
            if (originalFilePath == renamedFile) return;
            File.Move(originalFilePath, renamedFile);
        }

        private void HelperRenameDirectory(string path, MovieEntry entry)
        {
            // store original path
            var fileInfo = new PowerPath(path);
            var originalPath = fileInfo.GetDirectoryPath();

            // rename path
            var renamedPath = fileInfo.RenameLastDirectoryByPattern(_settings.FolderRenamePattern, entry);
            if (_settings.SwapThe) renamedPath.SwapLastDirectoryName(Commons.TheName);
            var directoryPath = renamedPath.GetDirectoryPath();

            // rename
            if (originalPath == directoryPath) return;
            Directory.Move(originalPath, directoryPath);
        }

        private string HelperFindFirstFile(string dir)
        {
            try
            {
                var searcher = Directory.EnumerateFiles(dir, Commons.AllFilesSearchPattern,
                    SearchOption.TopDirectoryOnly);
                var path = searcher.FirstOrDefault(x => _extensions.Contains(Path.GetExtension(x)));
                return path;
            }
            catch (Exception e)
            {
                Debug.Print("No movie found: {0}. {1}", dir, e.Message);
                return null;
            }
        }

        private async Task<MovieEntry> HelperFetch(string path)
        {
            var tmdb = _kernel.Get<ITmdb>();
            var guessit = _kernel.Get<IGuessit>();
            var filename = Path.GetFileName(path);

            // results
            GuessitResult name = null;
            TmdbResult newData = null;

            // process
            try
            {
                // guess
                name = await guessit.RealGuessName(filename);
                if (name?.ImdbId != null)
                {
                    // IMDB id detected
                    newData = await tmdb.GetByImdbId(name.ImdbId);
                }
                else if (name?.Title != null)
                {
                    // title detected
                    var movies = await tmdb.SearchMovies(name.Title, name.Year);
                    newData = await tmdb.GetByTmdbId(movies.First());
                }
            }
            catch (Exception e)
            {
                Debug.Print("Unable to fetch: {0}. {1}", filename, e.Message);
            }

            // entry create
            var entry = new MovieEntry();
            entry.SetFullPath(path);
            entry.SetData(newData ?? await tmdb.GetByFilename(filename));
            if (name != null) entry.SetData(name);
            return entry;
        }

        // ------ PRESENTER ---
        public override void UpdateUi(UiState state, int progressPercentage = -1)
        {
            switch (state)
            {
                case UiState.Ready:
                    Model.PrgStatusValue = 0;
                    Model.LblPercentageText = "0%";
                    Model.PrgStatusStyle = ProgressBarStyle.Blocks;
                    Model.LblStatusText = "Ready.";

                    Model.CmdDirectoriesEnabled = true;
                    Model.CmdToolsEnabled = true;
                    Model.CmdActionsEnabled = true;
                    Model.CmdStopEnabled = false;
                    Model.GridViewEnabled = true;
                    break;

                case UiState.Working:
                    Model.PrgStatusValue = 0;
                    Model.LblPercentageText = "0%";
                    Model.PrgStatusStyle = ProgressBarStyle.Marquee;
                    Model.LblStatusText = "Working...";

                    Model.CmdDirectoriesEnabled = false;
                    Model.CmdToolsEnabled = false;
                    Model.CmdActionsEnabled = false;
                    Model.CmdStopEnabled = true;
                    Model.GridViewEnabled = false;
                    break;

                case UiState.Cancelling:
                    Model.CmdStopEnabled = false;
                    break;

                case UiState.StatusUpdate:
                    Model.LblStatusText = "Working...";
                    if (progressPercentage == -1)
                    {
                        Model.PrgStatusStyle = ProgressBarStyle.Marquee;
                        Model.PrgStatusValue = 0;
                        Model.LblPercentageText = "Processing...";
                    }
                    else
                    {
                        Model.PrgStatusStyle = ProgressBarStyle.Blocks;
                        Model.PrgStatusValue = progressPercentage;
                        Model.LblPercentageText = $"{progressPercentage}%";
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private async void LoadImage(MovieEntry entry)
        {
            if (entry == null)
            {
                //Model.PicPosterImage?.Dispose();
                Model.PicPosterImage = Commons.DefaultImage();
                return;
            }

            // remove old pict
            Model.PicPosterImage?.Dispose();
            Model.PicPosterImage = Commons.DefaultImage();

            // ReSharper disable once AssignNullToNotNullAttribute
            var posterPath = Path.Combine(Path.GetDirectoryName(entry.FullPath), Commons.PosterFileName);
            var tmdb = _kernel.Get<ITmdb>();
            var deleteAfter = false;

            // download if necessary
            if (!File.Exists(posterPath))
            {
                deleteAfter = true;
                posterPath = Path.GetTempFileName();
                var uri = tmdb.GetPosterUrl(entry.PosterPath, PosterSize.w154);

                await tmdb.DownloadFile(uri, posterPath);
            }

            // load from file
            if (!File.Exists(posterPath)) return;
            using (var stream = new FileStream(posterPath, FileMode.Open))
            {
                Model.PicPosterImage = Image.FromStream(stream);
            }

            // delete temp file
            if (deleteAfter) File.Delete(posterPath);
        }
    }
}
