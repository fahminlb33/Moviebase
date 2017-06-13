using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Domain;
using Moviebase.Properties;
using Moviebase.Services;
using Ninject;

namespace Moviebase.Views
{
    public partial class MovieRenamerView : Form
    {
        private readonly SynchronizationContext _synchronizationContext;
        private readonly BindingList<MovieEntry> _dataSource; 
        private CancellationTokenSource _cancellationToken;
        
        private readonly MovieOrganizer _movieOrganizer;
        private readonly Guessit _guessit;
        private readonly Tmdb _tmdb ;
        private int _processedCount;
        private bool _isWorking;

        public MovieRenamerView()
        {
            InitializeComponent();

            var kernel = new StandardKernel();
            _guessit = kernel.Get<Guessit>();
            _tmdb = kernel.Get<Tmdb>();

            _synchronizationContext = SynchronizationContext.Current;
            _movieOrganizer = new MovieOrganizer();
            _dataSource = new BindingList<MovieEntry>();
            _processedCount = 0;
            _isWorking = false;
        }

        #region Core
        private void MainView_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _dataSource;
            ControlHelpers.ChangDataGridViewStyle(ref dataGridView1);
        }

        private void AddToList(MovieEntry entry)
        {
            _dataSource.Add(entry);
        }

        private void RecreateCancellationToken()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        private ParallelOptions RecreateParallelismOptions()
        {
            return new ParallelOptions
            {
                CancellationToken = _cancellationToken.Token,
                MaxDegreeOfParallelism = 2
            };
        }
        #endregion

