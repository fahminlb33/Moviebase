using System;
using System.Windows.Forms;
using Moviebase.Core.MVP;
using Moviebase.Core.Natives;
using Moviebase.Entities;
using Moviebase.Presenters;
using Moviebase.Properties;
// ReSharper disable LocalizableElement

namespace Moviebase.Views
{
    public partial class MainView : Form
    {
        private readonly FolderSelectDialog _folderBrowserDialog;
        private readonly SaveFileDialog _saveFileDialog;
        private readonly MainPresenter _presenter;
        private readonly ValidationSupportFactory _validationFactory;

        public MainView()
        {
            InitializeComponent();
            _presenter = new MainPresenter(this);
            GlueBindings();

            _validationFactory = new ValidationSupportFactory(x => this.ShowMessageBox(x, Strings.AppName,
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation));
            _folderBrowserDialog = new FolderSelectDialog
            {
                Title = Strings.BrowseFolderDescription,
            };
            _saveFileDialog = new SaveFileDialog
            {
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

        // --------- DIRS
        private void cmdFolderOpen_Click(object sender, EventArgs e)
        {
            var validate = _validationFactory.Create()
                .IsTrue(() => _presenter.Model.DataView.Count == 0, Strings.AlreadyOpenedFolderMessage)
                .IsTrue(() => _folderBrowserDialog.ShowDialog(), null);
            if (!validate.Validate())
            {
                return;
            }

            _presenter.OpenDirectory(_folderBrowserDialog.SelectedPath);
        }

        private void cmdFolderClose_Click(object sender, EventArgs e)
        {
            _presenter.CloseFolder();
        }

        private void cmdFolderRecent_Click(object sender, EventArgs e)
        {
            var settings = Settings.Default;
            var validate = _validationFactory.Create()
                .IsTrue(() => _presenter.Model.DataView.Count == 0, Strings.AlreadyOpenedFolderMessage)
                .IsTrue(() => !string.IsNullOrWhiteSpace(settings.LastOpenDirectory), Strings.OpenDirNoRecord)
                .IsTrue(() => System.IO.Directory.Exists(settings.LastOpenDirectory), Strings.OpenDirNotExist);
            if (!validate.Validate()) return;

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
            var validate = _validationFactory.Create()
                .IsTrue(() => _presenter.Model.DataView.Count > 0, Strings.FetchNoDataMessage)
                .EnsureInternetConnected();
            if (!validate.Validate()) return;

            _presenter.FetchMovieData();
        }

        private void mnuRenameAll_Click(object sender, EventArgs e)
        {
            var validate = _validationFactory.Create()
                .IsTrue(() => _presenter.Model.DataView.Count > 0, Strings.RenameNoDataMessage);
            if (!validate.Validate()) return;

            _presenter.RenameMovieFiles();
        }

        private void mnuDownloadAll_Click(object sender, EventArgs e)
        {
            var validate = _validationFactory.Create()
                .IsTrue(() => _presenter.Model.DataView.Count > 0, Strings.PosterNoDataMessage)
                .EnsureInternetConnected();
            if (!validate.Validate()) return;

            _presenter.DownloadMoviePoster();
        }

        private void mnuFolderThumbnail_Click(object sender, EventArgs e)
        {
            var validate = _validationFactory.Create()
                .IsTrue(() => _presenter.Model.DataView.Count > 0, Strings.ThumbnailNoDataMessage);
            if (!validate.Validate()) return;

            _presenter.ThumbnailFolder();
        }
        
        private void mnuSavePresistData_Click(object sender, EventArgs e)
        {
            var validate = _validationFactory.Create()
                .IsTrue(() => _presenter.Model.DataView.Count > 0, Strings.PresistNoDataMessage);
            if (!validate.Validate()) return;

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
            var validate = _validationFactory.Create().EnsureInternetConnected();
            if (!validate.Validate()) return;

            _presenter.ResearchMovie(grdMovies.CurrentRow.Index);
        }

        private void mnuFetchInclude_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            _presenter.SaveEntryId(grdMovies.CurrentRow.Index, Commons.NotFetchedEntryId);
            this.ShowMessageBox(Strings.ItemIncludedMessage, Strings.AppName);
        }

        private void mnuFetchIgnore_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            _presenter.SaveEntryId(grdMovies.CurrentRow.Index, Commons.IgnoredEntryId);
            this.ShowMessageBox(Strings.ItemExcludedMessage, Strings.AppName);
        }

        private void mnuSelectPoster_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            var currentId = _presenter.Model.DataView[grdMovies.CurrentRow.Index].TmdbId;
            var validate = _validationFactory.Create()
                .IsTrue(() => Commons.IsMovieFetched(currentId), Strings.PresistNoDataMessage)
                .EnsureInternetConnected();
            if (!validate.Validate()) return;

            _presenter.ShowSelectPosterWindow(grdMovies.CurrentRow.Index);
        }

        private void mnuAlternativeNames_Click(object sender, EventArgs e)
        {
            if (grdMovies.CurrentRow == null) return;
            var currentId = _presenter.Model.DataView[grdMovies.CurrentRow.Index].TmdbId;
            var validate = _validationFactory.Create()
                .IsTrue(() => Commons.IsMovieFetched(currentId), Strings.PresistNoDataMessage)
                .EnsureInternetConnected();
            if (!validate.Validate()) return;

            _presenter.ShowAlternativeNameWindow(grdMovies.CurrentRow.Index);
        }

        #endregion
    }
}
