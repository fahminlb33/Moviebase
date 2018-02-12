using System.Text;
using Moviebase.Core.Natives;

namespace Moviebase.Core.Diagnostics
{
    public class IniFile   // revision 11
    {
        readonly string _path;
        
        public IniFile(string iniPath = null)
        {
            _path = iniPath;
        }

        public string Read(string key, string section = "Main")
        {
            var retVal = new StringBuilder(255);
            NativeMethods.GetPrivateProfileString(section, key, "", retVal, 255, _path);
            return retVal.ToString();
        }

        public void Write(string key, string value, string section = "Main")
        {
            NativeMethods.WritePrivateProfileString(section, key, value, _path);
        }

        public void DeleteKey(string key, string section = "Main")
        {
            Write(key, null, section);
        }

        public void DeleteSection(string section = "Main")
        {
            Write(null, null, section);
        }

        public bool KeyExists(string key, string section = "Main")
        {
            return Read(key, section).Length > 0;
        }
    }
}
