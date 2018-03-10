using System.Diagnostics;
using System.IO;
using Moviebase.Core.Natives;
using Moviebase.Entities;
using Newtonsoft.Json;

namespace Moviebase.Core.Services
{
    public class PersistFileManager : IPersistFileManager
    {
        private bool _hideFile;

        public PersistFileManager(bool hideFile)
        {
            _hideFile = hideFile;
        }
        
        public MovieEntry Load(string path)
        {
            var dirPath = Path.GetDirectoryName(path);
            Debug.Assert(dirPath != null);
            if (!HasPersistentData(dirPath)) return null;

            var persistFile = Path.Combine(dirPath, Commons.PersistentFileName);
            var contents = File.ReadAllText(persistFile);
            var serialized = JsonConvert.DeserializeObject<MovieEntry>(contents);
            serialized.SetFullPath(path);
            return serialized;
        }

        public void Save(string dirPath, MovieEntry entry)
        {
            var path = Path.Combine(dirPath, Commons.PersistentFileName);
            if (File.Exists(path)) File.SetAttributes(path, FileAttributes.Normal);
            File.WriteAllText(path, JsonConvert.SerializeObject(entry));
            if (_hideFile) File.SetAttributes(path, FileAttributes.Hidden); 
        }

        public bool HasPersistentData(string path)
        {
            var persistFile = Path.Combine(path, Commons.PersistentFileName);
            return File.Exists(persistFile);
        }
    }
}
