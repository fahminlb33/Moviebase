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

        public List<MovieEntry> Movies { get; set; }
        public string OutputPath { get; set; }

        public IEnumerable<Task> CreateTasks()
        {
            yield return Task.Run(() =>
            {
                try
                {
                    _log.Debug("Task started.");
                    using (var csvWriter = new CsvWriter(new StreamWriter(OutputPath, false)))
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
