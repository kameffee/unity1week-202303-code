using System;
using UniRx;

namespace Unity1week202303.InGame.Retry
{
    public class RetryUseCase
    {
        private readonly ISubject<Unit> _onRetry = new Subject<Unit>();

        public IObservable<Unit> OnRetry() => _onRetry;

        public void Retry() => _onRetry.OnNext(Unit.Default);
    }
}
