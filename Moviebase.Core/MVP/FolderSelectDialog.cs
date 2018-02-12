using System;
using System.Windows.Forms;
using Moviebase.Core.Natives;

namespace Moviebase.Core.MVP
{
    /// <summary>
    /// Wraps System.Windows.Forms.OpenFileDialog to make it present
    /// a vista-style dialog.
    /// </summary>
    public class FolderSelectDialog
    {
        // Wrapped dialog
        private readonly OpenFileDialog _ofd;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FolderSelectDialog()
        {
            _ofd = new OpenFileDialog
            {
                // ReSharper disable once LocalizableElement
                Filter = "Folders|\n",
                AddExtension = false,
                CheckFileExists = false,
                DereferenceLinks = true,
                Multiselect = false
            };

        }

        #region Properties

        /// <summary>
        /// Gets/Sets the initial folder to be selected. A null value selects the current directory.
        /// </summary>
        public string InitialDirectory
        {
            get => _ofd.InitialDirectory;
            set => _ofd.InitialDirectory = string.IsNullOrEmpty(value) ? Environment.CurrentDirectory : value;
        }

        /// <summary>
        /// Gets/Sets the title to show in the dialog
        /// </summary>
        public string Title
        {
            get => _ofd.Title;
            set => _ofd.Title = value ?? "Select a folder";
        }

        /// <summary>
        /// Gets the selected folder
        /// </summary>
        public string SelectedPath => _ofd.FileName;

        #endregion

        #region Methods

        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <returns>True if the user presses OK else false</returns>
        public bool ShowDialog()
        {
            return ShowDialog(IntPtr.Zero);
        }

        /// <summary>
        /// Shows the dialog
        /// </summary>
        /// <param name="hWndOwner">Handle of the control to be parent</param>
        /// <returns>True if the user presses OK else false</returns>
        public bool ShowDialog(IntPtr hWndOwner)
        {
            bool flag;

            if (Environment.OSVersion.Version.Major >= 6)
            {
                var r = new Reflector("System.Windows.Forms");

                uint num = 0;
                var typeIFileDialog = r.GetType("FileDialogNative.IFileDialog");
                var dialog = r.Call(_ofd, "CreateVistaDialog");
                r.Call(_ofd, "OnBeforeVistaDialog", dialog);

                var options = (uint)r.CallAs(typeof(FileDialog), _ofd, "GetOptions");
                options |= (uint)r.GetEnum("FileDialogNative.FOS", "FOS_PICKFOLDERS");
                r.CallAs(typeIFileDialog, dialog, "SetOptions", options);

                var pfde = r.New("FileDialog.VistaDialogEvents", _ofd);
                object[] parameters = { pfde, num };
                r.CallAs2(typeIFileDialog, dialog, "Advise", parameters);
                num = (uint)parameters[1];
                try
                {
                    var num2 = (int)r.CallAs(typeIFileDialog, dialog, "Show", hWndOwner);
                    flag = 0 == num2;
                }
                finally
                {
                    r.CallAs(typeIFileDialog, dialog, "Unadvise", num);
                    GC.KeepAlive(pfde);
                }
            }
            else
            {
                var fbd = new FolderBrowserDialog
                {
                    Description = Title,
                    SelectedPath = InitialDirectory,
                    ShowNewFolderButton = false
                };
                if (fbd.ShowDialog(new WindowWrapper(hWndOwner)) != DialogResult.OK) return false;
                _ofd.FileName = fbd.SelectedPath;
                flag = true;
            }

            return flag;
        }

        #endregion
    }
}
