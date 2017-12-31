using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Properties;
using Moviebase.Views;
using Moviebase.Entities;
using BlastMVP;
using Moviebase.Core;
using Moviebase.Core.Contracts;
using Moviebase.Models;
using Ninject;

namespace Moviebase.Presenters
{
    class MainPresenter
    {
        private readonly StandardKernel _kernel = Program.AppKernel;
        private readonly FolderBrowserDialog _folderBrowserDialog;
        private readonly IWorkerPool _workerPool;
        
        public MainModel Model { get; }
        public MainView View { get; }

        public MainPresenter(MainView view)
        {
            View = view;
            Model = new MainModel(SynchronizationContext.Current);

            _workerPool = _kernel.Get<IWorkerPool>();
            _workerPool.RunWorkerStarted = Worker_RunWorkerStarted;
            _workerPool.ProgressChanged = Worker_ProgressChanged;
            _workerPool.RunWorkerCompleted = Worker_RunWorkerCompleted;

            _folderBrowserDialog = new FolderBrowserDialog
            {
                Description = StringResources.BrowseFolderDescription,
                ShowNewFolderButton = false
            };

            CheckComponents();
        }

        #region Worker Subscriber

        private void Worker_RunWorkerStarted()
        {
            Model.CmdDirectoriesEnabled = false;
            Model.CmdToolsEnabled = false;
            Model.CmdActionsEnabled = false;
            Model.CmdStopEnabled = true;
            Model.LblStatusText = "Working...";
        }

        private void Worker_RunWorkerCompleted()
        {
            Model.PrgStatusValue = 0;
            Model.LblPercentageText = "0%";
            Model.PrgStatusStyle = ProgressBarStyle.Blocks;
            Model.LblStatusText = "Ready.";

            Model.CmdDirectoriesEnabled = true;
            Model.CmdToolsEnabled = true;
            Model.CmdActionsEnabled = true;
            Model.CmdStopEnabled = false;
        }

        private void Worker_ProgressChanged(int progressPercentage, object state)
        {
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

            if (state == null) return;
            if (state.GetType() == typeof(MovieEntryState))
            {
                var arg = (MovieEntryState)state;
                Model.Invoke(() => Model.DataView.Add(arg.Entry));
            }
            else if (state.GetType() == typeof(ResearchMovieEntryState))
            {
                var arg = (ResearchMovieEntryState)state;
                Model.Invoke(()=> Model.DataView[arg.Index] = arg.Entry);
            }
        }

        #endregion
        
        #region Event Handlers

