using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moviebase.Core.MVP
{
    public abstract class PresenterBase
    {
        private CancellationTokenSource _cancellationToken;

        protected bool IsCancellationRequested => _cancellationToken != null && _cancellationToken.IsCancellationRequested;

        protected void RecreateCancellationToken()
        {
            _cancellationToken?.Dispose();
            _cancellationToken = new CancellationTokenSource();
        }

        protected abstract void UpdateUi(UiState state, int progressPercentage = -1);

        protected void CancelTask()
        {
            _cancellationToken?.Cancel();
            UpdateUi(UiState.Cancelling);
        }

        protected void ThrowIfCancellationRequested()
        {
            _cancellationToken?.Token.ThrowIfCancellationRequested();
        }

        protected void RunTask(Action action)
        {
            RecreateCancellationToken();
            Task.Run(() =>
            {
                UpdateUi(UiState.Working);
                action.Invoke();
                UpdateUi(UiState.Ready);
            }, _cancellationToken.Token);
        }
    }
}
