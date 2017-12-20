using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class CsvExportWorker : WorkerBase, ICsvExportWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public List<MovieEntryFacade> Movies { get; set; }
        public string OutputPath { get; set; }

        public override void RunWorker()
        {
            Task.Run(() => InternalRunWorker(null));
        }

        protected override void InternalRunWorker(object arg)
        {
            _log.Debug("Task started.");
            OnRunWorkerStarted(this, EventArgs.Empty);

            try
            {
                OnProgressChanged(this, new ProgressChangedEventArgs(-1, null));
                using (var textStream = new StreamWriter(OutputPath, false))
                using (var csvWriter = new CsvWriter(textStream))
                {
                    csvWriter.Configuration.RegisterClassMap<CsvExportMap>();
                    csvWriter.WriteRecords(Movies);
                }

                _log.Debug("Task finished.");
                OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(null, null, false));
            }
            catch (Exception e)
            {
                _log.Error(e, "Task finished with error.");
                OnRunWorkerCompleted(this, new RunWorkerCompletedEventArgs(null, e, true));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Movies?.Clear();
                Movies = null;
            }
            base.Dispose(disposing);
        }
    }
}
