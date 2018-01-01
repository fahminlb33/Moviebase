using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace Moviebase.Core
{
    public class Commons
    {
        public const string TempFolderName = "moviebase";
        public const string PersistentFileName = "moviebase.persist";
        public const string ExportFileName = "moviebase.csv";
        public const string IconFileName = "icon.ico";

        public const string JpgFileExtension = ".jpg";
        public const string Mp4FileExtension = ".mp4";
        public const string AllFilesSearchPattern = "*";
        public const string JpgSearchPattern = "*.jpg";

        public const string ShellClassInfoSection = ".ShellClassInfo";
        public const string TheName = "The";
        public const int MaxDegreeOfParallelism = 1;
        public const int TmdbWebRequestTries = 3;
        public const string PingServer = "8.8.8.8";
        
        public static readonly Bitmap DefaultImage = new Bitmap(1, 1);
        private static readonly string[] SizeSuffix = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

        [Obsolete]
        public static string BytesToString(long byteCount)
        {
            if (byteCount == 0) return $"0{SizeSuffix[0]}";
            var bytes = Math.Abs(byteCount);
            var place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            var num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return $"{Math.Sign(byteCount) * num}{SizeSuffix[place]}";
        }

        public static void RunNoException(Action act)
        {
            try
            {
                act?.Invoke();
            }
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
        }

        public static string StripWhitespace(string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }

        public static string PatternRename(string pattern, object values)
        {
            var sb = new StringBuilder(pattern);
            foreach (var prop in values.GetType().GetProperties())
            {
                if (!pattern.Contains(prop.Name)) continue;
                sb.Replace("{" + prop.Name + "}", prop.GetValue(values, null).ToString());
            }

            return sb.ToString();
        }
    }
}
