using Moviebase.Entities;

namespace Moviebase.Core
{
    public interface IPersistentDataManager
    {
        string[] FileExtensions { get; set; }
        string PersistentFileName { get; set; }
        bool HidePresistFile { get; set; }

        string SearchFirstFile(string dir);
        TmdbResult LoadData(string info);
        void SaveData(TmdbResult entry, string dirPath);
        bool HasPersistentData(string dirPath);
        string GetPosterUri(TmdbResult result, string dir, string filename);
    }
}