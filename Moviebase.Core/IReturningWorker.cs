using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moviebase.Core
{
    public interface IReturningWorker<T>
    {
        IEnumerable<Task<T>> CreateTasks();
    }
}
