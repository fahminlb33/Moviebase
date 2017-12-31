using CsvHelper.Configuration;
using Moviebase.Entities;

namespace Moviebase.Core.Contracts
{
    public sealed class CsvExportMap : ClassMap<MovieEntry>
    {
        public CsvExportMap()
        {
            Map(m => m.ImdbId);
            Map(m => m.Title);
            Map(m => m.Year);
            Map(m => m.Genre);
            Map(m => m.Plot);
        }
    }
}
