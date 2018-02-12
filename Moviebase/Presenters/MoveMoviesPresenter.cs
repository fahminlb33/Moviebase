using System;
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
    class MoveMoviesPresenter
    {
        private readonly StandardKernel _kernel = Program.AppKernel;
        private readonly FolderBrowserDialog _folderBrowser;
        private CancellationTokenSource _cancellationToken;
        private int _processed;

        public enum UiState
        {
            Running,
            Stopped,
            Update
        }

        public MoveMoviesModel Model { get; }
        public MoveMoviesView View { get; }

        public MoveMoviesPresenter(MoveMoviesView view)
        {
            View = view;
            Model = _kernel.Get<MoveMoviesModel>(new ConstructorArgument("context", SynchronizationContext.Current));
            _folderBrowser = new FolderBrowserDialog {Description = Strings.SelectFolderMessage};
        }

        #region Worker Subscriber

       
        #endregion
        private void RecreateCancellationToken()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        public void BrowseFolder()
        {
            if (_folderBrowser.ShowDialog() != DialogResult.OK) return;
            Model.TxtBrowseText = _folderBrowser.SelectedPath;
        }

        private void UpdateUi(UiState mode, string path = null)
        {
            switch (mode)
            {
                case UiState.Running:
                    Model.CmdBrowseEnabled = false;
                    Model.CmdExecuteEnabled = true;
                    Model.CmdExecuteText = Strings.LiteralStopText;
                    break;

                case UiState.Stopped:
                    Model.CmdBrowseEnabled = true;
                    Model.CmdExecuteEnabled = true;
                    Model.CmdExecuteText = Strings.LiteralMoveText;
                    break;

                case UiState.Update:
                    Interlocked.Increment(ref _processed);
                    var count = Interlocked.CompareExchange(ref _processed, 0, 0);
                    Model.LblCountText = string.Format(Strings.MoveMoviesCountPattern, count);
                    Model.Invoke(() => Model.DataView.Add(new MovedMovieEntry
                    {
                        Title = Path.GetFileName(path),
                        Path = Path.GetDirectoryName(path)
                    }));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
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
                OrganizeDirectory(_folderBrowser.SelectedPath);
            }
            else
            {
                _cancellationToken.Cancel();
                Model.CmdExecuteEnabled = false;
            }
        }

        public void OrganizeDirectory(string path)
        {
            RecreateCancellationToken();
            Task.Run(() =>
            {
                UpdateUi(UiState.Running);
                var dirEnumbEnumerable = Directory.EnumerateFiles(path, "*", SearchOption.TopDirectoryOnly);
                foreach (var basePath in dirEnumbEnumerable)
                {
                    var token = _cancellationToken.Token;
                    token.ThrowIfCancellationRequested();

                    try
                    {
                        var currentPath = new PowerPath(basePath);
                        if (!Settings.Default.MovieExtensions.Contains(currentPath.GetExtension())) return;

                        var newDir = Path.Combine(currentPath.GetDirectoryPath(), currentPath.GetFileNameWithoutExtension());
                        var newFile = Path.Combine(newDir, currentPath.GetFileName());

                        Directory.CreateDirectory(newDir);
                        File.Move(currentPath.GetFullPath(), newFile);
                        UpdateUi(UiState.Update, currentPath);
                    }
                    catch (Exception e)
                    {
                        Debug.Print("Organize error: {0}. {1}", basePath, e.Message);
                    }
                }
                UpdateUi(UiState.Stopped);
            }, _cancellationToken.Token);
        }
    }
}
