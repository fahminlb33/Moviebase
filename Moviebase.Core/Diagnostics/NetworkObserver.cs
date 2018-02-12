using System;
using System.Net.NetworkInformation;

namespace Moviebase.Core.Diagnostics
{
    public sealed class NetworkObserver
    {
        private static Lazy<NetworkObserver> _lazy = new Lazy<NetworkObserver>(() => new NetworkObserver());
        private static Ping _ping;

        public static NetworkObserver Instance => _lazy.Value;

        private NetworkObserver()
        {
            _ping = new Ping();
        }
        
        public bool IsInternetConnected()
        {
            try
            {
                var result = _ping.Send(Commons.PingServer);
                return result?.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
    }
}
