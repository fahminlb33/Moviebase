using System;
using System.ComponentModel;

namespace Moviebase.Core
{
    public interface IWorker
    {
        event ProgressChangedEventHandler ProgressChanged;
        event RunWorkerCompletedEventHandler RunWorkerCompleted;
        event EventHandler RunWorkerStarted;

        void Cancel();
        void RunWorker();
    }
}
