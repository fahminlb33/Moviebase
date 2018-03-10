using CsvHelper.Configuration;
using Moviebase.Entities;

namespace Moviebase.Core.Services
{
    public sealed class CsvExportMap : ClassMap<MovieEntry>
    {
        public CsvExportMap()
        {
            Map(m => m.Title);
            Map(m => m.Quality);
            Map(m => m.Size);
            Map(m => m.Year);
            Map(m => m.Subtitle);
            Map(m => m.Genre);
            Map(m => m.Source);
            Map(m => m.Plot);
        }
    }
}
