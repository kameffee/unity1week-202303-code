using System;
using UniRx;
using Unity1week202303.InGame.TypingProgress;
using Unity1week202303.TypingProgress;
using VContainer.Unity;

namespace Unity1week202303.InGame.Combos
{
    public class ComboCountPresenter : IInitializable, IDisposable
    {
        private readonly ComboCountView _view;
        private readonly ComboCounter _comboCounter;
        private readonly GetTypingResultUseCase _getTypingResultUse;
        private readonly CompositeDisposable _disposable = new();

        public ComboCountPresenter(
            ComboCountView view,
            ComboCounter comboCounter,
            GetTypingResultUseCase getTypingResultUse)
        {
            _view = view;
            _comboCounter = comboCounter;
            _getTypingResultUse = getTypingResultUse;
        }

        public void Initialize()
        {
            _comboCounter.ComboCount
                .Subscribe(count => _view.Set(count))
                .AddTo(_disposable);

            _getTypingResultUse.OnSuccessTyping()
                .Subscribe(count => _comboCounter.AddCount(count))
                .AddTo(_disposable);

            // 失敗時にコンボをリセット
            _getTypingResultUse.OnFailedTyping()
                .Subscribe(_ => _comboCounter.ResetCount())
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
