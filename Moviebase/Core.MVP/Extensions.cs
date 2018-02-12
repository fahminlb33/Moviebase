using Moviebase.Core.Diagnostics;

namespace Moviebase.Core.MVP
{
    public static class Extensions
    {
        public static ValidationSupport EnsureInternetConnected(this ValidationSupport support)
        {
            support.IsTrue(() => NetworkObserver.Instance.IsInternetConnected(), "Internet is not connected.");
            return support;
        }


    }
}
