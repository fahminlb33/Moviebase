using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using BlastMVP;
using Moviebase.Entities;
using Moviebase.Presenters;

namespace Moviebase.Views
{
    public partial class MainView : Form
    {
        private readonly MainPresenter _presenter;
        
        public MainView()
        {
            InitializeComponent();
            _presenter = new MainPresenter(this)
            {
                CloseFolderCallback = CloseFolderCallback
            };
            _presenter.CheckComponents();
            GlueBindings();
        }

        private void CloseFolderCallback()
        {
            lblTitle.Text = StringResources.ThreeDots;
            lblExtraInfo.Text = StringResources.ThreeDots;
            txtPlot.Text = string.Empty;
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = null;
        }

        private void GlueBindings()
        {
            var model = _presenter.Model;
            grdMovies.DataSource = model.DataView;
            
            cmdFolderRecent.Bind(c => c.Enabled).To(model, m => m.CmdDirectoriesEnabled);
            cmdFolderClose.Bind(c => c.Enabled).To(model, m => m.CmdDirectoriesEnabled);
            cmdFolderOpen.Bind(c => c.Enabled).To(model, m => m.CmdDirectoriesEnabled);
            cmdActions.Bind(c => c.Enabled).To(model, m => m.CmdActionsEnabled);
            cmdTools.Bind(c => c.Enabled).To(model, m => m.CmdToolsEnabled);
            cmdStop.Bind(c => c.Enabled).To(model, m => m.CmdStopEnabled);

            prgStatus.Bind(c => c.Value).To(model, m => m.PrgStatusValue);
            prgStatus.Bind(c => c.Style).To(model, m => m.PrgStatusStyle);
            lblStatus.Bind(c => c.Text).To(model, m => m.LblStatusText);
            lblPercentage.Bind(c => c.Text).To(model, m => m.LblPercentageText);
        }

        #region Data Grid

        private void grdMovies_SelectionChanged(object sender, EventArgs e)
        {
            if (grdMovies.SelectedCells.Count == 0)
            {
                CloseFolderCallback();
                return;
            }

            var rowIndex = grdMovies.SelectedCells[0].RowIndex;
            var dataItem = (MovieEntryFacade)grdMovies.Rows[rowIndex].DataBoundItem;
            lblTitle.Text = string.Format(StringResources.MovieTitleInfoPattern, dataItem.Title, dataItem.Year);
            lblExtraInfo.Text = string.Format(StringResources.MovieExtraInfoPattern, dataItem.Genre, dataItem.ImdbId);
            txtPlot.Text = dataItem.Plot;

            if (dataItem.InternalMovieData.Id <= 0) return;
            var movieDir = Path.GetDirectoryName(dataItem.FullPath);
            var posterPath = _presenter.GetPosterPath(dataItem.InternalMovieData, movieDir);
            pictureBox1.LoadAsync(posterPath);
        }

        private void grdMovies_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var hti = grdMovies.HitTest(e.X, e.Y);
            if (hti.RowIndex == -1) return;
            grdMovies.ClearSelection();
            grdMovies.Rows[hti.RowIndex].Selected = true;
        }

        #endregion

        #region Event Subscribers



