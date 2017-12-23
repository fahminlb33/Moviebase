using System.Collections.Generic;
using System.Threading.Tasks;
using Moviebase.Entities;
using Moviebase.Entities.Web;

namespace Moviebase.Core
{
    public interface ITmdb
    {
        Task<List<string>> SearchMovies(string query, int year);

        Task<TmdbResult> GetByImdbId(string id);
        Task<TmdbResult> GetByTmdbId(string id);
        Task<TmdbResult> GetByFilename(string filename);

        Task<List<string>> GetPosterUris(string id);
        string GetPosterUrl(string path, PosterSize size);
    }
}