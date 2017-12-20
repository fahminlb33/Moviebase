using System;
using System.ComponentModel;
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
using Ninject;

namespace Moviebase.Presenters
{
    class MainPresenter
    {
        private readonly StandardKernel _kernel = Program.AppKernel;
        private readonly SynchronizationContext _context;
        private readonly FolderBrowserDialog _folderBrowserDialog;
        private readonly WorkerPool _workerPool;

        public Action CloseFolderCallback;
        public MainModel Model { get; }
        public MainView View { get; }

        public MainPresenter(MainView view)
        {
            View = view;
            Model = new MainModel(SynchronizationContext.Current);
            _context = SynchronizationContext.Current;

            _workerPool = new WorkerPool();
            _workerPool.ProgressChanged += Worker_ProgressChanged;
            _workerPool.RunWorkerCompleted += Worker_RunWorkerCompleted;
            _workerPool.RunWorkerStarted += Worker_RunWorkerStarted;

            _folderBrowserDialog = new FolderBrowserDialog
            {
                Description = StringResources.BrowseFolderDescription,
                ShowNewFolderButton = false
            };
        }

        #region Worker Subscriber

        private void Worker_RunWorkerStarted(object sender, EventArgs e)
        {
            Model.CmdDirectoriesEnabled = false;
            Model.CmdToolsEnabled = false;
            Model.CmdActionsEnabled = false;
            Model.CmdStopEnabled = true;
            Model.LblStatusText = "Working...";
        }

        private void Worker_RunWorkerCompleted(object sender, EventArgs e)
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

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
            {
                Model.PrgStatusStyle = ProgressBarStyle.Marquee;
                Model.PrgStatusValue = 0;
                Model.LblPercentageText = "Processing...";
            }
            else
            {
                Model.PrgStatusStyle = ProgressBarStyle.Blocks;
                Model.PrgStatusValue = e.ProgressPercentage;
                Model.LblPercentageText = $"{e.ProgressPercentage}%";
            }

            if (e.UserState == null) return;
            if (e.UserState.GetType() == typeof(DirectoryAnalyzeWorkerState))
            {
                var state = (DirectoryAnalyzeWorkerState) e.UserState;
                _context.Post(d => Model.DataView.Add((MovieEntryFacade)d), state.Entry);
            }
            else if (e.UserState.GetType() == typeof(ResearchMovieWorkerState))
            {
                var state = (ResearchMovieWorkerState)e.UserState;
                _context.Post(d => Model.DataView[state.Index] = (MovieEntryFacade) d, state.Entry);
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
                if (path == null)
                {
                    View.ShowMessageBox(StringResources.OpenDirNoRecord, StringResources.AppName,
                        icon: MessageBoxIcon.Exclamation);
                    return;
                }
                if (!Directory.Exists(path))
                {
                    View.ShowMessageBox(StringResources.OpenDirNotExist, StringResources.AppName,
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
          
            _workerPool.RunWorker(worker);
        }

        public void CloseFolder()
        {
            Model.DataView.Clear();
            CloseFolderCallback?.Invoke();
        }

        // -------- ACTIONS
        public void RenameMovieFiles()
        {
            var worker = _kernel.Get<IMovieRenameWorker>();
            worker.MovieEntries = Model.DataView.ToList();
            worker.FileRenamePattern = Settings.Default.FileRenamePattern;
            worker.FolderRenamePattern = Settings.Default.FolderRenamePattern;
            worker.SwapThe = Settings.Default.SwapThe;
            _workerPool.RunWorker(worker);
        }

        public void DownloadMoviePoster()
        {
            var worker = _kernel.Get<IPosterDownloadWorker>();
            worker.MovieEntries = Model.DataView.Where(w => w.InternalMovieData.PosterPath != null).ToList();
            worker.FileName = Settings.Default.PosterFileName + Commons.JpgFileExtension;
            worker.OverwritePoster = Settings.Default.OverwritePoster;
            _workerPool.RunWorker(worker);
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
            _workerPool.RunWorker(worker);
        }

        public void SavePresistData()
        {
            var worker = _kernel.Get<ISavePresistDataWorker>();
            worker.SaveItems = Model.DataView.ToList();
            _workerPool.RunWorker(worker);
        }

        public void SingleSavePersistData(MovieEntryFacade entry)
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
            _workerPool.RunWorker(worker);
        }

        public void ExportCsv()
        {
            var worker = _kernel.Get<ICsvExportWorker>();
            worker.Movies = Model.DataView.ToList();
            worker.OutputPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Commons.ExportFileName);
            _workerPool.RunWorker(worker);
        }

        public void ThumbnailFolder()
        {
            var worker = _kernel.Get<IThumbnailFolderWorker>();
            worker.MovieDirectories = Model.DataView.Select(x => Path.GetDirectoryName(x.FullPath)).ToList();
            worker.PosterName = Settings.Default.PosterFileName;
            _workerPool.RunWorker(worker);
        }

        #endregion

        #region Methods

        public string GetPosterPath(TmdbResult result, string dir)
        {
            var manager = _kernel.Get<IPersistentDataManager>();
            return manager.GetPosterUri(result, dir, Settings.Default.PosterFileName + Commons.JpgFileExtension);
        }

        public void CheckComponents()
        {
            Task.Run(() =>
            {
                var comp = _kernel.Get<IComponentManager>();
                if (comp.CheckPythonInstallation() && comp.CheckGuessItInstallation()) return;

                View.ShowMessageBox(StringResources.ComponentMissingMessage, StringResources.AppName,
                    icon: MessageBoxIcon.Exclamation);
                Model.CmdActionsEnabled = false;
                Model.CmdDirectoriesEnabled = false;
            });
        }

        #endregion

    }
}
