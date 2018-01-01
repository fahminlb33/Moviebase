using System.Threading.Tasks;

namespace Moviebase.Core
{
    public interface INetworkObserver
    {
        Task<bool> CheckInternetConnection();
    }
}