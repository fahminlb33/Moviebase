using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Moviebase.Domain;
using Moviebase.Properties;

namespace Moviebase.Services
{
    public class Tmdb : ITmdb
    {
        private const string ApiEndpoint = "https://api.themoviedb.org/3";
        private const string PosterEndPoint = "https://image.tmdb.org/t/p/";
        private readonly TmdbUriBuilder _uriBuilder;

        public Tmdb()
        {
            _uriBuilder = new TmdbUriBuilder(ApiEndpoint, Settings.Default.ApiKey);
        }

        public int SearchFirstMovieId(string query, int year)
        {
            var param = new NameValueCollection
            {
                {"query", Uri.EscapeDataString(query)},
                { "include_adult", "0"}
            };
            if (year > 0) param.Add("year", year.ToString());

            var uri = _uriBuilder.BuildUri("/search/movie", param);
            var result = HttpHelper.GetRequestBody<MovieSearchRoot>(uri);

            if (result.total_results <= 0) return -1;
            return result.results.First().id;
        }

        public MovieEntry GetByTmdbId(int id)
        {
            var col = new NameValueCollection
            {
                { "append_to_response", "alternative_titles" }
            };
            var uri = _uriBuilder.BuildUri($"/movie/{id}", col);
            var result = HttpHelper.GetRequestBody<MovieDetailsRoot>(uri);

            // parse
            var data = new MovieEntry
            {
                Id = result.id,
                Genre = string.Join(", ", result.genres.Select(x => x.name)),
                ImdbId = result.imdb_id,
                Plot = result.overview,
                Title = result.title,
                Year = DateTime.Parse(result.release_date).Year,

                AlternativeNames = ParseAlternatives(result.title, result.alternative_titles.titles),
                PosterPath = result.poster_path
            };
            return data;
        }

        public MovieEntry GetByImdbId(string id)
        {
            var col = new NameValueCollection
            {
                { "external_source", "imdb_id" }
            };
            var uri = _uriBuilder.BuildUri($"/find/{id}", col);
            var result = HttpHelper.GetRequestBody<FindRoot>(uri);

            return result.movie_results != null && result.movie_results.Count > 0
                ? GetByTmdbId(result.movie_results.First().id)
                : null;
        }

        public MovieEntry GetFromFile(string filename)
        {
            return new MovieEntry
            {
                Title = filename,
            };
        }

        public string GetPosterUri(string path, PosterSize size)
        {
            return PosterEndPoint + size + path;
        }

        public string[] GetPosters(int id)
        {
            var col = new NameValueCollection
            {
                { "include_image_language", "en,null" }
            };
            var uri = _uriBuilder.BuildUri($"/movie/{id}/images", col);
            var result = HttpHelper.GetRequestBody<PosterFindRoot>(uri);

            return result.posters.Select(x => x.file_path).ToArray();
        }

        private string[] ParseAlternatives(string title, List<Title> titles)
        {
            var combined = new List<string>();
            combined.Add(title);
            combined.AddRange(titles.Select(x => x.title));
            return combined.ToArray();
        }
    }
}
