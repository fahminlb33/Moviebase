using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Properties;
using Moviebase.Views;
using Moviebase.Entities;
using Moviebase.Core;
using Moviebase.Core.Diagnostics;
using Moviebase.Core.MVP;
using Moviebase.Core.Services;
using Moviebase.Models;
using Ninject;
using Ninject.Parameters;

namespace Moviebase.Presenters
{
    class MainPresenter
    {
        private readonly StandardKernel _kernel = Program.AppKernel;
        private readonly Settings _settings = Settings.Default;
        private readonly FolderBrowserDialog _folderBrowserDialog;
        private CancellationTokenSource _cancellationToken;
        private int _lastSelectedIndex;

        public enum UiState
        {
            Ready,
            Working,
            Cancelling,
            StatusUpdate,
        }

        public MainModel Model { get; }
        public MainView View { get; }

        public MainPresenter(MainView view)
        {
            View = view;
            Model = _kernel.Get<MainModel>(new ConstructorArgument("context", SynchronizationContext.Current));

            CheckComponents();
            _folderBrowserDialog = new FolderBrowserDialog
            {
                Description = StringResources.BrowseFolderDescription,
                ShowNewFolderButton = false
            };
        }
        
        #region Event Handlers

        // -------- STOP
        public void StopProcess()
        {
            _cancellationToken?.Cancel();
            UpdateUi(UiState.Cancelling);
        }

        // -------- WINDOWS
        public void ShowSettingsWindow()
        {
            using (var vw = new SettingsView()) vw.ShowDialog();
        }

        public void ShowMoveMoviesWindow()
        {
            using (var vw = new MoveMoviesView()) vw.ShowDialog();
        }

        public void ShowAboutView()
        {
            using (var vw = new AboutView()) vw.ShowDialog();
        }

        // -------- ACTIONS
        public void OpenLastDirectory()
        {
            var path = _settings.LastOpenDirectory;
            var validation = CreateValidationSupport()
                .IsTrue(() => Model.DataView.Count == 0, StringResources.AlreadyOpenedFolderMessage)
                .IsTrue(() => Directory.Exists(path), StringResources.OpenDirNoRecord)
                .Validate();
            if (!validation) return;

            InternalAnalyzeDirectory(path);
        }

