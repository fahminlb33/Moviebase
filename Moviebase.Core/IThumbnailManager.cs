namespace Moviebase.Core
{
    public interface IThumbnailManager
    {
        void CreateThumbnail(string path);
        void RemoveThumbnail(string path);
    }
}