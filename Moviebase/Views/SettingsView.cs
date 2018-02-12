using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Core;
using Moviebase.Core.Diagnostics;
using Moviebase.Core.MVP;
using Moviebase.Entities;
using Moviebase.Properties;
using Ninject;

namespace Moviebase.Views
{
    public partial class SettingsView : Form
    {
        private static readonly MovieEntry TryitData = new MovieEntry(new TmdbResult
        {
            Genre = "Science Fiction",
            Id = 1134,
            ImdbId = "tt172845369",
            Plot = "There is...",
            Title = "Star Trek Beyond",
            Year = 2017
        }, null);

        public SettingsView()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            var appSettings = Settings.Default;

            appSettings.TmdbApiKey = txtTmdbApiKey.Text;
            appSettings.LastOpenDirectory = txtLastOpenDir.Text;
            appSettings.SwapThe = chkSwapThe.Checked;
            appSettings.OverwritePoster = chkOverwritePoster.Checked;
            appSettings.HidePresistFile = chkHidePresist.Checked;

            appSettings.FileRenamePattern = txtFileRenamePattern.Text;
            appSettings.FolderRenamePattern = txtFolderRenamePattern.Text;

            appSettings.MovieExtensions.Clear();
            appSettings.MovieExtensions.AddRange(lstExtensions.Items.Cast<string>().ToArray());
            
            appSettings.Save();
            this.ShowMessageBox(Strings.SettingsRestartMessage, Strings.AppName);
            DialogResult = DialogResult.OK;
        }

        private void SettingsView_Load(object sender, EventArgs e)
        {
            var appSettings = Settings.Default;

            txtTmdbApiKey.Text = appSettings.TmdbApiKey;
            txtLastOpenDir.Text = appSettings.LastOpenDirectory;
            chkSwapThe.Checked = appSettings.SwapThe;
            chkOverwritePoster.Checked = appSettings.OverwritePoster;
            chkHidePresist.Checked = appSettings.HidePresistFile;

            txtFileRenamePattern.Text = appSettings.FileRenamePattern;
            txtFolderRenamePattern.Text = appSettings.FolderRenamePattern;

            lstExtensions.Items.AddRange(appSettings.MovieExtensions.Cast<object>().ToArray());
        }

        private void cmdClearLastDir_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtLastOpenDir.Text = "";
        }

        private void cmdDetect_Click(object sender, EventArgs e)
        {
            cmdDetect.Enabled = false;
            picPython.Image = Resources.cross;
            picGuessit.Image = Resources.cross;

            Task.Run(async () =>
            {
                try
                {
                    var comp = Program.AppKernel.Get<IComponentManager>();

                    var python = await comp.IsPythonInstalled();
                    Invoke(new Action(() => picPython.Image = python ? Resources.tick : Resources.cross));

                    var guessit = await comp.IsGuessItInstalled();
                    Invoke(new Action(() => picGuessit.Image = guessit ? Resources.tick : Resources.cross));
                }
                catch (Exception er)
                {
                    Debug.Print(er.ToString());
                }
            }).ContinueWith(x => Invoke(new Action(() => cmdDetect.Enabled = true)));
        }

        private void txtFileRenamePattern_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblPatternOutput.Text = Commons.PatternRename(txtFileRenamePattern.Text, TryitData) + Commons.Mp4FileExtension;
            }
            catch (Exception exception)
            {
                Debug.Print(exception.Message);
                lblPatternOutput.Text = Strings.InvalidPatternText;
            }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            var ext = txtExt.Text;
            if (string.IsNullOrEmpty(ext))
            {
                this.ShowMessageBox(Strings.NoExtensionToAddMessage, Strings.AppName);
                return;
            }
            if (!ext.StartsWith(".", StringComparison.Ordinal)) ext = "." + ext;
            lstExtensions.Items.Add(Commons.StripWhitespace(ext));
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        { 
            if (lstExtensions.SelectedItem == null) return;
            lstExtensions.Items.Remove(lstExtensions.SelectedItem);
        }

        private void lstExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var a = lstExtensions.SelectedItem.ToString();
            if(!string.IsNullOrEmpty(a))
            txtExt.Text = a;
        }
    }
}
