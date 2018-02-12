using System.Collections.Specialized;
using System.Threading.Tasks;
using Moviebase.Entities;

namespace Moviebase.Core.Services
{
    public interface ITmdbWebRequest
    {
        string BuildApiUri(string path, NameValueCollection col);
        string BuildPosterUrl(string path, PosterSize size);
        Task DownloadFile(string url, string outputPath);
        Task<T> GetRequestBody<T>(string uri);
        Task<T> GetRequestBody<T>(string path, NameValueCollection col);
    }
}