        #region Threads
        private void FindMovie(string dirPath)
        {
            try
            {
                MovieEntry entry;
                string fileName = null;

                // step 1 -- persistent data
                if (_movieOrganizer.HasPersistentData(dirPath))
                {
                    entry = _movieOrganizer.LoadData(dirPath);
                    if (entry != null) goto post;
                }

                // find movie
                var filePath = MovieOrganizer.FindFirstFile(dirPath);
                if (filePath == null) return;
                fileName = Path.GetFileName(filePath);

                // step 2 -- filename contains imdb title
                if (_cancellationToken.IsCancellationRequested) return;
                var imdbId = _guessit.GuessImdbId(filePath);
                if (imdbId != null)
                {
                    if (_cancellationToken.IsCancellationRequested) return;
                    entry = _tmdb.GetByImdbId(imdbId);
                    goto post;
                }

                // step 3 -- manually find
                if (_cancellationToken.IsCancellationRequested) return;
                var name = _guessit.GuessName(fileName);
                if (name == null) goto allfail;

                if (_cancellationToken.IsCancellationRequested) return;
                var searchedId = _tmdb.SearchFirstMovieId(name.title, name.year);
                if (searchedId == -1) goto allfail;

                if (_cancellationToken.IsCancellationRequested) return;
                entry = _tmdb.GetByTmdbId(searchedId);
                if (entry != null) goto post;

                // step 4 -- no more choices
                allfail:
                entry = _tmdb.GetFromFile(fileName);

                post:
                entry.BasePath = dirPath;
                if (fileName != null) entry.FileName = fileName;

                _movieOrganizer.SaveData(entry);
                _synchronizationContext.Send(x => AddToList((MovieEntry) x), entry);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void RenameMovie(MovieEntry entry)
        {
            try
            {
                // rename file
                var name = Renamer.GetReplacedPattern(Settings.Default.MovieNamePattern, entry);
                var source = Path.Combine(entry.BasePath, entry.FileName);
                var dest = Path.Combine(entry.BasePath, name);

                File.Move(source, dest);
                entry.FileName = name;
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                // rename dir
                if (Settings.Default.RenameDirectory)
                {
                    var name = Renamer.GetReplacedPattern(Settings.Default.DirectoryNamePattern, entry);
                    var parent = Path.GetFullPath(Path.Combine(entry.BasePath, @"..\"));
                    var dest = Path.Combine(parent, name);

                    Directory.Move(entry.BasePath, dest);
                    entry.BasePath = dest;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            // save persist data
            _movieOrganizer.SaveData(entry);

            Interlocked.Increment(ref _processedCount);
            _synchronizationContext.Post(x =>
            {
                var percentage = (double) _processedCount/_dataSource.Count*100;
                prgStatus.Value = (int) percentage;
            }, null);
        }

        private void SearchMovieThread(string rootDir)
        {
            _isWorking = true;
            var dirEnumerator = _movieOrganizer.EnumerateDirectoryRated(rootDir);
            
            Parallel.ForEach(dirEnumerator, RecreateParallelismOptions(), FindMovie);
            _synchronizationContext.Send(x => FinishThreadCallback(), null);
            _isWorking = false;
        }

        private void RenameMovieThread(IEnumerable<MovieEntry> dirEnumerator)
        {
            _isWorking = true;

            Parallel.ForEach(dirEnumerator, RecreateParallelismOptions(), RenameMovie);
            _synchronizationContext.Send(x => FinishThreadCallback(), null);
            _isWorking = false;
        }

        private void FinishThreadCallback()
        {
            cmdStop.Enabled = false;
            cmdOpenFolder.Enabled = true;
            cmdRenameAll.Enabled = true;
            prgStatus.Style = ProgressBarStyle.Blocks;
            prgStatus.Value = 0;
            _processedCount = 0;
        }
        #endregion

        #region DataGridView
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ctGridView.Enabled = !(e.RowIndex == -1 || _isWorking);
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.Rows[e.RowIndex].Selected = true;
                ctGridView.Show(MousePosition);
            }
            else
            {
                var data = (MovieEntry)dataGridView1.Rows[e.RowIndex].DataBoundItem;

                lblTitle.Text = data.Title;
                lblYear.Text = data.Year.ToString();
                lblGenre.Text = data.Genre;
                txtPlot.Text = data.Plot;
                picPoster.CancelAsync();
                picPoster.LoadAsync(_tmdb.GetPosterUri(data.PosterPath, PosterSize.w154));
            }
        }

        private void cmdSelectPoster_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            var entry = (MovieEntry) dataGridView1.SelectedRows[0].DataBoundItem;

            using (var vw = new PosterSelectorView(entry.Id))
            {
                if (vw.ShowDialog() == DialogResult.OK)
                {
                    entry.PosterPath = "/" + vw.FilePath;
                } 
            }
        }
        #endregion

        #region MenuStrip
        private void mnuOpenFolder_Click(object sender, EventArgs e)
        {
            cmdOpenFolder.PerformClick();
        }

        private void mnuSettings_Click(object sender, EventArgs e)
        {
            cmdSettings.PerformClick();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuMovieOrganizer_Click(object sender, EventArgs e)
        {
            cmdMovieOrganizer.PerformClick();
        }

        private void mnuSubsceneFinder_Click(object sender, EventArgs e)
        {
            cmdSubsceneFinder.PerformClick();
        }
        #endregion

        #region ToolStrip
        private void cmdOpenFolder_Click(object sender, EventArgs e)
        {
            if (dialogFolder.ShowDialog() != DialogResult.OK) return;

            cmdStop.Enabled = true;
            cmdOpenFolder.Enabled = false;
            cmdRenameAll.Enabled = false;
            prgStatus.Style = ProgressBarStyle.Marquee;

            RecreateCancellationToken();
            var path = string.Copy(dialogFolder.SelectedPath);
            Task.Run(() => SearchMovieThread(path), _cancellationToken.Token);
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            _cancellationToken?.Cancel();
            cmdStop.Enabled = false;
        }

        private void cmdRenameAll_Click(object sender, EventArgs e)
        {
            if (_dataSource.Count == 0) return;

            cmdStop.Enabled = true;
            cmdOpenFolder.Enabled = false;
            cmdRenameAll.Enabled = false;
            prgStatus.Style = ProgressBarStyle.Blocks;

            RecreateCancellationToken();
            var dirEnumerator = _dataSource.AsEnumerable();
            Task.Run(() => RenameMovieThread(dirEnumerator));
        }

        private void cmdMovieOrganizer_Click(object sender, EventArgs e)
        {
            using (var vw = new FileOrganizerView())
                vw.ShowDialog();
        }

        private void cmdSubsceneFinder_Click(object sender, EventArgs e)
        {
            using (var vw = new SubtitleSearchView())
                vw.ShowDialog();
        }

        private void cmdSettings_Click(object sender, EventArgs e)
        {
            using (var vw = new SettingsView())
                vw.ShowDialog();
        }
        #endregion
    }
}

