using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using NLog;

namespace Moviebase.Core.Workers
{
    public class MoveMoviesWorker : IMoveMovieWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public string AnalyzePath { get; set; }
        public List<string> FileExtensions { get; set; }
 
        public IEnumerable<Task<string>> CreateTasks()
        {
            var dirEnumbEnumerable = Directory.EnumerateFiles(AnalyzePath, "*", SearchOption.TopDirectoryOnly);
            foreach (var dirPath in dirEnumbEnumerable)
            {
                yield return Task.Run(() =>
                {
                    try
                    {
                        _log.Info("Processing: " + dirPath);
                        using (var path = new PowerPath(dirPath))
                        {
                            if (!FileExtensions.Contains(path.GetExtension())) return null;

                            var newDir = Path.Combine(path.GetDirectoryPath(), path.GetFileNameWithoutExtension());
                            Directory.CreateDirectory(newDir);

                            var newFile = Path.Combine(newDir, path.GetFileName());
                            File.Move(path.GetFullPath(), newFile);

                            _log.Info("Processed: " + dirPath);
                            return newFile;
                        }
                    }
                    catch (Exception e)
                    {
                        _log.Error(e, "Error processing: " + dirPath);
                        return null;
                    }
                });
            }
        }
    }
}
