using System;
using UniRx;
using VContainer.Unity;

namespace Unity1week202303.InGame.Situations
{
    public class RemainingTimePresenter : IInitializable, IDisposable
    {
        private readonly TimeCounter _timeCounter;
        private readonly RemainingTimer _remainingTimer;

        private readonly CompositeDisposable _disposable = new();

        public RemainingTimePresenter(TimeCounter timeCounter, RemainingTimer remainingTimer)
        {
            _timeCounter = timeCounter;
            _remainingTimer = remainingTimer;
        }

        public void Initialize()
        {
            _timeCounter.ElapsedTime
                .Where(_ => !_remainingTimer.IsFinish())
                .Subscribe(elapsedTime => _remainingTimer.SetElapsedTime(elapsedTime))
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
