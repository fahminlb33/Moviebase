using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using Moviebase.Core.Contracts;
using NLog;

namespace Moviebase.Core.Workers
{
    public class MoveMoviesWorker : WorkerBase, IMoveMovieWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public string AnalyzePath { get; set; }
        public List<string> FileExtensions { get; set; }
        
        public override void RunWorker()
        {
            Task.Run(() =>
            {
                _log.Debug("Task started.");
                OnRunWorkerStarted(this, EventArgs.Empty);

                try
                {
                    var options = new ParallelOptions
                    {
                        CancellationToken = CancellationToken.Token,
                        MaxDegreeOfParallelism = Commons.MaxDegreeOfParallelism
                    };

                    var dirEnumbEnumerable = Directory.EnumerateFiles(AnalyzePath, "*", SearchOption.TopDirectoryOnly);
                    Parallel.ForEach(dirEnumbEnumerable, options, InternalRunWorker);

                    _log.Debug("Task finished.");
                    OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(null, null, false));
                }
                catch (Exception e)
                {
                    _log.Error(e, "Task finished with error.");
                    OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(null, e, true));
                }
            });
        }

        protected override void InternalRunWorker(object arg)
        {
            try
            {
                _log.Info("Processing: " + arg);
                using (var path = new PowerPath(arg.ToString()))
                {
                    if (!FileExtensions.Contains(path.GetExtension())) return;

                    var newDir = Path.Combine(path.GetDirectoryPath(), path.GetFileNameWithoutExtension());
                    Directory.CreateDirectory(newDir);

                    var newFile = Path.Combine(newDir, path.GetFileName());
                    File.Move(path.GetFullPath(), newFile);

                    _log.Info("Processed: " + arg);
                    OnProgressChanged(this, new ProgressChangedEventArgs(-1, newFile));
                }
            }
            catch (Exception e)
            {
                _log.Error(e, "Error processing: " + arg);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                FileExtensions?.Clear();
                FileExtensions = null;
            }
            base.Dispose(disposing);
        }
    }
}
