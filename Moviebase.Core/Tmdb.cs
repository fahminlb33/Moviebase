using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Moviebase.Entities;
using Moviebase.Entities.Web;

namespace Moviebase.Core
{
    public class Tmdb : ITmdb, IDisposable
    {
        private readonly ITmdbWebRequest _webApi;

        public Tmdb(ITmdbWebRequest tmdbWebRequest)
        {
            _webApi = tmdbWebRequest;
        }

        #region ITmdb Implementation
        public async Task<List<string>> SearchMovies(string query, int year = 0)
        {
            var param = new NameValueCollection
            {
                {"query", Uri.EscapeDataString(query)},
                { "include_adult", "0"}
            };
            if (year > 0) param.Add("year", year.ToString());

            var response = await _webApi.GetRequestBody<MovieSearchRoot>(TmdbWebRequest.SearchMoviePath, param);
            return response.total_results <= 0 ? null : response.results.Select(x => x.id.ToString()).ToList();
        }

        public TmdbResult GetByFilename(string filename)
        {
            return new TmdbResult
            {
                Title = filename,
            };
        }

        public async Task<TmdbResult> GetByTmdbId(string id)
        {
            if (id == "-1") return null;
            var col = new NameValueCollection
            {
                { "append_to_response", "alternative_titles" }
            };

            // get
            var response = await _webApi.GetRequestBody<MovieDetailsRoot>(string.Format(TmdbWebRequest.MoviePath, id), col);
            var data = new TmdbResult
            {
                Id = response.id,
                Genre = string.Join(", ", response.genres.Select(x => x.name)),
                ImdbId = response.imdb_id,
                Plot = response.overview,
                Title = response.title,
                Year = DateTime.Parse(response.release_date).Year,

                AlternativeNames = ParseAlternatives(response.title, response.alternative_titles.Titles),
                PosterPath = response.poster_path
            };
            return data;
        }

        public async Task<TmdbResult> GetByImdbId(string id)
        {
            var col = new NameValueCollection
            {
                { "external_source", "imdb_id" }
            };

            var response = await _webApi.GetRequestBody<FindRoot>(string.Format(TmdbWebRequest.FindPath, id), col);
            if (response.movie_results != null && response.movie_results.Count > 0)
            {
                return await GetByTmdbId(response.movie_results.First().id.ToString());
            }
            return null;
        }

        public async Task<List<string>> GetPosterUris(string id)
        {
            var col = new NameValueCollection
            {
                { "include_image_language", "en,null" }
            };

            var response = await _webApi.GetRequestBody<PosterFindRoot>(string.Format(TmdbWebRequest.PostersPath, id), col);
            return response.posters.Select(x => x.file_path).ToList();
        }

        public string GetPosterUrl(string path, PosterSize size)
        {
            return _webApi.BuildPosterUrl(path, size);
        }
        
        #endregion

        private string[] ParseAlternatives(string title, IEnumerable<Title> titles)
        {
            var combined = new List<string> {title};
            combined.AddRange(titles.Select(x => x.title));
            return combined.ToArray();
        }

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _webApi?.Dispose();
            }
                
            _disposedValue = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
