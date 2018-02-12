using Moviebase.Entities;

namespace Moviebase.Core
{
    public interface IPersistFileManager
    {
        TmdbResult Load(string path);
        void Save(string outputPath, TmdbResult entry);
        bool HasPersistentData(string path);
    }
}