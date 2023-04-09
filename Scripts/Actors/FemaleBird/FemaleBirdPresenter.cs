using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity1week202303.InGame.TypingProgress;
using VContainer.Unity;

namespace Unity1week202303.Actors.FemaleBird
{
    public class FemaleBirdPresenter : IInitializable, IDisposable
    {
        private readonly FemaleBirdView _view;
        private readonly GetTypingResultUseCase _getTypingResultUseCase;
        private readonly CompositeDisposable _disposable = new();
        

        public FemaleBirdPresenter(
            FemaleBirdView view,
            GetTypingResultUseCase getTypingResultUseCase)
        {
            _view = view;
            _getTypingResultUseCase = getTypingResultUseCase;
        }

        public void Initialize()
        {
            _getTypingResultUseCase.OnSuccessTyping()
                .Subscribe(successCount => _view.EmitHeart(successCount))
                .AddTo(_disposable);

            _getTypingResultUseCase.OnFailedTyping()
                .Subscribe(failedCount => _view.EmitBreakHeart(failedCount))
                .AddTo(_disposable);
        }

        public async UniTask Show()
        {
            await _view.Show();
        }

        public async UniTask Hide()
        {
            await _view.Hide();
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
