using UniRx;

namespace Unity1week202303.InGame.Inputs
{
    /// <summary>
    /// 強制的に見えないInputFieldにフォーカスをさせる
    /// </summary>
    public class ForceInputFieldModeUseCase
    {
        public IReadOnlyReactiveProperty<bool> IsEnable => _isEnable;

        private readonly ReactiveProperty<bool> _isEnable = new(false);

        public void SetEnable(bool isEnable) => _isEnable.Value = isEnable;
    }
}
