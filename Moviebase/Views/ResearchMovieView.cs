using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Core.MVP;
using Moviebase.Core.Services;
using Moviebase.Entities;
using Ninject;

namespace Moviebase.Views
{
    public partial class ResearchMovieView : Form
    {
        private MovieEntry _oldEntry;

        public MovieEntry SelectedEntry { get; set; }

        public ResearchMovieView(MovieEntry entry)
        {
            InitializeComponent();
            _oldEntry = entry;
        }

        public async void SearchTask(string title, int year)
        {
            var tmdb = Program.AppKernel.Get<ITmdb>();

            try
            {
                // find title
                var foundTitles = new Dictionary<string, TmdbResult>();
                var ids = await tmdb.SearchMovies(title, year);
                foreach (var id in ids)
                {
                    var detail = await tmdb.GetByTmdbId(id);
                    foundTitles.Add(detail.TmdbId.ToString(), detail);
                }
                Invoke(new Action(() => comboBox1.DataSource = foundTitles.Values.ToArray()));
            }
            catch (Exception e)
            {
                Debug.Print("Unable to retrieve." + e);
                Invoke(new Action(() => this.ShowMessageBox("No movie record found!", Strings.AppName)));
            }

            // show selection
            Invoke(new Action(() =>
            {
                cmdFind.Enabled = true;
                cmdOK.Enabled = true;
                comboBox1.Enabled = true;
            }));
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            var selected = (TmdbResult) comboBox1.SelectedItem;
            SelectedEntry = new MovieEntry();
            SelectedEntry.SetData(_oldEntry);
            SelectedEntry.SetData(selected);
            DialogResult = DialogResult.OK;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void cmdFind_Click(object sender, EventArgs e)
        {
            Task.Run(() => SearchTask(textBox1.Text, (int) numericUpDown1.Value));
            cmdFind.Enabled = false;
            cmdOK.Enabled = false;
            comboBox1.Enabled = false;
        }
    }
}