        public void OpenDirectory()
        {
            var validation = CreateValidationSupport()
                .IsTrue(() => Model.DataView.Count == 0, StringResources.AlreadyOpenedFolderMessage)
                .Validate();
            if (!validation) return;

            if (_folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            _settings.LastOpenDirectory = _folderBrowserDialog.SelectedPath;
            _settings.Save();
            InternalAnalyzeDirectory(_folderBrowserDialog.SelectedPath);
        }

        public void CloseFolder()
        {
            Model.DataView.Clear();
            ResetDetails();
        }

        public async Task ExportCsv()
        {
            var result = CreateValidationSupport()
                .IsTrue(() => Model.DataView.Count > 0, StringResources.ExportNoDataMessage)
                .Validate();
            if (!result) return;

            UpdateUi(UiState.Working);
            await Task.Run(() =>
            {
                CsvExporter.ExportCsv(Model.DataView, Path.Combine(Commons.DesktopPath, Commons.ExportFileName));
            });
            View.ShowMessageBox("Export success.", StringResources.AppName);
            UpdateUi(UiState.Ready);
        }

        public void RenameMovieFiles()
        {
            var result = CreateValidationSupport()
                .IsTrue(() => Model.DataView.Count > 0, StringResources.RenameNoDataMessage)
                .Validate();
            if (!result) return;

            RecreateCancellationToken();
            Task.Run(() =>
            {
                var count = 0;
                foreach (var entry in Model.DataView.Where(x => Commons.IsMovieFetched(x.TmdbId) && !string.IsNullOrWhiteSpace(x.Title)))
                {
                    var token = _cancellationToken.Token;
                    if (token.IsCancellationRequested) UpdateUi(UiState.Ready);
                    if (token.IsCancellationRequested) break;

                    try
                    {
                        var path = entry.FullPath;
                        InternalRenameFile(path, entry);
                        InternalRenameDirectory(path, entry);
                        count++;
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Rename error: {0}. {1}", entry.Title, e.Message);
                    }
                }

                View.ShowMessageBox(string.Format(StringResources.RenameFinishedMessage, count), StringResources.AppName);
                Model.DataView.Clear();
            }, _cancellationToken.Token);
        }

        public void FetchMovieData()
        {
            var result = CreateValidationSupport()
                .IsTrue(() => Model.DataView.Count > 0, StringResources.FetchNoDataMessage)
                .EnsureInternetConnected()
                .Validate();
            if (!result) return;

            RecreateCancellationToken();
            Task.Run(async () =>
            {
                foreach (var path in Model.DataView.Where(x => !Commons.IsMovieFetched(x.Data.Id)).Select(x => x.FullPath))
                {
                    var token = _cancellationToken.Token;
                    token.ThrowIfCancellationRequested();

                    try
                    {
                        Model.DataView.SwapItem(x => x.FullPath == path, await InternalFetch(path));
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Fetch error: {0}. {1}", path, e.Message);
                    }
                }
            }, _cancellationToken.Token);
        }

        public void DownloadMoviePoster()
        {
            var result = CreateValidationSupport()
                .IsTrue(() => Model.DataView.Count > 0, StringResources.PosterNoDataMessage)
                .EnsureInternetConnected()
                .Validate();
            if (!result) return;
            
            RecreateCancellationToken();
            Task.Run(async () =>
            {
                var tmdb = _kernel.Get<ITmdb>();
                foreach (var entry in Model.DataView.Where(x => x.Data.PosterPath != null))
                {
                    var token = _cancellationToken.Token;
                    if (token.IsCancellationRequested) break;

                    try
                    {
                        var url = tmdb.BuildPosterUrl(entry.Data.PosterPath, PosterSize.original);

                        var destFolder = Path.GetDirectoryName(entry.FullPath);
                        Debug.Assert(destFolder != null);
                        var destFile = Path.Combine(destFolder, Commons.PosterFileName);

                        if (File.Exists(destFile)) continue;
                        await tmdb.DownloadFile(url, destFile);
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Download poster error: {0}. {1}", entry.Title, e.Message);
                    }
                }
            }, _cancellationToken.Token);
        }

        public void ThumbnailFolder()
        {
            var result = CreateValidationSupport()
                .IsTrue(() => Model.DataView.Count > 0, StringResources.ThumbnailNoDataMessage)
                .Validate();
            if (!result) return;

            RecreateCancellationToken();
            Task.Run(() =>
            {
                var thumbnailManager = _kernel.Get<IThumbnailManager>();
                foreach (var entry in Model.DataView.Where(x => x.Data.PosterPath != null))
                {
                    var token = _cancellationToken.Token;
                    if (token.IsCancellationRequested) break;

                    thumbnailManager.CreateThumbnail(Path.GetDirectoryName(entry.FullPath));
                }
            }, _cancellationToken.Token);
        }

        public void SavePresistData()
        {
            var result = new ValidationSupport()
                .IsTrue(() => Model.DataView.Count > 0, StringResources.PresistNoDataMessage)
                .SetCommonFailAction(x => View.ShowMessageBox(x, StringResources.AppName, icon: MessageBoxIcon.Exclamation))
                .Validate();
            if (!result) return;

            RecreateCancellationToken();
            Task.Run(() =>
            {
                var persistFileManager = _kernel.Get<IPersistFileManager>();
                foreach (var entry in Model.DataView)
                {
                    var token = _cancellationToken.Token;
                    token.ThrowIfCancellationRequested();

                    try
                    {
                        persistFileManager.Save(Path.GetDirectoryName(entry.FullPath), entry.Data);
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Save error: {0}. {1}", entry.Title, e.Message);
                    }
                }
            }, _cancellationToken.Token);
        }

        public void ShowSelectPosterWindow(int index)
        {
            var localEntry = Model.DataView[index];
            var result = CreateValidationSupport()
                .IsTrue(() => Commons.IsMovieFetched(localEntry.Data.Id), StringResources.NotFetchedMessage)
                .EnsureInternetConnected()
                .Validate();
            if (!result) return;

            using (var vw = new SelectPosterView(localEntry.Data.Id.ToString()))
            {
                if (vw.ShowDialog() != DialogResult.OK) return;

                localEntry.Data.PosterPath = vw.SelectedPath;
                _lastSelectedIndex = -1;
            }
        }

        public void ShowAlternativeNameWindow(int index)
        {
            var localEntry = Model.DataView[index];
            var validation = CreateValidationSupport()
                .IsTrue(() => Commons.IsMovieFetched(localEntry.Data.Id), StringResources.NotFetchedMessage)
                .Validate();
            if (!validation) return;

            var result = View.ShowComboBoxInput(StringResources.AlternativeNameTitle, localEntry.Data.AlternativeNames, out string value);
            if (result != DialogResult.OK) return;

            localEntry.Title = value;
            Model.DataView.ResetBindings();
            _lastSelectedIndex = -1;
        }

        public void SaveIgnoreEntry(int index)
        {
            var manager = _kernel.Get<IPersistFileManager>();
            var entry = Model.DataView[index];
            entry.Data.Id = Commons.IgnoredEntryId;
            manager.Save(Path.GetDirectoryName(entry.FullPath), entry.Data);
        }

        public async Task ResearchMovie(int index)
        {
            var result = CreateValidationSupport().EnsureInternetConnected().Validate();
            if (!result) return;
            UpdateUi(UiState.Working);

            // old movie title/path
            var title = Model.DataView[index].Title;
            var fullPath = Model.DataView[index].FullPath;
            var tmdb = _kernel.Get<ITmdb>();

            // find title
            var foundTitles = new Dictionary<string, TmdbResult>();
            var ids = await tmdb.SearchMovies(title, 0);
            foreach (var id in ids)
            {
                var detail = await tmdb.GetByTmdbId(id);
                foundTitles.Add(detail.Id.ToString(), detail);
            }

            // show selection
            var selections = foundTitles.Values.Select(x => $"{x.Id}: {x.Title} ({x.Year})").ToArray();
            var dialogResult = View.ShowComboBoxInput("Select metadata.", selections, out string value);
            if (dialogResult != DialogResult.OK)
            {
                UpdateUi(UiState.Ready);
                return;
            }

            // update
            var selectedId = value.Split(':')[0];
            var selected = foundTitles.First(x => x.Value.Id.ToString() == selectedId).Value;
            Model.DataView.SwapItem(x => x.FullPath == fullPath, new MovieEntry(selected, fullPath));
            UpdateUi(UiState.Ready);
        }

        // -------- GRID VIEW
        public void GridSelectionChanged(int index)
        {
            if (_lastSelectedIndex == index) return;

            var dataItem = Model.DataView[index];
            if (Commons.IsMovieFetched(dataItem.TmdbId))
            {
                Model.LblTitleText = string.Format(StringResources.MovieTitleInfoPattern, dataItem.Title, dataItem.Year);
                Model.LblExtraInfoText = string.Format(StringResources.MovieExtraInfoPattern, dataItem.Genre, dataItem.ImdbId);
                Model.LblPlotText = dataItem.Plot;
                LoadImage(dataItem);
            }
            else
            {
                Model.LblTitleText = "No data.";
                Model.LblExtraInfoText = "No data.";
                Model.LblPlotText = "No data.";
                LoadImage(null);
            }
            _lastSelectedIndex = index;
        }

        public void GridCellFormatting(ref DataGridViewCellFormattingEventArgs e)
        {
            var item = Model.DataView[e.RowIndex];

            switch (item.Data.Id)
            {
                case Commons.NotFetchedEntryId:
                    e.CellStyle.BackColor = Color.Crimson;
                    break;

                case Commons.IgnoredEntryId:
                    e.CellStyle.BackColor = Color.Yellow;
                    break;
            }
        }

        public void ResetDetails()
        {
            Model.LblTitleText = StringResources.LiteralThreeDots;
            Model.LblExtraInfoText = StringResources.LiteralThreeDots;
            Model.LblPlotText = string.Empty;
            LoadImage(null);
        }
        #endregion

        #region Private Methods
        private void RecreateCancellationToken()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        private ValidationSupport CreateValidationSupport()
        {
            return new ValidationSupport()
                .SetCommonFailAction(x => View.ShowMessageBox(x, StringResources.AppName,
                    icon: MessageBoxIcon.Exclamation));
        }
        
        private void LoadImage(MovieEntry entry)
        {
            Task.Run(async () =>
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                var posterPath = Path.Combine(Path.GetDirectoryName(entry.FullPath), Commons.PosterFileName + Commons.JpgFileExtension);
                var tmdb = _kernel.Get<ITmdb>();
                if (!File.Exists(posterPath)) posterPath = tmdb.GetPosterUrl(entry.Data.PosterPath, PosterSize.w154);

                // remove old pict
                if (Model.PicPosterImage != Commons.DefaultImage) Model.PicPosterImage?.Dispose(); 
                Model.PicPosterImage = Commons.DefaultImage;
                if (posterPath == null) return;

                // download if not availiable
                var deleteAfter = false;
                if (posterPath.ToUpperInvariant().StartsWith("HTTP"))
                {
                    deleteAfter = true;
                    var requester = _kernel.Get<ITmdbWebRequest>();
                    posterPath = Path.GetTempFileName();
                    await requester.DownloadFile(posterPath, posterPath);
                }

                // load from file
                if (!File.Exists(posterPath)) return;
                using (var stream = new FileStream(posterPath, FileMode.Open))
                {
                    Model.PicPosterImage = Image.FromStream(stream);
                }

                // delete temp file
                if (deleteAfter) File.Delete(posterPath);
            });
        }

