using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moviebase.Entities.Web;
using Newtonsoft.Json;
using NLog;

namespace Moviebase.Core
{
    public class TmdbWebRequest : IDisposable, ITmdbWebRequest
    {
        public const string ApiEndpoint = "api.themoviedb.org";
        public const string PosterEndPoint = "https://image.tmdb.org/t/p/";
        public const string SearchMoviePath = "/search/movie";
        public const string MoviePath = "/movie/{0}";
        public const string FindPath = "/find/{0}";
        public const string PostersPath = "/movie/{0}/images";

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static readonly HttpClient HttpClientInstance = new HttpClient();
        private readonly string _apiKey;

        public TmdbWebRequest(string apiKey)
        {
            _apiKey = apiKey;
        }

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
                    if (response.StatusCode != (HttpStatusCode)429)
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
                    else if (response.IsSuccessStatusCode)
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                        break;
                    }

                    ++tryTimes;
                } while (tryTimes < Commons.TmdbWebRequestTries);

                return responseBody == null ? default(T) : JsonConvert.DeserializeObject<T>(responseBody);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error requesting API.");
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
                fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);
                await response.Content.CopyToAsync(fs).ContinueWith(t => fs.Close());
            }
            catch (Exception e)
            {
                fs?.Close();
                Log.Error(e, "Error requesting API.");
            }
        }

        public string BuildPosterUrl(string path, PosterSize size)
        {
            return PosterEndPoint + size + path;
        }

        public string BuildApiUri(string path, NameValueCollection col)
        {
            var queries = new NameValueCollection {{"api_key", _apiKey}, col};
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

        #region IDisposable Support
        private bool _disposedValue; 

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                // ReSharper disable once UseNullPropagation
                if(HttpClientInstance!=null) HttpClientInstance.Dispose();
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
