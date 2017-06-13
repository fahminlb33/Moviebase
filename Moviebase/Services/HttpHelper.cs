using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Newtonsoft.Json;

namespace Moviebase.Services
{
    public static class HttpHelper
    {
        public static T GetRequestBody<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var response = client.GetStringAsync(uri).Result;
                    return JsonConvert.DeserializeObject<T>(response);
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                    return default(T);
                }
            }
        }

        public static void DownloadFile(string uri, string savePath)
        {
            using (var client = new HttpClient())
            using (var fs = new FileStream(savePath, FileMode.Create))
            {
                var response = client.GetAsync(uri).Result;
                response.Content.CopyToAsync(fs).Wait();
            }
        }
    }
}
