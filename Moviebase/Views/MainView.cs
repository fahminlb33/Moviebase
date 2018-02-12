using System;
using System.Drawing;
using System.Windows.Forms;
using Moviebase.Core.MVP;
using Moviebase.Entities;
using Moviebase.Presenters;
using Moviebase.Properties;

namespace Moviebase.Views
{
    public partial class MainView : Form
    {
        private readonly FolderSelectDialog _folderBrowserDialog;
        private readonly SaveFileDialog _saveFileDialog;
        private readonly MainPresenter _presenter;

        public MainView()
        {
            InitializeComponent();
            _presenter = new MainPresenter(this);
            GlueBindings();

            _folderBrowserDialog = new FolderSelectDialog
            {
                Title = Strings.BrowseFolderDescription,
            };
            _saveFileDialog = new SaveFileDialog
            {
                // ReSharper disable once LocalizableElement
                Filter = "Comma Separated File|*.csv",
                DefaultExt = "*.csv",
                FileName = "Moviebase.csv",
                Title = Strings.BrowseExportTitle
            };
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
            if (!_folderBrowserDialog.ShowDialog()) return;
            _presenter.OpenDirectory(_folderBrowserDialog.SelectedPath);
        }

        private void cmdFolderClose_Click(object sender, EventArgs e)
        {
            _presenter.CloseFolder();
        }

        private void cmdFolderRecent_Click(object sender, EventArgs e)
        {
            var settings = Settings.Default;
            if (string.IsNullOrWhiteSpace(settings.LastOpenDirectory))
            {
                this.ShowMessageBox(Strings.OpenDirNoRecord, Strings.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }
            if (!System.IO.Directory.Exists(settings.LastOpenDirectory))
            {
                this.ShowMessageBox(Strings.OpenDirNotExist, Strings.AppName, icon: MessageBoxIcon.Exclamation);
                return;
            }

            _presenter.OpenDirectory(settings.LastOpenDirectory);
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

        private void mnuExportCsv_Click(object sender, EventArgs e)
        {
            if (_saveFileDialog.ShowDialog() != DialogResult.OK) return;
            _presenter.ExportCsv(_saveFileDialog.FileName);
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
        private void mnuReSearch_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            _presenter.ResearchMovie(grdMovies.CurrentRow.Index);
        }

        private void mnuIgnore_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            _presenter.SaveIgnoreEntry(grdMovies.CurrentRow.Index);
            this.ShowMessageBox(Strings.ItemExcludedMessage, Strings.AppName);
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
