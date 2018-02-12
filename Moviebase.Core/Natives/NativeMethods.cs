using System.Runtime.InteropServices;
using System.Security;
using System.Text;

// ReSharper disable InconsistentNaming

namespace Moviebase.Core.Natives
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileStringW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WritePrivateProfileString([In] [MarshalAs(UnmanagedType.LPWStr)] string lpAppName, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpKeyName, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpString, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileStringW")]
        public static extern uint GetPrivateProfileString([In] [MarshalAs(UnmanagedType.LPWStr)] string lpAppName, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpKeyName, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpDefault, [Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpReturnedString, uint nSize, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpFileName);
    }
}
