using System;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202303.InGame.Situations
{
    public class GameTimerPresenter : IInitializable, IDisposable
    {
        private readonly RemainingTimer _remainingTimer;
        private readonly GameTimerView _gameTimerView;
        private readonly CompositeDisposable _disposable = new();

        public GameTimerPresenter(
            RemainingTimer remainingTimer,
            GameTimerView gameTimerView)
        {
            _remainingTimer = remainingTimer;
            _gameTimerView = gameTimerView;
        }

        public void Initialize()
        {
            _remainingTimer.RemainingTime
                .Select(f => Mathf.FloorToInt(f))
                .DistinctUntilChanged()
                .Subscribe(remainingTime => _gameTimerView.Set(TimeSpan.FromSeconds(remainingTime)))
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
