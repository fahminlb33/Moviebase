using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Moviebase.Core.MVP;
using Moviebase.Core.Natives;
using Moviebase.Core.Services;
using Moviebase.Entities;
using Ninject;

namespace Moviebase.Presenters
{
    partial class MainPresenter
    {
        private async void InternalOpenDirectory(string path)
        {
			// save states
			UpdateUi(UiState.Working);
            _settings.LastOpenDirectory = path;
            _settings.Save();

			// clear data
            Model.Invoke(() => Model.DataView.Clear());

			// enumerate
            var tmdb = _kernel.Get<ITmdb>();
            var persistFileManager = _kernel.Get<IPersistFileManager>();
            var dirEnumbEnumerable = Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly).ToList();
            var calc = new PercentageCalculator(dirEnumbEnumerable.Count);

			// walk
            foreach (var basePath in dirEnumbEnumerable)
            {
                try
                {
                    string currentMoviePath;
                    var currentFolder = new PowerPath(basePath);

                    // check for ignore pattern
                    var lastName = currentFolder.GetLastDirectoryName();
                    if (lastName.StartsWith("[") && lastName.EndsWith("]")) continue;

                    // find first movie
                    if ((currentMoviePath = HelperFindFirstFile(basePath)) == null) continue;

                    // find metadata
                    MovieEntry entry;
                    if (persistFileManager.HasPersistentData(Path.GetDirectoryName(currentMoviePath)))
                    {
                        entry = persistFileManager.Load(currentMoviePath);
                    }
                    else
                    {
                        var currentResult = await tmdb.GetByFilename(currentMoviePath);
                        entry = new MovieEntry();
                        entry.SetFullPath(currentMoviePath);
                        entry.SetData(currentResult);
                    }

                    // push to collection
                    Model.Invoke(() => Model.DataView.Add(entry));
                }
                catch (Exception e)
                {
                    Debug.Print("Analyze error: {0}. {1}", basePath, e.Message);
                }

				// sync
                _lastSelectedIndex = -1;
                UpdateUi(UiState.StatusUpdate, calc.Increment());
                if (IsCancellationRequested) break;
            }

            // finish
            UpdateUi(UiState.Ready);
        }

        private void InternalRenameMovies()
        {
			// enumerate
            UpdateUi(UiState.Working);
            var entries = Model.DataView.Where(x => Commons.IsMovieFetched(x.TmdbId) &&
                                                    !string.IsNullOrWhiteSpace(x.Title)).ToList();
            var calc = new PercentageCalculator(entries.Count);
            int unprocessed = 0;

			// walk
            foreach (var entry in entries)
            {
                try
                {
                    var path = entry.FullPath;
                    HelperRenameFile(path, entry);
                    HelperRenameDirectory(path, entry);
                }
                catch (Exception e)
                {
                    Debug.Print("Rename error: {0}. {1}", entry.Title, e.Message);
                    unprocessed++;
                }

                // sync
                UpdateUi(UiState.StatusUpdate, calc.Increment());
                if (IsCancellationRequested) break;
            }

			// finished
            UpdateUi(UiState.Ready);
            Model.Invoke(() =>
            {
                Model.DataView.Clear();
                View.ShowMessageBox("Rename completed. Please re-analyze the folder. Errors: " + unprocessed, Strings.AppName);
            });
        }

        private async void InternalFetch()
        {
			// enumerate
            UpdateUi(UiState.Working);
            var paths = Model.DataView.Where(x => !Commons.IsMovieFetched(x.TmdbId))
                .Select(x => x.FullPath)
                .ToList();
            var calc = new PercentageCalculator(paths.Count);

			// walk
            foreach (var path in paths)
            {
                try
                {
                    var data = await HelperFetch(path);
                    Model.Invoke(() => Model.DataView.SwapItem(x => x.FullPath == path, data));
                }
                catch (Exception e)
                {
                    Debug.Print("Fetch error: {0}. {1}", path, e.Message);
                }

                UpdateUi(UiState.StatusUpdate, calc.Increment());
                if (IsCancellationRequested) break;
            }

			// update
            UpdateUi(UiState.Ready);
        }

        private async void InternalDownloadPoster()
        {
			// enumerate
            UpdateUi(UiState.Working);
            var tmdb = _kernel.Get<ITmdb>();
            var entries = Model.DataView.Where(x => x.PosterPath != null).ToList();
            var calc = new PercentageCalculator(entries.Count);

			// walk
            foreach (var entry in entries)
            {
                try
                {
                    var url = tmdb.BuildPosterUrl(entry.PosterPath, PosterSize.original);
                    // ReSharper disable once AssignNullToNotNullAttribute
                    var destFile = Path.Combine(Path.GetDirectoryName(entry.FullPath), Commons.PosterFileName);

                    if (File.Exists(destFile)) continue;
                    await tmdb.DownloadFile(url, destFile);
                }
                catch (Exception e)
                {
                    Debug.Print("Download poster error: {0}. {1}", entry.Title, e.Message);
                }

                UpdateUi(UiState.StatusUpdate, calc.Increment());
                if (IsCancellationRequested) break;
            }

			// finish
            UpdateUi(UiState.Ready);
        }

        private void InternalThumbnailFolder()
        {
			// enumerate
            UpdateUi(UiState.Working);
            var entries = Model.DataView.Where(x => x.PosterPath != null).ToList();
            var calc = new PercentageCalculator(entries.Count);
            var thumbnailManager = _kernel.Get<IThumbnailManager>();

			// walk
            foreach (var entry in entries)
            {
                thumbnailManager.CreateThumbnail(Path.GetDirectoryName(entry.FullPath));
                UpdateUi(UiState.StatusUpdate, calc.Increment());
                if (IsCancellationRequested) break;
            }

            // finish
            UpdateUi(UiState.Ready);
        }

        private void InternalSavePresistData()
        {
			// enumerate
            UpdateUi(UiState.Working);
            var persistFileManager = _kernel.Get<IPersistFileManager>();
            var calc = new PercentageCalculator(Model.DataView.Count);

			// walk
            foreach (var entry in Model.DataView)
            {
                try
                {
                    persistFileManager.Save(Path.GetDirectoryName(entry.FullPath), entry);
                }
                catch (Exception e)
                {
                    Debug.Print("Save error: {0}. {1}", entry.Title, e.Message);
                }

                UpdateUi(UiState.StatusUpdate, calc.Increment());
                if (IsCancellationRequested) break;
            }

            // finish
            UpdateUi(UiState.Ready);
        }
    }
}
