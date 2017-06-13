using Moviebase.Domain;

namespace Moviebase.Services
{
    public interface ITmdb
    {
        MovieEntry GetByImdbId(string id);
        MovieEntry GetByTmdbId(int id);
        string GetPosterUri(string path, PosterSize size);
        int SearchFirstMovieId(string query, int year);
    }
}