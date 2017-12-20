using System;
using System.Collections.Generic;
using System.ComponentModel;
using NLog;

namespace Moviebase.Core
{
    public class WorkerPool : IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly Queue<IWorker> _waitingWorkers;
        private WorkerBase _worker;
        private bool _isWorkerRunning;

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        public event EventHandler RunWorkerCompleted;
        public event EventHandler RunWorkerStarted;

        public WorkerPool()
        {
            _waitingWorkers = new Queue<IWorker>();
        }

        public void RunWorker(IWorker worker)
        {
            if (worker == null) throw new ArgumentNullException(nameof(worker));
            if (_isWorkerRunning)
            {
                _waitingWorkers.Enqueue(worker);
                _log.Debug("Work queued.");
            }
            else
            {
                if (_worker != null)
                {
                    _worker.ProgressChanged -= worker_ProgressChanged;
                    _worker.RunWorkerCompleted -= worker_RunWorkerCompleted;
                    _worker.RunWorkerStarted -= worker_RunWorkerStarted;
                    _worker.Dispose();
                }

                _worker = (WorkerBase)worker;
                _worker.ProgressChanged += worker_ProgressChanged;
                _worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                _worker.RunWorkerStarted += worker_RunWorkerStarted;

                _log.Debug("Running work...");
                _worker.RunWorker();
                _isWorkerRunning = true;
            }
        }

        public void Stop()
        {
            _worker?.Cancel();
            _waitingWorkers.Clear();
        }

        #region Event Invocator
        private void worker_RunWorkerCompleted(object sender, EventArgs e)
        {
            _isWorkerRunning = false;
            if (_waitingWorkers.Count > 0)
            {
                RunWorker(_waitingWorkers.Dequeue());
            }
            else
            {
                OnRunWorkerCompleted();
            }
        }

        private void worker_RunWorkerStarted(object sender, EventArgs e)
        {
            OnRunWorkerStarted();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnProgressChanged(e);
        }

        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(this, e);
        }

        protected virtual void OnRunWorkerCompleted()
        {
            RunWorkerCompleted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnRunWorkerStarted()
        {
            RunWorkerStarted?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region IDisposable Support
        private bool _disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                if (_worker != null) _worker.Dispose();
                if (_waitingWorkers != null)
                {
                    foreach (var waitingWorker in _waitingWorkers)
                    {
                        ((IDisposable) waitingWorker)?.Dispose();
                    }

                    _waitingWorkers.Clear();
                }
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
