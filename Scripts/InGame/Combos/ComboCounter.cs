using UniRx;

namespace Unity1week202303.InGame.Combos
{
    /// <summary>
    /// コンボのカウント
    /// </summary>
    public class ComboCounter
    {
        public IReadOnlyReactiveProperty<int> ComboCount => _comboCount;
        public IReadOnlyReactiveProperty<int> MaxComboCount => _maxComboCount;
        
        private readonly ReactiveProperty<int> _maxComboCount = new(0);
        private readonly ReactiveProperty<int> _comboCount = new();

        public void AddCount(int value)
        {
            _comboCount.Value += value;
            if (_comboCount.Value > _maxComboCount.Value)
            {
                // 最大コンボ数を更新
                _maxComboCount.Value = _comboCount.Value;
            }
        }

        public void ResetCount()
        {
            _comboCount.Value = 0;
        }

        public void Clear()
        {
            _comboCount.Value = 0;
            _maxComboCount.Value = 0;
        }
    }
}
