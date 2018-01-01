using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Moviebase.Core
{
    public class NetworkObserver : IDisposable, INetworkObserver
    {
        private Ping _ping;

        public NetworkObserver()
        {
            _ping = new Ping();
        }

        public async Task<bool> CheckInternetConnection()
        {
            var result = await _ping.SendPingAsync(Commons.PingServer);
            return result.Status == IPStatus.Success;
        }

        #region IDisposable Support

        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                if (_ping != null) _ping.Dispose();
            }

            _ping = null;

            _disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
