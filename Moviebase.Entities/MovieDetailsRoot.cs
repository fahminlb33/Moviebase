using System.Collections.Generic;

// ReSharper disable InconsistentNaming
namespace Moviebase.Entities
{
    public class MovieDetailsRoot
    {
        public List<Genre> genres { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string overview { get; set; }
        public string poster_path { get; set; }
        public string release_date { get; set; }
        public string title { get; set; }
        public AlternativeTitles alternative_titles { get; set; }
    }
}
