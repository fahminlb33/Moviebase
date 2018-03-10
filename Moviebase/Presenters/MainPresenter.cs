using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Properties;
using Moviebase.Views;
using Moviebase.Entities;
using Moviebase.Core.MVP;
using Moviebase.Core.Natives;
using Moviebase.Core.Services;
using Moviebase.Models;
using Ninject;
using Ninject.Parameters;

namespace Moviebase.Presenters
{
    partial class MainPresenter : PresenterBase
    {
        private readonly StandardKernel _kernel = Program.AppKernel;
        private readonly Settings _settings = Settings.Default;
        private int _lastSelectedIndex;
        private List<string> _extensions;

        public MainModel Model { get; }
        public MainView View { get; }

        public MainPresenter(MainView view)
        {
            View = view;
            Model = _kernel.Get<MainModel>(new ConstructorArgument("context", SynchronizationContext.Current));
            _extensions = new List<string>(Settings.Default.MovieExtensions.Split(';'));
        }
        
        // -------- STOP
        public void StopProcess()
        {
            CancelTask();
        }

        // --------- TOOLS
        public void ShowSettingsWindow()
        {
            using (var vw = new SettingsView()) vw.ShowDialog();
        }

        public void ShowMoveMoviesWindow()
        {
            using (var vw = new MoveMoviesView()) vw.ShowDialog();
        }

        public void ShowAboutView()
        {
            using (var vw = new AboutView()) vw.ShowDialog();
        }

        // --------- DIRS
        public void OpenDirectory(string path)
        {
            RecreateCancellationToken();
            Task.Run(() => InternalOpenDirectory(path), CancellationToken.Token);
        }

        public void CloseFolder()
        {
            Model.DataView.Clear();
            ResetDetails();
        }

        // --------- ACTIONS
        public void FetchMovieData()
        {
            RecreateCancellationToken();
            Task.Run(() => InternalFetch(), CancellationToken.Token);
        }
        
        public void RenameMovieFiles()
        {
            RecreateCancellationToken();
            Task.Run(() => InternalRenameMovies(), CancellationToken.Token);
        }
        
        public void DownloadMoviePoster()
        {
            RecreateCancellationToken();
            Task.Run(() => InternalDownloadPoster(), CancellationToken.Token);
        }
        
        public void ThumbnailFolder()
        {
            RecreateCancellationToken();
            Task.Run(() => InternalThumbnailFolder(), CancellationToken.Token);
        }
        
        public void SavePresistData()
        {
            RecreateCancellationToken();
            Task.Run(() => InternalSavePresistData(), CancellationToken.Token);
        }
        
        public void ExportCsv(string path)
        {
            Task.Run(() => CsvExporter.ExportCsv(Model.DataView, path));
        }

        // -------- GRID VIEW
        public void ResearchMovie(int index)
        {
            var currentItem = (MovieEntry)Model.DataView[index].Clone();
            using (var vw = new ResearchMovieView(currentItem))
            {
                if (vw.ShowDialog() != DialogResult.OK) return;
                Model.DataView.SwapItem(x => x.FullPath == currentItem.FullPath, vw.SelectedEntry);
            }
        }

        public void SaveEntryId(int index, int id)
        {
            var manager = _kernel.Get<IPersistFileManager>();
            var entry = Model.DataView[index];
            entry.TmdbId = id;
            manager.Save(Path.GetDirectoryName(entry.FullPath), entry);
        }

        public void ShowSelectPosterWindow(int index)
        {
            var localEntry = Model.DataView[index];
            using (var vw = new SelectPosterView(localEntry.TmdbId.ToString()))
            {
                if (vw.ShowDialog() != DialogResult.OK) return;

                localEntry.PosterPath = vw.SelectedPath;
                _lastSelectedIndex = -1;
            }
        }

        public void ShowAlternativeNameWindow(int index)
        {
            var localEntry = Model.DataView[index];
            var result = View.ShowComboBoxInput(Strings.AlternativeNameTitle, localEntry.AlternativeNames, out string value);
            if (result != DialogResult.OK) return;

            localEntry.Title = value;
            Model.DataView.ResetBindings();
            _lastSelectedIndex = -1;
        }

        public void GridSelectionChanged(int index)
        {
            if (_lastSelectedIndex == index) return;

            var dataItem = Model.DataView[index];
            if (dataItem != null && Commons.IsMovieFetched(dataItem.TmdbId))
            {
                Model.LblTitleText = string.Format(Strings.MovieTitleInfoPattern, dataItem.Title, dataItem.Year);
                Model.LblExtraInfoText = string.Format(Strings.MovieExtraInfoPattern, dataItem.Genre, dataItem.ImdbId);
                Model.LblPlotText = dataItem.Plot;
                LoadImage(dataItem);
            }
            else
            {
                Model.LblTitleText = "No data.";
                Model.LblExtraInfoText = "No data.";
                Model.LblPlotText = "No data.";
                LoadImage(null);
            }
            _lastSelectedIndex = index;
        }

        public void GridCellFormatting(ref DataGridViewCellFormattingEventArgs e)
        {
            var item = Model.DataView[e.RowIndex];
            if (item == null) return;
            switch (item.TmdbId)
            {
                case Commons.NotFetchedEntryId:
                    e.CellStyle.BackColor = Color.Crimson;
                    break;

                case Commons.IgnoredEntryId:
                    e.CellStyle.BackColor = Color.Yellow;
                    break;
            }
        }

        public void ResetDetails()
        {
            Model.LblTitleText = Strings.LiteralThreeDots;
            Model.LblExtraInfoText = Strings.LiteralThreeDots;
            Model.LblPlotText = string.Empty;
            LoadImage(null);
        }
        
    }
}
