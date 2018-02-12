using System;
using System.Diagnostics;
using System.IO;
using ImageMagick;
using Moviebase.Core.Diagnostics;

namespace Moviebase.Core
{
    public class ThumbnailManager : IThumbnailManager
    {
        public void CreateThumbnail(string path)
        {
            try
            {
                var posterPath = Path.Combine(path, Commons.PosterFileName);
                if (!File.Exists(posterPath)) return;

                GenerateIcon(posterPath);
                WriteDesktopIni(path);
            }
            catch (Exception e)
            {
                Debug.Print("Thumbnail creation error: {0}. {1}", path, e.Message);
            }
        }

        public void RemoveThumbnail(string path)
        {
            Commons.RunNoException(() => File.Delete(Path.Combine(path, "icon.ico")));
            Commons.RunNoException(() => File.Delete(Path.Combine(path, "desktop.ini")));
        }

        #region Private Methods
        private void GenerateIcon(string inputFile)
        {
            try
            {
                var dirPath = Path.GetDirectoryName(inputFile);
                Debug.Assert(dirPath != null);

                var outputSquareFile = Path.Combine(dirPath, Commons.IconFileName);
                InternalGenerateSquareImage(inputFile, outputSquareFile);
            }
            catch (Exception e)
            {
                Debug.Print("Magick error: {0}. {1}", inputFile, e.Message);
            }
        }

        private void WriteDesktopIni(string path)
        {
            try
            {
                var iniPath = Path.Combine(path, "desktop.ini");
                var iconPath = Path.Combine(path, Commons.IconFileName);

                var ini = new IniFile(iniPath);
                ini.Write("ConfirmFileOp", "0", Commons.ShellClassInfoSection);
                ini.Write("IconFile", Commons.IconFileName, Commons.ShellClassInfoSection);
                ini.Write("IconIndex", "0", Commons.ShellClassInfoSection);
                ini.Write("IconResource", $"{Commons.IconFileName},0", Commons.ShellClassInfoSection);

                Commons.RunNoException(() => File.SetAttributes(iconPath, FileAttributes.Hidden | FileAttributes.System));
                Commons.RunNoException(() => File.SetAttributes(iniPath, FileAttributes.Hidden | FileAttributes.System));
                Commons.RunNoException(() => File.SetAttributes(path, FileAttributes.System));
            }
            catch (Exception e)
            {
                Debug.Print("Error writing desktop.ini file: {0}. {1}", path, e.Message);
            }
        }

        private void InternalGenerateSquareImage(string inputPath, string outputPath)
        {
            using (var image = new MagickImage(inputPath))
            {
                image.VirtualPixelMethod = VirtualPixelMethod.Background;
                image.BackgroundColor = MagickColors.White;

                // square out
                int size = Math.Max(image.Width, image.Height);
                int x = -Math.Max((image.Height - image.Width) / 2, 0);
                int y = -Math.Max((image.Width - image.Height) / 2, 0);
                var geometry = new MagickGeometry(x, y, size, size);
                image.SetArtifact("distort:viewport", geometry.ToString());

                // filter
                image.FilterType = FilterType.Point;
                image.Distort(DistortMethod.ScaleRotateTranslate, 0);

                image.RePage();
                image.Resize(new MagickGeometry(256, 256));
                image.Write(outputPath);
            }
        }
        #endregion
    }
}
