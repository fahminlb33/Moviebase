using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Moviebase.Entities.Web;
using Newtonsoft.Json;
using NLog;

namespace Moviebase.Core
{
    internal class TmdbWebRequest : IDisposable
    {
        public const string ApiEndpoint = "api.themoviedb.org";
        public const string PosterEndPoint = "https://image.tmdb.org/t/p/";
        public const string SearchMoviePath = "/search/movie";
        public const string MoviePath = "/movie/{0}";
        public const string FindPath = "/find/{0}";
        public const string PostersPath = "/movie/{0}/images";

        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly HttpClient _wc;
        private readonly string _apiKey;

        public TmdbWebRequest(string apiKey)
        {
            _apiKey = apiKey;
            _wc = new HttpClient();
        }

        public async Task<T> GetRequestBody<T>(string path, NameValueCollection col)
        {
            return await GetRequestBody<T>(BuildApiUri(path, col));
        }

        public async Task<T> GetRequestBody<T>(string uri)
        {
            try
            {
                var response = await _wc.GetAsync(uri);
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                _log.Error(e, "Error requesting API.");
                return default(T);
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

        private string BuildQueryString(NameValueCollection col)
        {
            if (col == null) return String.Empty;
            var sb = new StringBuilder();

            for (int i = 0; i < col.Count; i++)
            {
                var key = col.GetKey(i);
                sb.AppendFormat("&{0}={1}", key, col.Get(key));
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
                if(_wc!=null) _wc.Dispose();
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
