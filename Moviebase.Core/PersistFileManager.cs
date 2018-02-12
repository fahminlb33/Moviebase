using System.Diagnostics;
using System.IO;
using Moviebase.Entities;
using Newtonsoft.Json;

namespace Moviebase.Core
{
    public class PersistFileManager : IPersistFileManager
    {
        private bool _hideFile;

        public PersistFileManager(bool hideFile)
        {
            _hideFile = hideFile;
        }
        
        public TmdbResult Load(string path)
        {
            var dirPath = Path.GetDirectoryName(path);
            Debug.Assert(dirPath != null);

            var persistFile = Path.Combine(dirPath, Commons.PersistentFileName);
            return HasPersistentData(dirPath)
                ? JsonConvert.DeserializeObject<TmdbResult>(File.ReadAllText(persistFile))
                : null;
        }

        public void Save(string dirPath, TmdbResult entry)
        {
            var persistFile = Path.Combine(dirPath, Commons.PersistentFileName);
            File.WriteAllText(persistFile, JsonConvert.SerializeObject(entry));
            if (_hideFile) File.SetAttributes(persistFile, FileAttributes.Hidden);
        }

        public bool HasPersistentData(string path)
        {
            var persistFile = Path.Combine(path, Commons.PersistentFileName);
            return File.Exists(persistFile);
        }
    }
}
