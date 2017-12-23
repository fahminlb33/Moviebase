using System.Threading.Tasks;

namespace Moviebase.Core
{
    public interface IComponentManager
    {
        Task<bool> CheckGuessItInstallation();
        Task<bool> CheckPythonInstallation();
    }
}