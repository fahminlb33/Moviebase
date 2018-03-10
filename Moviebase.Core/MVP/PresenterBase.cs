using System;
using System.Threading;

namespace Moviebase.Core.MVP
{
    public abstract class PresenterBase : IDisposable
    {
        protected CancellationTokenSource CancellationToken;

        protected bool IsCancellationRequested => CancellationToken != null && CancellationToken.IsCancellationRequested;

        protected void RecreateCancellationToken()
        {
            CancellationToken?.Dispose();
            CancellationToken = new CancellationTokenSource();
        }

        public abstract void UpdateUi(UiState state, int progressPercentage = -1);

        protected void CancelTask()
        {
            CancellationToken?.Cancel();
            UpdateUi(UiState.Cancelling);
        }

        protected void ThrowIfCancellationRequested()
        {
            CancellationToken?.Token.ThrowIfCancellationRequested();
        }

        #region IDisposable Support
        private bool _disposedValue; 

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;
            if (disposing)
            {
                if (CancellationToken != null) CancellationToken.Dispose();
            }

            CancellationToken = null;
            _disposedValue = true;
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
