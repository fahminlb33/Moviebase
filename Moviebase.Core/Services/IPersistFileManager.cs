using Moviebase.Entities;

namespace Moviebase.Core.Services
{
    public interface IPersistFileManager
    {
        MovieEntry Load(string path);
        void Save(string outputPath, MovieEntry entry);
        bool HasPersistentData(string path);
    }
}