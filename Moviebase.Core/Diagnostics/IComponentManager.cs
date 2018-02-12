using System.Threading.Tasks;

namespace Moviebase.Core.Diagnostics
{
    public interface IComponentManager
    {
        Task<bool> IsGuessItInstalled();
        Task<bool> IsPythonInstalled();
    }
}