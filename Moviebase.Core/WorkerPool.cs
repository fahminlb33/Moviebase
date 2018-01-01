using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace Moviebase.Core
{
    public class WorkerPool : IWorkerPool, IDisposable
    {
        private static Logger _log = LogManager.GetCurrentClassLogger();
        private CancellationTokenSource _cancellationToken;
        private volatile bool _isWorking;
        
        public Action RunWorkerStarted { get; set; }
        public Action RunWorkerCompleted { get; set; }
        public Action<int, object> ProgressChanged { get; set; }
        
        public void Start(INonReturningWorker worker)
        {
            if (_isWorking)
                throw new InvalidOperationException("Could not start another task. Existing task is currently running.");

            RecreateCancellationToken();
            Task.Run(() => RunWorkerStartedCallback())
                .ContinueWith(x => RunTasks(worker.CreateTasks()))
                .ContinueWith(x => RunWorkerCompletedCallback());
        }

        public void Start<T>(IReturningWorker<T> worker)
        {
            if (_isWorking)
                throw new InvalidOperationException("Could not start another task. Existing task is currently running.");

            RecreateCancellationToken();
            Task.Run(() => RunWorkerStartedCallback())
                .ContinueWith(x => RunTasks(worker.CreateTasks()))
                .ContinueWith(x => RunWorkerCompletedCallback());
        }

        public void Stop()
        {
            if (!_isWorking) return;
            _cancellationToken?.Cancel();
        }

        private void RunWorkerStartedCallback()
        {
            _isWorking = true;
            RunWorkerStarted?.Invoke();
        }

        private void RunWorkerCompletedCallback()
        {
            _isWorking = false;
            RunWorkerCompleted?.Invoke();
        }

        private void RecreateCancellationToken()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        private async void RunTasks<T>(IEnumerable<Task<T>> tasks)
        {
            foreach (var bucket in Interleaved(tasks))
            {
                var t = await bucket;
                try
                {
                    ProgressChanged?.Invoke(0, await t);
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception exc)
                {
                    _log.Error(exc);
                }
            }
        }

        private async void RunTasks(IEnumerable<Task> tasks)
        {
            foreach (var bucket in Interleaved(tasks))
            {
                var t = await bucket;
                try
                {
                    await t;
                }
                catch (OperationCanceledException)
                {
                }
                catch (Exception exc)
                {
                    _log.Error(exc);
                }
            }
        }

        private Task<Task<T>>[] Interleaved<T>(IEnumerable<Task<T>> tasks)
        {
            var inputTasks = tasks.ToList();

            var buckets = new TaskCompletionSource<Task<T>>[inputTasks.Count];
            var results = new Task<Task<T>>[buckets.Length];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new TaskCompletionSource<Task<T>>();
                results[i] = buckets[i].Task;
            }

            int nextTaskIndex = -1;
            Action<Task<T>> continuation = completed =>
            {
                var bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
                bucket.TrySetResult(completed);
            };

            foreach (var inputTask in inputTasks)
                inputTask.ContinueWith(continuation, _cancellationToken.Token, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

            return results;
        }

        private Task<Task>[] Interleaved(IEnumerable<Task> tasks)
        {
            var inputTasks = tasks.ToList();

            var buckets = new TaskCompletionSource<Task>[inputTasks.Count];
            var results = new Task<Task>[buckets.Length];
            for (int i = 0; i < buckets.Length; i++)
            {
                buckets[i] = new TaskCompletionSource<Task>();
                results[i] = buckets[i].Task;
            }

            int nextTaskIndex = -1;
            Action<Task> continuation = completed =>
            {
                var bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
                bucket.TrySetResult(completed);
            };

            foreach (var inputTask in inputTasks)
                inputTask.ContinueWith(continuation, _cancellationToken.Token, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);

            return results;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_cancellationToken != null) _cancellationToken.Dispose();
                }

                _cancellationToken = null;
                RunWorkerCompleted = null;
                RunWorkerStarted = null;
                ProgressChanged = null;

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~WorkerPool() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
