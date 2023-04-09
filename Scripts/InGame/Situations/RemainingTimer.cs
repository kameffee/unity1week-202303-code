using System;
using UniRx;
using UnityEngine;

namespace Unity1week202303.InGame.Situations
{
    /// <summary>
    /// 残り時間タイマー
    /// </summary>
    public class RemainingTimer
    {
        public IReadOnlyReactiveProperty<float> RemainingTime => _remainingTime;
        public IObservable<Unit> OnFinish => _onFinish;

        private readonly ReactiveProperty<float> _remainingTime = new(0f);
        private readonly ISubject<Unit> _onFinish = new Subject<Unit>();

        public TimeSpan TimeLimit => _timeLimit;

        private TimeSpan _timeLimit;
        private float _elpsedTime = 0;

        public RemainingTimer()
        {
        }

        /// <summary>
        /// ゲームの制限時間を設定する
        /// </summary>
        public void SetTime(TimeSpan timeSpan)
        {
            _timeLimit = timeSpan;
            _remainingTime.Value = (float)_timeLimit.TotalSeconds - _elpsedTime;
        }

        public void SetElapsedTime(float elapsedTime)
        {
            _remainingTime.Value = Mathf.Max(0f, (float)_timeLimit.TotalSeconds - elapsedTime);
            if (Mathf.Approximately(_remainingTime.Value, 0f))
            {
                _onFinish.OnNext(Unit.Default);
            }
        }

        public bool IsFinish()
        {
            return _remainingTime.Value == 0f;
        }
    }
}
