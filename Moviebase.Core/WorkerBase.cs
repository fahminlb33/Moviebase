using System;
using System.ComponentModel;
using System.Threading;
using NLog;

namespace Moviebase.Core
{
    public abstract class WorkerBase : IWorker, IDisposable
    {
        protected readonly CancellationTokenSource CancellationToken;
        protected readonly object SyncLock = new object();
        protected int ProcessedWork, TotalWork;

        public event ProgressChangedEventHandler ProgressChanged;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public event EventHandler RunWorkerStarted;

        protected WorkerBase()
        {
            CancellationToken = new CancellationTokenSource();
            ProcessedWork = 0;
            TotalWork = 0;
        }

        public abstract void RunWorker();
        protected abstract void InternalRunWorker(object arg);

        public virtual void Cancel()
        {
            if (CancellationToken == null || CancellationToken.IsCancellationRequested) return;
            CancellationToken.Cancel();
        }

        #region Event Invocator
        protected virtual void OnRunWorkerStarted(object sender, EventArgs e)
        {
            RunWorkerStarted?.Invoke(sender, e);
        }

        protected virtual void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lock (SyncLock)
            {
                ProgressChanged?.Invoke(sender, e);
            }
        }

        protected virtual void OnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted?.Invoke(sender, e);
        } 
        #endregion

        protected virtual void IncrementWorkDone()
        {
            Interlocked.Increment(ref ProcessedWork);
        }

        protected virtual int GetPercentage()
        {
            return ToPercentage(ProcessedWork, TotalWork);
        }

        protected int ToPercentage(int processed, int total)
        {
            return Convert.ToInt32((double) processed / total * 100);
        }

        #region IDisposable Support
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                CancellationToken?.Dispose();
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
