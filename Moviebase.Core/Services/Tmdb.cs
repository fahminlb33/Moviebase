using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Moviebase.Entities;
using Newtonsoft.Json;

namespace Moviebase.Core.Services
{
    public class Tmdb : ITmdb
    {
        public const string ApiEndpoint = "api.themoviedb.org";
        public const string PosterEndPoint = "https://image.tmdb.org/t/p/";
        public const string SearchMoviePath = "/search/movie";
        public const string MoviePath = "/movie/{0}";
        public const string FindPath = "/find/{0}";
        public const string PostersPath = "/movie/{0}/images";
        
        private static readonly HttpClient HttpClientInstance = new HttpClient();
        private readonly string _apiKey;

        public Tmdb(string apiKey)
        {
            _apiKey = apiKey;
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

            var response = await GetRequestBody<MovieSearchRoot>(SearchMoviePath, param);
            return response.total_results <= 0 ? null : response.results.Select(x => x.id.ToString()).ToList();
        }

        public async Task<TmdbResult> GetByFilename(string filename)
        {
            await Task.Yield();
            return new TmdbResult
            {
                Title = filename,
                Id = Commons.NotFetchedEntryId
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
            var response = await GetRequestBody<MovieDetailsRoot>(string.Format(MoviePath, id), col);
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

            var response = await GetRequestBody<FindRoot>(string.Format(FindPath, id), col);
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

            var response = await GetRequestBody<PosterFindRoot>(string.Format(PostersPath, id), col);
            return response.posters.Select(x => x.file_path).ToList();
        }

        public string GetPosterUrl(string path, PosterSize size)
        {
            return BuildPosterUrl(path, size);
        }

        #endregion

        #region ITmdbWebRequest Implementation
        public async Task<T> GetRequestBody<T>(string path, NameValueCollection col)
        {
            return await GetRequestBody<T>(BuildApiUri(path, col));
        }

        public async Task<T> GetRequestBody<T>(string uri)
        {
            try
            {
                string responseBody = null;
                var tryTimes = 0;
                do
                {
                    var response = await HttpClientInstance.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseByte = await response.Content.ReadAsByteArrayAsync();
                        responseBody = Encoding.UTF8.GetString(responseByte);
                        break;
                    }
                    if (response.StatusCode == (HttpStatusCode)429)
                    {
                        var retryAfter = response.Headers.RetryAfter?.Delta;
                        if (retryAfter.HasValue && retryAfter.Value.TotalSeconds > 0)
                        {
                            await Task.Delay(retryAfter.Value);
                        }
                        else
                        {
                            await Task.Delay(1000);
                        }
                    }

                    ++tryTimes;
                } while (tryTimes < Commons.TmdbWebRequestTries);

                return responseBody == null ? default(T) : JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception e)
            {
                Debug.Print("Error requesting resource: " + e.Message);
                return default(T);
            }
        }

        public async Task DownloadFile(string url, string outputPath)
        {
            FileStream fs = null;
            try
            {
                var response = await HttpClientInstance.GetAsync(url);
                response.EnsureSuccessStatusCode();

                if (File.Exists(outputPath)) File.Delete(outputPath);
                fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);
                await response.Content.CopyToAsync(fs).ContinueWith(t => fs.Close());
            }
            catch (Exception e)
            {
                Debug.Print("Error requesting resource: " + e.Message);
            }
            finally
            {
                fs?.Close();
            }
        }

        public string BuildPosterUrl(string path, PosterSize size)
        {
            return PosterEndPoint + size + path;
        }

        public string BuildApiUri(string path, NameValueCollection col)
        {
            var queries = new NameValueCollection { { "api_key", _apiKey }, col };
            var uri = new UriBuilder
            {
                Host = ApiEndpoint,
                Scheme = Uri.UriSchemeHttps,
                Path = "3" + path,
                Query = BuildQueryString(queries)
            };

            return uri.ToString();
        }

        public string BuildQueryString(NameValueCollection col)
        {
            if (col == null) return String.Empty;
            var sb = new StringBuilder();

            for (int i = 0; i < col.Count; i++)
            {
                var key = col.GetKey(i);
                if (i > 0) sb.Append("&");
                sb.AppendFormat("{0}={1}", key, col.Get(key));
            }
            return sb.ToString();
        }
        #endregion

        private string[] ParseAlternatives(string title, IEnumerable<Title> titles)
        {
            var combined = new List<string> {title};
            combined.AddRange(titles.Select(x => x.title));
            return combined.ToArray();
        }
    }
}