        private void CheckComponents()
        {
            Task.Run(() =>
            {
                var comp = _kernel.Get<IComponentManager>();
                if (comp.IsPythonInstalled().Result && comp.IsGuessItInstalled().Result) return;

                View.ShowMessageBox(StringResources.ComponentMissingMessage, StringResources.AppName,
                    icon: MessageBoxIcon.Exclamation);
                Model.CmdActionsEnabled = false;
                Model.CmdDirectoriesEnabled = false;
            });
        }

        private void UpdateUi(UiState state, int progressPercentage = -1)
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
                    Model.PrgStatusStyle = ProgressBarStyle.Blocks;
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

        private void InternalAnalyzeDirectory(string path)
        {
            Model.DataView.Clear();
            RecreateCancellationToken();
            Task.Run(async () =>
            {
                UpdateUi(UiState.Working);
                var persistFileManager = _kernel.Get<IPersistFileManager>();
                var tmdb = _kernel.Get<ITmdb>();
                var dirEnumbEnumerable = Directory.EnumerateDirectories(path, "*", SearchOption.TopDirectoryOnly);
                foreach (var basePath in dirEnumbEnumerable)
                {
                    var token = _cancellationToken.Token;
                    if (token.IsCancellationRequested) UpdateUi(UiState.Ready);
                    token.ThrowIfCancellationRequested();

                    try
                    {
                        string currentMoviePath;
                        var currentFolder = new PowerPath(basePath);

                        // check for ignore pattern
                        var lastName = currentFolder.GetLastDirectoryName();
                        if (lastName.StartsWith("[") && lastName.EndsWith("]")) continue;

                        // find first movie
                        if ((currentMoviePath = InternalFindFirstFile(basePath)) == null) continue;

                        // find metadata
                        TmdbResult entry;
                        if (persistFileManager.HasPersistentData(Path.GetDirectoryName(currentMoviePath)))
                        {
                            entry = persistFileManager.Load(currentMoviePath);
                        }
                        else
                        {
                            entry = await tmdb.GetByFilename(Path.GetFileNameWithoutExtension(currentMoviePath));
                        }

                        // pop to event
                        Model.Invoke(() => Model.DataView.Add(new MovieEntry(entry, currentMoviePath)));
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Analyze error: {0}. {1}", basePath, e.Message);
                    }
                }

                _lastSelectedIndex = -1;
                UpdateUi(UiState.Ready);
            }, _cancellationToken.Token);
        }
        
