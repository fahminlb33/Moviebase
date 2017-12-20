using System.Drawing;
using System.Windows.Forms;

namespace BlastMVP
{
    public static class FormExtensions
    {
        public static DialogResult ShowMessageBox(this Form frm, string message, string title = "",
            MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Asterisk)
        {
            return MessageBox.Show(frm, message, title, buttons, icon);
        }

        public static DialogResult ShowInputBox(this Form frm, string message, string title, out string value)
        {
            var size = new Size(200, 70);
            var inputForm = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                ClientSize = size,
                Text = title,
                MinimizeBox = false,
                MaximizeBox = false
            };
            using (inputForm)
            {
                var textBox = new TextBox
                {
                    Size = new Size(size.Width - 10,23),
                    Location = new Point(5,5),
                };
                inputForm.Controls.Add(textBox);

                var okButton = new Button
                {
                    DialogResult = DialogResult.OK,
                    Name = "okButton",
                    Size = new Size(75, 23),
                    Text = "&OK",
                    Location = new Point(size.Width - 80 - 80, 39),
                };
                inputForm.Controls.Add(okButton);

                var cancelButton = new Button
                {
                    DialogResult = DialogResult.Cancel,
                    Name = "cancelButton",
                    Size = new Size(75, 23),
                    Text = "&Cancel",
                    Location = new Point(size.Width - 80, 39),
                };
                inputForm.Controls.Add(cancelButton);

                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;
                inputForm.StartPosition = FormStartPosition.CenterParent;

                var result = inputForm.ShowDialog(frm);
                value = textBox.Text;
                return result;
            }
        }

        public static DialogResult ShowComboBoxInput(this Form frm, string title, string[] items, out string value)
        {
            var size = new Size(200, 70);
            var inputForm = new Form
            {
                FormBorderStyle = FormBorderStyle.FixedDialog,
                ClientSize = size,
                Text = title,
                MinimizeBox = false,
                MaximizeBox = false
            };
            using (inputForm)
            {
                var comboBox = new ComboBox()
                {
                    Size = new Size(size.Width - 10, 23),
                    Location = new Point(5, 5),
                    DataSource = items,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                inputForm.Controls.Add(comboBox);

                var okButton = new Button
                {
                    DialogResult = DialogResult.OK,
                    Name = "okButton",
                    Size = new Size(75, 23),
                    Text = "&OK",
                    Location = new Point(size.Width - 80 - 80, 39),
                };
                inputForm.Controls.Add(okButton);

                var cancelButton = new Button
                {
                    DialogResult = DialogResult.Cancel,
                    Name = "cancelButton",
                    Size = new Size(75, 23),
                    Text = "&Cancel",
                    Location = new Point(size.Width - 80, 39),
                };
                inputForm.Controls.Add(cancelButton);

                inputForm.AcceptButton = okButton;
                inputForm.CancelButton = cancelButton;
                inputForm.StartPosition = FormStartPosition.CenterParent;

                var result = inputForm.ShowDialog();
                value = comboBox.Text;
                return result;
            }
        }
    }
}
