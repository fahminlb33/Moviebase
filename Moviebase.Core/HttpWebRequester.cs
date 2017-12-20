using System;
using System.Net;
using Newtonsoft.Json;
using NLog;

namespace Moviebase.Core
{
    internal class HttpWebRequester : IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly WebClient _wc;

        public HttpWebRequester()
        {
            _wc = new WebClient();
        }

        public T GetRequestBody<T>(string uri)
        {
            try
            {
                var response = _wc.DownloadString(uri);
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error requesting API.");
                return default(T);
            }
        }

        #region IDisposable Support
        private bool _disposedValue; 

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                _wc.Dispose();
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
