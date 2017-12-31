using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BlastMVP;
using Moviebase.Core;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using Moviebase.Models;
using Moviebase.Properties;
using Moviebase.Views;
using Ninject;

namespace Moviebase.Presenters
{
    class MoveMoviesPresenter
    {
        private readonly StandardKernel _kernel = Program.AppKernel;
        private readonly FolderBrowserDialog _folderBrowser;
        private readonly IWorkerPool _workerPool;
        private int _processed;

        public MoveMoviesModel Model { get; }
        public MoveMoviesView View { get; }

        public MoveMoviesPresenter(MoveMoviesView view)
        {
            View = view;
            Model = new MoveMoviesModel(SynchronizationContext.Current);

            _workerPool = _kernel.Get<IWorkerPool>();
            _workerPool.RunWorkerStarted = Worker_RunWorkerStarted;
            _workerPool.ProgressChanged = Worker_ProgressChanged;
            _workerPool.RunWorkerCompleted = Worker_RunWorkerCompleted;

            _folderBrowser = new FolderBrowserDialog {Description = StringResources.SelectFolderMessage};
        }

        #region Worker Subscriber

        private void Worker_RunWorkerStarted()
        {
            Model.CmdBrowseEnabled = false;
            Model.CmdExecuteEnabled = true;
            Model.CmdExecuteText = StringResources.LiteralStopText;
        }

        private void Worker_RunWorkerCompleted()
        {
            Model.CmdBrowseEnabled = true;
            Model.CmdExecuteEnabled = true;
            Model.CmdExecuteText = StringResources.LiteralMoveText;
        }

        private void Worker_ProgressChanged(int progressPercentage, object state)
        {
            var path = new PowerPath(state.ToString());

            Interlocked.Increment(ref _processed);
            var count = Interlocked.CompareExchange(ref _processed, 0, 0);
            Model.LblCountText = string.Format(StringResources.MoveMoviesCountPattern, count);
            Model.Invoke(() => Model.DataView.Add(new MovedMovieEntry
            {
                Title = path.GetFileName(),
                Path = path.GetDirectoryPath()
            }));
        }

        #endregion

        public void BrowseFolder()
        {
            if (_folderBrowser.ShowDialog() != DialogResult.OK) return;
            Model.TxtBrowseText = _folderBrowser.SelectedPath;
        }

        public void SearchMovies()
        {
            if (Model.TxtBrowseText == null)
            {
                View.ShowMessageBox(StringResources.SelectFolderMessage, StringResources.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            if (Model.CmdExecuteText == StringResources.LiteralMoveText)
            {
                var worker = Program.AppKernel.Get<IMoveMovieWorker>();
                worker.AnalyzePath = _folderBrowser.SelectedPath;
                worker.FileExtensions = Settings.Default.MovieExtensions.Cast<string>().ToList();
                _workerPool.Start(worker);
            }
            else
            {
                _workerPool.Stop();
                Model.CmdExecuteEnabled = false;
            }
        }
    }
}
