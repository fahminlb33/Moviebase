using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Moviebase.Entities;

namespace Moviebase.Views
{
    public partial class AboutView : Form
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void AboutView_Load(object sender, EventArgs e)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = FileVersionInfo.GetVersionInfo(assembly.Location);

            var versionPart = version.FileVersion.Split('.');
            lblVersion.Text = string.Format(StringResources.VersionStringPattern, versionPart[0], versionPart[1], versionPart[2], versionPart[3]);
            button1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
