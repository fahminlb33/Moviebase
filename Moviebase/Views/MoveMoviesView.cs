using System;
using System.Windows.Forms;
using BlastMVP;
using Moviebase.Presenters;

namespace Moviebase.Views
{
    public partial class MoveMoviesView : Form
    {
        private readonly MoveMoviesPresenter _presenter;

        public MoveMoviesView()
        {
            InitializeComponent();
            _presenter = new MoveMoviesPresenter(this);
            GlueBindings();
        }

        public void GlueBindings()
        {
            var model = _presenter.Model;
            dataGridView1.DataSource = model.DataView;

            btnBrowse.Bind(c => c.Enabled).To(model, m => m.CmdBrowseEnabled);
            cmdSearch.Bind(c => c.Text).To(model, m => m.CmdExecuteText);
            cmdSearch.Bind(c => c.Enabled).To(model, m => m.CmdExecuteEnabled);

            lblCount.Bind(c => c.Text).To(model, m => m.LblCountText);
            txtPath.Bind(c => c.Text).To(model, m => m.TxtBrowseText);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            _presenter.BrowseFolder();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            _presenter.SearchMovies();
        }
    }
}
