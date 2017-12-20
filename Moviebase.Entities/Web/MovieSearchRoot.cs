using System.Collections.Generic;

// ReSharper disable InconsistentNaming
namespace Moviebase.Entities.Web
{
    public class MovieSearchRoot
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<MovieSearchResult> results { get; set; }
    }
}
