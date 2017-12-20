using System.Collections.Generic;
using Moviebase.Entities;
using Moviebase.Entities.Web;

namespace Moviebase.Core
{
    public interface ITmdb
    {
        List<string> SearchMovies(string query, int year);

        TmdbResult GetByImdbId(string id);
        TmdbResult GetByTmdbId(string id);
        TmdbResult GetByFilename(string filename);

        string[] GetPosterUris(string id);
        string GetPosterUrl(string path, PosterSize size);
    }
}