using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Domain;
using Moviebase.Services;
using Ninject;

namespace Moviebase.Views
{
    public partial class PosterSelectorView : Form
    {
        private SynchronizationContext _synchronizationContext;
        private Tmdb _tmdb;
        private int _id;
        private string _tempFolder;
        private CancellationTokenSource _cancellationToken;

        public string FilePath { get; set; }

        public PosterSelectorView(int id)
        {
            InitializeComponent();

            var kernel = new StandardKernel();
            _tmdb = kernel.Get<Tmdb>();
            _synchronizationContext = SynchronizationContext.Current;
            _id = id;
        }

        private void cmdOk_Click(object sender, System.EventArgs e)
        {
            if (lvPosters.CheckedItems.Count == 0)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }

            var item = lvPosters.CheckedItems.First();
            FilePath = item.FileName;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void PosterSelectorView_Load(object sender, System.EventArgs e)
        {
            _tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(_tempFolder);

            _cancellationToken = new CancellationTokenSource();
            Task.Run(() => FindPosters(), _cancellationToken.Token);
        }

        private void FindPosters()
        {
            try
            {
                var posters = _tmdb.GetPosters(_id);
                foreach (var poster in posters)
                {
                    if (_cancellationToken.IsCancellationRequested) goto exit;
                    var fullPath = _tmdb.GetPosterUri(poster, PosterSize.w154);
                    var destFile = Path.Combine(_tempFolder, Path.GetFileName(fullPath));

                    HttpHelper.DownloadFile(fullPath, destFile);
                    _synchronizationContext.Send(x =>
                    {
                        lvPosters.Items.Add(x.ToString());
                    }, destFile);
                }

                exit:
                _synchronizationContext.Send(x =>
                {
                    prgProgress.Style = ProgressBarStyle.Blocks;
                }, null);
            }
            catch
            {
                _synchronizationContext.Send(
                    x =>
                    {
                        MessageBox.Show("Cannot find posters.", "Moviebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }, null);
            }
        }

        private void PosterSelectorView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Directory.Delete(_tempFolder, true);
            }
            catch
            {
                // ignored
            }
        }

        private void lvPosters_ItemCheckBoxClick(object sender, Manina.Windows.Forms.ItemEventArgs e)
        {
            if (lvPosters.CheckedItems.Count <= 1) return;
            MessageBox.Show("You cannot select more than 1 poster.", "Moviebase", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            foreach (var item in lvPosters.Items)
            {
                item.Checked = false;
            }
        }
    }
}