        // -------- STOP
        public void StopProcess()
        {
            _workerPool.Stop();
            Model.CmdStopEnabled = false;
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

        public string ShowSelectPosterWindow(string id)
        {
            using (var vw = new SelectPosterView(id))
            {
                return vw.ShowDialog() == DialogResult.OK ? vw.SelectedPath : null;
            }
        }

        public string ShowAlternativeNameWindow(string[] names)
        {
            View.ShowComboBoxInput(StringResources.AlternativeNameTitle, names, out string value);
            return value;
        }

        // -------- DIRECTORIES
        public void OpenDirectory(bool isLastDir)
        {
            Model.DataView.Clear();
            var worker = _kernel.Get<IDirectoryAnalyzeWorker>();
            var settings = Settings.Default;

            if (isLastDir)
            {
                var path = settings.LastOpenDirectory;
                if (path == null || !Directory.Exists(path))
                {
                    View.ShowMessageBox(StringResources.OpenDirNoRecord, StringResources.AppName,
                        icon: MessageBoxIcon.Exclamation);
                    return;
                }

                worker.AnalyzePath = path;
            }
            else
            {
                if (_folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
                settings.LastOpenDirectory = _folderBrowserDialog.SelectedPath;
                settings.Save();

                worker.AnalyzePath = _folderBrowserDialog.SelectedPath;
            }
          
            _workerPool.Start(worker);
        }

        public void CloseFolder()
        {
            Model.DataView.Clear();
            ResetDetails();
        }

        // -------- ACTIONS
        public void RenameMovieFiles()
        {
            var worker = _kernel.Get<IMovieRenameWorker>();
            worker.MovieEntries = Model.DataView.ToList();
            worker.FileRenamePattern = Settings.Default.FileRenamePattern;
            worker.FolderRenamePattern = Settings.Default.FolderRenamePattern;
            worker.SwapThe = Settings.Default.SwapThe;

            _workerPool.Start(worker);
        }

        public void DownloadMoviePoster()
        {
            var worker = _kernel.Get<IPosterDownloadWorker>();
            worker.MovieEntries = Model.DataView.Where(w => w.InternalMovieData.PosterPath != null).ToList();
            worker.FileName = Settings.Default.PosterFileName + Commons.JpgFileExtension;
            worker.OverwritePoster = Settings.Default.OverwritePoster;

            _workerPool.Start(worker);
        }

        public void FetchMovieData()
        {
            var worker = _kernel.Get<IMovieFetchWorker>();
            worker.AnalyzeItems = Model.DataView
                .Where(i => i.InternalMovieData.Id == 0)
                .Select(x => x.FullPath)
                .ToList();
            foreach (var item in Model.DataView.Where(i => i.InternalMovieData.Id == 0).ToList())
            {
                Model.DataView.Remove(item);
            }
            _workerPool.Start(worker);
        }

        public void SavePresistData()
        {
            var worker = _kernel.Get<ISavePresistDataWorker>();
            worker.SaveItems = Model.DataView.ToList();
            _workerPool.Start(worker);
        }

        public void SingleSavePersistData(MovieEntry entry)
        {
            var manager = _kernel.Get<IPersistentDataManager>();
            entry.InternalMovieData.Id = -2;
            manager.SaveData(entry.InternalMovieData, Path.GetDirectoryName(entry.FullPath));
        }

        public void ResearchMovie(int index)
        {
            var worker = _kernel.Get<IResearchMovieWorker>();
            worker.Index = index;
            worker.FullPath = Model.DataView[index].FullPath;
            worker.View = View;
            _workerPool.Start(worker);
        }

        public void ExportCsv()
        {
            var worker = _kernel.Get<ICsvExportWorker>();
            worker.Movies = Model.DataView.ToList();
            worker.OutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Commons.ExportFileName);
            _workerPool.Start(worker);
        }

        public void ThumbnailFolder()
        {
            var worker = _kernel.Get<IThumbnailFolderWorker>();
            worker.MovieDirectories = Model.DataView.Select(x => Path.GetDirectoryName(x.FullPath)).ToList();
            worker.PosterName = Settings.Default.PosterFileName;
            _workerPool.Start(worker);
        }

        // -------- GRID VIEW
        public void GridSelectionChanged(int rowIndex)
        {
            var dataItem = Model.DataView[rowIndex];
            Model.LblTitleText = string.Format(StringResources.MovieTitleInfoPattern, dataItem.Title, dataItem.Year);
            Model.LblExtraInfoText = string.Format(StringResources.MovieExtraInfoPattern, dataItem.Genre, dataItem.ImdbId);
            Model.LblPlotText = dataItem.Plot;

            var movieDir = Path.GetDirectoryName(dataItem.FullPath);
            var posterPath = GetPosterPath(dataItem.InternalMovieData, movieDir);
            LoadImage(posterPath);
        }

        public void GridCellFormatting(ref DataGridViewCellFormattingEventArgs e)
        {
            var item = Model.DataView[e.RowIndex];

            switch (item.InternalMovieData.Id)
            {
                case 0:
                    e.CellStyle.BackColor = Color.Crimson;
                    break;
                case -2:
                    e.CellStyle.BackColor = Color.Yellow;
                    break;
            }
        }
        #endregion

        #region Methods

        public void ResetDetails()
        {
            Model.LblTitleText = StringResources.LiteralThreeDots;
            Model.LblExtraInfoText = StringResources.LiteralThreeDots;
            Model.LblPlotText = string.Empty;
            LoadImage(null);
        }
        
        private string GetPosterPath(TmdbResult result, string dir)
        {
            var manager = _kernel.Get<IPersistentDataManager>();
            return manager.GetPosterUri(result, dir, Settings.Default.PosterFileName + Commons.JpgFileExtension);
        }

        private void LoadImage(string uri)
        {
            Task.Run(async () =>
            {
                // remove old pict
                if (Model.PicPosterImage != Commons.DefaultImage) Model.PicPosterImage?.Dispose(); 
                Model.PicPosterImage = Commons.DefaultImage;
                if (uri == null) return;

                // download if not availiable
                var loadPath = uri;
                var deleteAfter = false;
                if (uri.ToUpperInvariant().StartsWith("HTTP"))
                {
                    deleteAfter = true;
                    var requester = _kernel.Get<ITmdbWebRequest>();
                    loadPath = Path.GetTempFileName();
                    await requester.DownloadFile(uri, loadPath);
                }

                // load from file
                using (var stream = new FileStream(loadPath, FileMode.Open))
                {
                    Model.PicPosterImage = Image.FromStream(stream);
                }

                // delete temp file
                if (deleteAfter) File.Delete(loadPath);
            });
        }

        private void CheckComponents()
        {
            Task.Run(() =>
            {
                var comp = _kernel.Get<IComponentManager>();
                if (comp.CheckPythonInstallation().Result && comp.CheckGuessItInstallation().Result) return;

                View.ShowMessageBox(StringResources.ComponentMissingMessage, StringResources.AppName,
                    icon: MessageBoxIcon.Exclamation);
                Model.CmdActionsEnabled = false;
                Model.CmdDirectoriesEnabled = false;
            });
        }

        #endregion

    }
}
