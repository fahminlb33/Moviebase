using System;

namespace Moviebase.Core.Natives
{
    /// <summary>
    /// Creates IWin32Window around an IntPtr
    /// </summary>
    public class WindowWrapper : System.Windows.Forms.IWin32Window
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="handle">Handle to wrap</param>
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        /// <summary>
        /// Original ptr
        /// </summary>
        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
}
