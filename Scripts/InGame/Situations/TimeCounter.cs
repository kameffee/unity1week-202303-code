using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202303.InGame.Situations
{
    /// <summary>
    /// 経過時間のカウント
    /// </summary>
    public class TimeCounter : ITickable
    {
        public IReadOnlyReactiveProperty<float> ElapsedTime => _elapsedTime;

        private readonly ReactiveProperty<float> _elapsedTime = new();
        private float _startTime;
        private bool _isRun;

        public void Start()
        {
            _startTime = Time.time;
            _elapsedTime.Value = 0f;
            _isRun = true;
        }

        public void Stop()
        {
            _isRun = false;
        }

        void ITickable.Tick()
        {
            if (!_isRun) return;

            _elapsedTime.Value = Time.time - _startTime;
        }

        public void Reset() => _elapsedTime.Value = 0;
    }
}
