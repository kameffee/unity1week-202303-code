using UniRx;

namespace Unity1week202303.InGame.Situations
{
    /// <summary>
    /// ゲームの状況
    /// </summary>
    public class Situation
    {
        public IReadOnlyReactiveProperty<int> SendCount => _sendCount;
        public IReadOnlyReactiveProperty<int> MissCount => _missCount;

        private readonly ReactiveProperty<int> _sendCount = new();
        private readonly ReactiveProperty<int> _missCount = new();

        public Situation()
        {
        }

        public void AddSendCount(int count) => _sendCount.Value += count;

        public void AddMissCount(int value) => _missCount.Value += value;

        public void Reset()
        {
            _sendCount.Value = 0;
            _missCount.Value = 0;
        }
    }
}
