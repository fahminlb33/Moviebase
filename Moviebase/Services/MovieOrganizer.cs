using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Moviebase.Domain;
using Moviebase.Properties;
using Newtonsoft.Json;

namespace Moviebase.Services
{
    public class MovieOrganizer
    {
        public static string FindFirstFile(string dir)
        {
            var settings = Settings.Default;
            var searcher = Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly);
            return searcher.FirstOrDefault(x => settings.SupportedExtensions.Contains(Path.GetExtension(x)));
        }

        public MovieEntry LoadData(string dirPath)
        {
            var persistFile = Path.Combine(dirPath, "moviebase.persist");
            return HasPersistentData(dirPath)
                ? JsonConvert.DeserializeObject<MovieEntry>(File.ReadAllText(persistFile))
                : null;
        }

        public void SaveData(MovieEntry entry)
        {
            var persistFile = Path.Combine(entry.BasePath, "moviebase.persist");
            File.WriteAllText(persistFile, JsonConvert.SerializeObject(entry));
        }

        public bool HasPersistentData(string dirPath)
        {
            var persistFile = Path.Combine(dirPath, "moviebase.persist");
            return File.Exists(persistFile);
        }

        public IEnumerable<string> EnumerateDirectoryRated(string dir)
        {
            var ra = Directory.EnumerateDirectories(dir);
            foreach (var r in ra)
            {
                yield return r;
                Thread.Sleep(1000);
            }
        }
    }
}
