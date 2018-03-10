using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CsvHelper;

namespace Moviebase.Core.Services
{
    public static class CsvExporter
    {
        public static void ExportCsv<T>(IEnumerable<T> list, string outputPath)
        {
            try
            {
                using (var csvWriter = new CsvWriter(new StreamWriter(outputPath, false)))
                {
                    csvWriter.Configuration.RegisterClassMap<CsvExportMap>();
                    csvWriter.WriteRecords(list);
                }
            }
            catch (Exception e)
            {
                Debug.Print("Unable to write all records: " + e.Message);
            }
        }
    }
}
