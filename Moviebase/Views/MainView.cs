using System;
using System.Drawing;
using System.Windows.Forms;
using Moviebase.Core.MVP;
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
            _presenter = new MainPresenter(this);
            GlueBindings();
        }

        private void GlueBindings()
        {
            var model = _presenter.Model;
            grdMovies.DataSource = model.DataView;
            grdMovies.Bind(c => c.Enabled).To(model, m => m.GridViewEnabled);
            picPoster.Bind(c => c.Image).To(model, m => m.PicPosterImage);
            lblTitle.Bind(c => c.Text).To(model, m => m.LblTitleText);
            lblExtraInfo.Bind(c => c.Text).To(model, m => m.LblExtraInfoText);
            txtPlot.Bind(x => x.Text).To(model, m => m.LblPlotText);

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
                _presenter.ResetDetails();
                return;
            }

            _presenter.GridSelectionChanged(grdMovies.SelectedCells[0].RowIndex);
        }

        private void grdMovies_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            _presenter.GridCellFormatting(ref e);
        }

        private void grdMovies_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button != MouseButtons.Right) return;
            //var hti = grdMovies.HitTest(e.X, e.Y);
            //if (hti.RowIndex == -1) return;
            //grdMovies.ClearSelection();
            //grdMovies.Rows[hti.RowIndex].Selected = true;
        }

        #endregion

        #region Event Subscribers

        private void cmdFolderOpen_Click(object sender, EventArgs e)
        {
            _presenter.OpenDirectory();
        }

        private void cmdFolderClose_Click(object sender, EventArgs e)
        {
            _presenter.CloseFolder();
        }

        private void cmdFolderRecent_Click(object sender, EventArgs e)
        {
            _presenter.OpenLastDirectory();
        }

        // --------- TOOLS
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            _presenter.ShowSettingsWindow();
        }

        private void mnuMoveVies_Click(object sender, EventArgs e)
        {
            _presenter.ShowMoveMoviesWindow();
        }

        private async void mnuExportCsv_Click(object sender, EventArgs e)
        {
            await _presenter.ExportCsv();
        }

        // --------- ACTIONS
        private void mnuFetchAll_Click(object sender, EventArgs e)
        {
            _presenter.FetchMovieData();
        }

        private void mnuRenameAll_Click(object sender, EventArgs e)
        {
            _presenter.RenameMovieFiles();
        }

        private void mnuDownloadAll_Click(object sender, EventArgs e)
        {
            _presenter.DownloadMoviePoster();
        }

        private void mnuFolderThumbnail_Click(object sender, EventArgs e)
        {
           _presenter.ThumbnailFolder();
        }
        
        private void mnuSavePresistData_Click(object sender, EventArgs e)
        {
            _presenter.SavePresistData();
        }

        // --------- STOP
        private void cmdStop_Click(object sender, EventArgs e)
        {
            _presenter.StopProcess();
        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            _presenter.ShowAboutView();
        }

        // --------- DATA GRID
        private async void mnuReSearch_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            await _presenter.ResearchMovie(grdMovies.CurrentRow.Index);
        }

        private void mnuIgnore_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            _presenter.SaveIgnoreEntry(grdMovies.CurrentRow.Index);
            this.ShowMessageBox(StringResources.ItemExcludedMessage, StringResources.AppName);
        }

        private void mnuSelectPoster_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            _presenter.ShowSelectPosterWindow(grdMovies.CurrentRow.Index);
        }

        private void mnuAlternativeNames_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            _presenter.ShowAlternativeNameWindow(grdMovies.CurrentRow.Index);
        }

        #endregion

        private void toolTip1_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(255, 32, 32, 32));
            e.Graphics.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(255,32, 32, 32))), e.Bounds);
            e.DrawBorder();
            e.DrawText(); 
        }
    }
}
