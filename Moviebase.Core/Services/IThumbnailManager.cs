namespace Moviebase.Core.Services
{
    public interface IThumbnailManager
    {
        void CreateThumbnail(string path);
        void RemoveThumbnail(string path);
    }
}