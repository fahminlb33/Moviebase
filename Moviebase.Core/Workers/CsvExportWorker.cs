using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using Moviebase.Core.Contracts;
using Moviebase.Entities;
using NLog;

namespace Moviebase.Core.Workers
{
    public class CsvExportWorker : ICsvExportWorker
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();

        public List<MovieEntryFacade> Movies { get; set; }
        public string OutputPath { get; set; }

        public IEnumerable<Task> CreateTasks()
        {
            yield return new Task(() =>
            {
                _log.Debug("Task started.");

                try
                {
                    using (var textStream = new StreamWriter(OutputPath, false))
                    using (var csvWriter = new CsvWriter(textStream))
                    {
                        csvWriter.Configuration.RegisterClassMap<CsvExportMap>();
                        csvWriter.WriteRecords(Movies);
                    }

                    _log.Debug("Task finished.");
                }
                catch (Exception e)
                {
                    _log.Error(e, "Task finished with error.");
                }
            });
        }
    }
}
