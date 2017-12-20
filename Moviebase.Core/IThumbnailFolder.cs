namespace Moviebase.Core
{
    public interface IThumbnailFolder
    {
        void GenerateIcon(string inputFile);
        void RemoveThumbnail(string dirPath);
        void WriteDesktopIni(string dirPath);
    }
}