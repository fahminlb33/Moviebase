using System;

namespace Moviebase.Core
{
    public interface IWorkerPool
    {
        Action RunWorkerStarted { get; set; }
        Action RunWorkerCompleted { get; set; }
        Action<int, object> ProgressChanged { get; set; }

        void Start(IWorker worker);
        void Start<T>(IReturningWorker<T> worker);
        void Stop();
    }
}