        private void InternalRenameFile(string path, MovieEntry entry)
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

        private void InternalRenameDirectory(string path, MovieEntry entry)
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

        private string InternalFindFirstFile(string dir)
        {
            try
            {
                var searcher = Directory.EnumerateFiles(dir, Commons.AllFilesSearchPattern, SearchOption.TopDirectoryOnly);
                var path = searcher.FirstOrDefault(x => _settings.MovieExtensions.Contains(Path.GetExtension(x)));
                return path;
            }
            catch (Exception e)
            {
                Debug.Print("Using remote poster for: {0}. {1}", dir, e.Message);
                return null;
            }
        }

        private async Task<MovieEntry> InternalFetch(string path)
        {
            var guessit = _kernel.Get<IGuessit>();
            var tmdb = _kernel.Get<ITmdb>();
            var filename = Path.GetFileName(path);
            TmdbResult newData = null;
            try
            {
                var name = await guessit.RealGuessName(filename);
                if (name?.ImdbId != null)
                {
                    newData = await tmdb.GetByImdbId(name.ImdbId);
                }
                else if (name?.Title != null)
                {
                    var movies = await tmdb.SearchMovies(name.Title, name.Year);
                    newData = await tmdb.GetByTmdbId(movies.First());
                }
            }
            catch (Exception e)
            {
                Debug.Print("Using remote poster for: {0}. {1}", filename, e.Message);
            }

            return new MovieEntry(newData ?? await tmdb.GetByFilename(filename), path);
        }
        #endregion
    }
}
