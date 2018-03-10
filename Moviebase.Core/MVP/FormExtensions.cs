using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
// ReSharper disable LocalizableElement

namespace Moviebase.Core.MVP
{
    public static class FormExtensions
    {
        public static int FindIndex<T>(this IList<T> list, Predicate<T> predicate)
        {
            int i = 0;
            foreach (var item in list)
            {
                if (predicate(item)) return i;
                i++;
            }
            return -1;
        }

        public static void SwapItem<T>(this IList<T> list, Predicate<T> predicate, T item)
        {
            var index = FindIndex(list, predicate);
            list.RemoveAt(index);
            list.Insert(index, item);
        }

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
