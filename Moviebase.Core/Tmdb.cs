using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Moviebase.Entities;
using Moviebase.Entities.Web;

namespace Moviebase.Core
{
    public class Tmdb : ITmdb, IDisposable
    {
        private const string ApiEndpoint = "https://api.themoviedb.org/3";
        private const string PosterEndPoint = "https://image.tmdb.org/t/p/";

        private readonly UriBuilderHelper _uriBuilder;
        private readonly HttpWebRequester _webApi;

        public Tmdb(string apiKey)
        {
            _uriBuilder = new UriBuilderHelper(ApiEndpoint, apiKey);
            _webApi = new HttpWebRequester();
        }

        #region ITmdb Implementation
        public List<string> SearchMovies(string query, int year = 0)
        {
            var param = new NameValueCollection
            {
                {"query", Uri.EscapeDataString(query)},
                { "include_adult", "0"}
            };
            if (year > 0) param.Add("year", year.ToString());

            var uri = _uriBuilder.BuildUri("/search/movie", param);
            var response = _webApi.GetRequestBody<MovieSearchRoot>(uri);

            return response.total_results <= 0 ? null : response.results.Select(x => x.id.ToString()).ToList();
        }

        public TmdbResult GetByFilename(string filename)
        {
            return new TmdbResult
            {
                Title = filename,
            };
        }

        public TmdbResult GetByTmdbId(string id)
        {
            if (id == "-1") return null;
            var col = new NameValueCollection
            {
                { "append_to_response", "alternative_titles" }
            };
            var uri = _uriBuilder.BuildUri($"/movie/{id}", col);
            var response = _webApi.GetRequestBody<MovieDetailsRoot>(uri);

            // parse
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

        public TmdbResult GetByImdbId(string id)
        {
            var col = new NameValueCollection
            {
                { "external_source", "imdb_id" }
            };
            var uri = _uriBuilder.BuildUri($"/find/{id}", col);
            var response = _webApi.GetRequestBody<FindRoot>(uri);

            if (response.movie_results != null && response.movie_results.Count > 0)
            {
                return GetByTmdbId(response.movie_results.First().id.ToString());
            }
            return null;
        }

        public string[] GetPosterUris(string id)
        {
            var col = new NameValueCollection
            {
                { "include_image_language", "en,null" }
            };
            var uri = _uriBuilder.BuildUri($"/movie/{id}/images", col);
            var response = _webApi.GetRequestBody<PosterFindRoot>(uri);

            return response.posters.Select(x => x.file_path).ToArray();
        }

        public string GetPosterUrl(string path, PosterSize size)
        {
            return PosterEndPoint + size + path;
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
