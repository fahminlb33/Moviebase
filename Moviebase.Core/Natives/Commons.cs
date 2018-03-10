using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace Moviebase.Core.Natives
{
    public static class Commons
    {
        public static Func<int, bool> IsMovieFetched = x => x > 0;
        public const int NotFetchedEntryId = -1;
        public const int IgnoredEntryId = -2;

        public const string TempFolderName = "moviebase";
        public const string PersistentFileName = "moviebase.persist";
        public const string IconFileName = "Folder.ico";
        public const string PosterFileName = "Folder.jpg";
        
        public const string Mp4FileExtension = ".mp4";
        public const string AllFilesSearchPattern = "*";
        public const string JpgSearchPattern = "*.jpg";

        public const string ShellClassInfoSection = ".ShellClassInfo";
        public const string TheName = "The";
        public const int MaxDegreeOfParallelism = 1;
        public const int TmdbWebRequestTries = 3;
        public const string PingServer = "8.8.8.8";
        public static readonly Func<Bitmap> DefaultImage = () => new Bitmap(1, 1);

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
