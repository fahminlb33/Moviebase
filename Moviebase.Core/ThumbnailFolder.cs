using System;
using System.Diagnostics;
using System.IO;
using ImageMagick;
using NLog;

namespace Moviebase.Core
{
    public class ThumbnailFolder : IThumbnailFolder
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        
        public void GenerateIcon(string inputFile)
        {
            try
            {
                var dirPath = Path.GetDirectoryName(inputFile);
                Debug.Assert(dirPath != null);

                var outputSquareFile = Path.Combine(dirPath, Commons.TempIconFileName);
                InternalGenerateSquareImage(inputFile, outputSquareFile);

                var outputIconFile = Path.Combine(dirPath, Commons.IconFileName);
                InternalGenerateIcon(outputSquareFile, outputIconFile);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error executing Magick.");
            }
        }

        public void WriteDesktopIni(string dirPath)
        {
            try
            {
                var iniPath = Path.Combine(dirPath, "desktop.ini");
                var iconPath = Path.Combine(dirPath, Commons.IconFileName);

                var ini = new IniFile(iniPath);
                ini.Write("ConfirmFileOp", "0", Commons.ShellClassInfoSection);
                ini.Write("IconFile", Commons.IconFileName, Commons.ShellClassInfoSection);
                ini.Write("IconIndex", "0", Commons.ShellClassInfoSection);
                ini.Write("IconResource", $"{Commons.IconFileName},0", Commons.ShellClassInfoSection);

                Commons.RunNoException(() => File.SetAttributes(iconPath, FileAttributes.Hidden | FileAttributes.System));
                Commons.RunNoException(() => File.SetAttributes(iniPath, FileAttributes.Hidden | FileAttributes.System));
                Commons.RunNoException(() => File.SetAttributes(dirPath, FileAttributes.System));
            }
            catch (Exception e)
            {
                _log.Error(e, "Error writing desktop.ini file.");
            }
        }

        public void RemoveThumbnail(string dirPath)
        {
            try
            {
                Commons.RunNoException(() => File.Delete(Path.Combine(dirPath, "icon.ico")));
                Commons.RunNoException(() => File.Delete(Path.Combine(dirPath, "desktop.ini")));
            }
            catch (Exception e)
            {
                _log.Error(e, "Error removing thumbnail info.");
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

        private void InternalGenerateIcon(string inputPath, string outputPath)
        {
            using (var image = new MagickImage(inputPath))
            {
                image.Format = MagickFormat.Icon;
                image.Write(outputPath);
            }
        }
    }
}
