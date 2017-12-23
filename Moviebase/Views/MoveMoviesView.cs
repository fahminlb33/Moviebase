using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using BlastMVP;
using Moviebase.Core;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using Moviebase.Properties;
using Ninject;

namespace Moviebase.Views
{
    public partial class MoveMoviesView : Form
    {
        private readonly StandardKernel _kernel = Program.AppKernel;
        private readonly SynchronizationContext _synchronizationContext;
        private readonly IWorkerPool _workerPool;
        private int _processed;

        public MoveMoviesView()
        {
            InitializeComponent();
            _synchronizationContext = SynchronizationContext.Current;
            _workerPool = _kernel.Get<IWorkerPool>();
            PrepareView();
        }

        private void PrepareView()
        {
            _workerPool.RunWorkerCompleted = Worker_RunWorkerCompleted;
            _workerPool.ProgressChanged = Worker_ProgressChanged;
        }

        private void Worker_ProgressChanged(int progressPercentage, object state)
        {
            _synchronizationContext.Post(d =>
            {
                using (var path = new PowerPath(state.ToString()))
                {
                    Interlocked.Increment(ref _processed);
                    lblCount.Text = string.Format(StringResources.MoveMoviesCount, Interlocked.CompareExchange(ref _processed, 0, 0));
                    var item = lvMovies.Items.Add(path.GetFileName());
                    item.SubItems.Add(path.GetDirectoryPath());
                }
            }, null);
        }

        private void Worker_RunWorkerCompleted()
        {
            _synchronizationContext.Post(d =>
            {
                cmdSearch.Text = StringResources.MoveMovieCommandText;
                cmdSearch.Enabled = true;
            }, null);
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (folderBrowser.ShowDialog() != DialogResult.OK) return;
            txtPath.Text = folderBrowser.SelectedPath;
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            if (txtPath.TextLength == 0)
            {
                this.ShowMessageBox(StringResources.SelectFolderMessage, StringResources.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }
            if (cmdSearch.Text == StringResources.MoveMovieCommandText)
            {
                var worker = Program.AppKernel.Get<IMoveMovieWorker>();
                worker.AnalyzePath = folderBrowser.SelectedPath;
                worker.FileExtensions = Settings.Default.MovieExtensions.Cast<string>().ToList();
                _workerPool.Start(worker);
                cmdSearch.Text = StringResources.StopText;
            }
            else
            {
                _workerPool.Stop();
                cmdSearch.Enabled = false;
            }
        }
    }
}
