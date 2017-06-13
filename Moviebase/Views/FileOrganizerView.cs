using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Properties;
using Moviebase.Services;

namespace Moviebase.Views
{
    public partial class FileOrganizerView : Form
    {
        private SynchronizationContext _synchronizationContext;
        private CancellationTokenSource _cancellationToken;

        private int _fileCount;
        private long _totalSize;

        public FileOrganizerView()
        {
            InitializeComponent();

            _synchronizationContext = SynchronizationContext.Current;
        }

        private void cmdMove_Click(object sender, EventArgs e)
        {
            if (txtRoot.TextLength == 0)
            {
                MessageBox.Show("Select a folder first!", "Moviebase", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (cmdMove.Text == "Move")
            {
                _fileCount = 0;
                _totalSize = 0;

                _cancellationToken?.Dispose();
                _cancellationToken = new CancellationTokenSource();

                Task.Run(() => MoveFiles(txtRoot.Text), _cancellationToken.Token).ContinueWith(t=>FinishCallback());
                cmdMove.Text = "Cancel";
            }
            else
            {
                _cancellationToken.Cancel();
                cmdMove.Enabled = false;
            }
        }

        private void MoveFiles(string path)
        {
            var supportedExtensions = Settings.Default.SupportedExtensions;
            var dir = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly);
            foreach (var currentPath in dir)
            {
                if (!supportedExtensions.Contains(Path.GetExtension(currentPath)))continue;

                // create dir
                var newDir = Path.Combine(path, Path.GetFileNameWithoutExtension(currentPath));
                if (Directory.Exists(newDir)) continue;
                Directory.CreateDirectory(newDir);

                // move
                var dest = Path.Combine(newDir, Path.GetFileName(currentPath));
                File.Move(currentPath, dest);

                UpdateProgress(new FileInfo(dest).Length);
            }
        }

        private void UpdateProgress(long size)
        {
            _synchronizationContext.Send(x =>
            {
                var currentSize = (long) x;
                _totalSize += currentSize;
                _fileCount++;

                lblFileCount.Text = _fileCount.ToString();
                lblTotalSize.Text = FileHelpers.BytesToString(_totalSize);
            }, size);
        }

        private void FinishCallback()
        {
            _synchronizationContext.Send(x =>
            {
                cmdMove.Text = "Move";
                cmdMove.Enabled = true;
            }, null);
        }

        private void cmdBrowse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dialogFolder.ShowDialog() != DialogResult.OK) return;
            txtRoot.Text = dialogFolder.SelectedPath;
        }
    }
}
