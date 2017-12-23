using System.Runtime.InteropServices;
using System.Security;
using System.Text;

// ReSharper disable InconsistentNaming

namespace Moviebase.Core.Native
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);
    }
}
