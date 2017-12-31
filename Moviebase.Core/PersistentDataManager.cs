using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Moviebase.Entities;
using Moviebase.Entities.Web;
using Newtonsoft.Json;
using NLog;

namespace Moviebase.Core
{
    public class PersistentDataManager : IPersistentDataManager
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly ITmdb _tmdb;

        public string[] FileExtensions { get; set; }
        public string PersistentFileName { get; set; }
        public bool HidePresistFile { get; set; }

        public PersistentDataManager(ITmdb tmdb)
        {
            _tmdb = tmdb;
        }

        public string SearchFirstFile(string dir)
        {
            try
            {
                var searcher = Directory.EnumerateFiles(dir, "*.*", SearchOption.TopDirectoryOnly);
                var path = searcher.FirstOrDefault(x => FileExtensions.Contains(Path.GetExtension(x)));
                return path;
            }
            catch (Exception e)
            {
                _log.Error(e, "Error finding first movie file.");
                return null;
            }
        }

        public string GetPosterUri(TmdbResult result, string dir, string filename)
        {
            var posterPath = Path.Combine(dir, filename);
            if (!File.Exists(posterPath))
            {
                _log.Debug("Using remote poster.");
                posterPath = _tmdb.GetPosterUrl(result.PosterPath, PosterSize.w154);
            }
            else
            {
                _log.Debug("Using local poster file.");
            }

            return posterPath;
        }

        public TmdbResult LoadData(string info)
        {
            var dirPath = Path.GetDirectoryName(info);
            Debug.Assert(dirPath != null);

            var persistFile = Path.Combine(dirPath, PersistentFileName);
            return HasPersistentData(dirPath)
                ? JsonConvert.DeserializeObject<TmdbResult>(File.ReadAllText(persistFile))
                : null;
        }

        public void SaveData(TmdbResult entry, string dirPath)
        {
            var persistFile = Path.Combine(dirPath, PersistentFileName);
            File.WriteAllText(persistFile, JsonConvert.SerializeObject(entry));
            if (HidePresistFile) File.SetAttributes(persistFile, FileAttributes.Hidden);
        }

        public bool HasPersistentData(string dirPath)
        {
            var persistFile = Path.Combine(dirPath, PersistentFileName);
            return File.Exists(persistFile);
        }
    }
}
