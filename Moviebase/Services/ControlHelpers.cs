using System.Drawing;
using System.Windows.Forms;

namespace Moviebase.Services
{
    public static class ControlHelpers
    {
        public static void ShowMessageBox(this Form ctl, string caption, MessageBoxIcon icon)
        {
            MessageBox.Show(caption, "Moviebase", MessageBoxButtons.OK, icon);
        }

        public static void ChangDataGridViewStyle(ref DataGridView dgv)
        {
            var font = new Font("Tahoma", 9.87F, FontStyle.Regular, GraphicsUnit.Point, 0);

            // control style
            dgv.Font = font;
            dgv.BackColor = Color.Black;
            dgv.ForeColor = Color.Maroon;
            dgv.BorderStyle = BorderStyle.None;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;

            // cell style
            dgv.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Yellow;

            // column header style
            dgv.ColumnHeadersHeight = 60;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Raised;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            // column header style
            dgv.ColumnHeadersDefaultCellStyle.Font = font;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // row style
            dgv.RowTemplate.Height = 25;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            // row color
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LemonChiffon;

            // column sizing
            dgv.Columns[0].Width = 200;
            dgv.Columns[1].Width = 300;
            dgv.Columns[2].Width = 200;
            dgv.Columns[3].Width = 200;
            dgv.Columns[4].Width = 200;
        }
    }
}