        // --------- TOOLS
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            _presenter.ShowSettingsWindow();
        }

        private void mnuMoveVies_Click(object sender, EventArgs e)
        {
            _presenter.ShowMoveMoviesWindow();
        }

        private void mnuExportCsv_Click(object sender, EventArgs e)
        {
            if (_presenter.Model.DataView.Count == 0)
            {
                this.ShowMessageBox(StringResources.ExportNoDataMessage, StringResources.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            _presenter.ExportCsv();
        }

        // --------- ACTIONS
        private void mnuFetchAll_Click(object sender, EventArgs e)
        {
            if (_presenter.Model.DataView.Count == 0)
            {
                _presenter.View.ShowMessageBox(StringResources.FetchNoDataMessage, StringResources.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            _presenter.FetchMovieData();
            _presenter.SavePresistData();
        }

        private void mnuRenameAll_Click(object sender, EventArgs e)
        {
            if (_presenter.Model.DataView.Count == 0)
            {
                _presenter.View.ShowMessageBox(StringResources.RenameNoDataMessage, StringResources.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            _presenter.SavePresistData();
            _presenter.RenameMovieFiles();
        }

        private void mnuDownloadAll_Click(object sender, EventArgs e)
        {
            if (_presenter.Model.DataView.Count == 0)
            {
                _presenter.View.ShowMessageBox(StringResources.PosterNoDataMessage, StringResources.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            _presenter.SavePresistData();
            _presenter.DownloadMoviePoster();
        }

        private void mnuFolderThumbnail_Click(object sender, EventArgs e)
        {
            if (_presenter.Model.DataView.Count == 0)
            {
                _presenter.View.ShowMessageBox(StringResources.ThumbnailNoDataMessage, StringResources.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            _presenter.ThumbnailFolder();
        }
        
        private void mnuSavePresistData_Click(object sender, EventArgs e)
        {
            if (_presenter.Model.DataView.Count == 0)
            {
                _presenter.View.ShowMessageBox(StringResources.PresistNoDataMessage, StringResources.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            _presenter.SavePresistData();
        }

        // --------- STOP
        private void cmdStop_Click(object sender, EventArgs e)
        {
            _presenter.StopProcess();
        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            using (var vw = new AboutView()) vw.ShowDialog();
        }

        // --------- DATA GRID
        private void mnuReSearch_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            _presenter.ResearchMovie(grdMovies.CurrentRow.Index);
        }

        private void mnuIgnore_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            var dataItem = (MovieEntryFacade)grdMovies.CurrentRow.DataBoundItem;
            _presenter.SingleSavePersistData(dataItem);
            this.ShowMessageBox(StringResources.ItemExcludedMessage, StringResources.AppName);
        }

        private void mnuSelectPoster_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            var dataItem = (MovieEntryFacade)grdMovies.CurrentRow.DataBoundItem;
            var path = _presenter.ShowSelectPosterWindow(dataItem.InternalMovieData.Id.ToString());
            if (path != null) dataItem.InternalMovieData.PosterPath = path;
        }

        private void mnuAlternativeNames_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            var dataItem = (MovieEntryFacade)grdMovies.CurrentRow.DataBoundItem;
            var name = _presenter.ShowAlternativeNameWindow(dataItem.InternalMovieData.AlternativeNames);
            if (name != null) dataItem.Title = name;
        }
        #endregion

        private void cmdFolderOpen_Click(object sender, EventArgs e)
        {
            if (_presenter.Model.DataView.Count > 0)
            {
                var result = this.ShowMessageBox(StringResources.AlreadyOpenedFolderMessage, StringResources.AppName,
                    icon: MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    _presenter.CloseFolder();
                }
                else
                {
                    return;
                }
            }

            _presenter.OpenDirectory(false);
        }

        private void cmdFolderClose_Click(object sender, EventArgs e)
        {
            _presenter.CloseFolder();
        }

        private void cmdFolderRecent_Click(object sender, EventArgs e)
        {
            if (_presenter.Model.DataView.Count > 0)
            {
                var result = this.ShowMessageBox(StringResources.AlreadyOpenedFolderMessage, StringResources.AppName, icon: MessageBoxIcon.Question);
                if (result != DialogResult.OK)
                    return;
                _presenter.CloseFolder();
            }
            _presenter.OpenDirectory(true);
        }

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(255, 32, 32, 32));
            e.Graphics.DrawRectangle(new System.Drawing.Pen(new SolidBrush(Color.FromArgb(255,32, 32, 32))), e.Bounds);
            e.DrawBorder();
            e.DrawText(); 
        }
    }
}

