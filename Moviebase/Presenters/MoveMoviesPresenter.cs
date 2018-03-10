using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Core.MVP;
using Moviebase.Entities;
using Moviebase.Models;
using Moviebase.Properties;
using Moviebase.Views;
using Ninject;
using Ninject.Parameters;

namespace Moviebase.Presenters
{
    class MoveMoviesPresenter : PresenterBase
    {
        private readonly StandardKernel _kernel = Program.AppKernel;
        private readonly FolderBrowserDialog _folderBrowser;
        private int _processed;
        

        public MoveMoviesModel Model { get; }
        public MoveMoviesView View { get; }

        public MoveMoviesPresenter(MoveMoviesView view)
        {
            View = view;
            Model = _kernel.Get<MoveMoviesModel>(new ConstructorArgument("context", SynchronizationContext.Current));
            _folderBrowser = new FolderBrowserDialog {Description = Strings.SelectFolderMessage};
        }
        
        public void BrowseFolder()
        {
            if (_folderBrowser.ShowDialog() != DialogResult.OK) return;
            Model.TxtBrowseText = _folderBrowser.SelectedPath;
        }

        public void SearchMovies()
        {
            if (Model.TxtBrowseText == null)
            {
                View.ShowMessageBox(Strings.SelectFolderMessage, Strings.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            if (Model.CmdExecuteText == Strings.LiteralMoveText)
            {
                RecreateCancellationToken();
                Task.Run(() => InternalOrganizeDirectory(Model.TxtBrowseText), CancellationToken.Token);
            }
            else
            {
                CancelTask();
                Model.CmdExecuteEnabled = false;
            }
        }

        public void InternalOrganizeDirectory(string path)
        {
            // enumerate
            UpdateUi(UiState.Working);
            var extensions = new List<string>(Settings.Default.MovieExtensions.Split(';'));
            var dirEnumbEnumerable = Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);

            // walk
            foreach (var basePath in dirEnumbEnumerable)
            {
                try
                {
                    var currentPath = new PowerPath(basePath);
                    if (!extensions.Contains(currentPath.GetExtension())) return;

                    var newDir = Path.Combine(currentPath.GetDirectoryPath(), currentPath.GetFileNameWithoutExtension());
                    var newFile = Path.Combine(newDir, currentPath.GetFileName());

                    Directory.CreateDirectory(newDir);
                    File.Move(currentPath.GetFullPath(), newFile);
                    Model.Invoke(() => Model.DataView.Add(new MovedMovieEntry
                    {
                        Title = Path.GetFileName(path),
                        Path = Path.GetDirectoryName(path)
                    }));
                }
                catch (Exception e)
                {
                    Debug.Print("Organize error: {0}. {1}", basePath, e.Message);
                }

                if (CancellationToken.IsCancellationRequested) break;
            }

            // finish
            UpdateUi(UiState.Ready);
        }

        public override void UpdateUi(UiState state, int progressPercentage = -1)
        {
            switch (state)
            {
                case UiState.Working:
                    Model.CmdBrowseEnabled = false;
                    Model.CmdExecuteEnabled = true;
                    Model.CmdExecuteText = Strings.LiteralStopText;
                    break;

                case UiState.Ready:
                    Model.CmdBrowseEnabled = true;
                    Model.CmdExecuteEnabled = true;
                    Model.CmdExecuteText = Strings.LiteralMoveText;
                    break;

                case UiState.StatusUpdate:
                    Interlocked.Increment(ref _processed);
                    var count = Interlocked.CompareExchange(ref _processed, 0, 0);
                    Model.LblCountText = string.Format(Strings.MoveMoviesCountPattern, count);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (_folderBrowser != null) _folderBrowser.Dispose();
            base.Dispose(disposing);
        }
    }
}
