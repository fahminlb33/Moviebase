using System.Collections.Specialized;
using System.Threading.Tasks;
using Moviebase.Entities.Web;

namespace Moviebase.Core
{
    public interface ITmdbWebRequest
    {
        string BuildApiUri(string path, NameValueCollection col);
        string BuildPosterUrl(string path, PosterSize size);
        void Dispose();
        Task DownloadFile(string url, string outputPath);
        Task<T> GetRequestBody<T>(string uri);
        Task<T> GetRequestBody<T>(string path, NameValueCollection col);
    }
}