using System;
using UniRx;
using Unity1week202303.Audio;
using Unity1week202303.InGame.State;
using Unity1week202303.InGame.TypingProgress;
using VContainer.Unity;

namespace Unity1week202303.TypingProgress
{
    public class TypingProgressPresenter : IInitializable, IDisposable
    {
        private readonly TypingProgressView _view;
        private readonly TypingProgressService _typingProgressService;
        private readonly InGameStateUseCase _inGameStateUseCase;
        private readonly GetTypingResultUseCase _getTypingResultUseCase;
        private readonly AudioPlayer _audioPlayer;

        private readonly CompositeDisposable _disposable = new();

        public TypingProgressPresenter(
            TypingProgressView view,
            TypingProgressService typingProgressService,
            InGameStateUseCase inGameStateUseCase,
            GetTypingResultUseCase getTypingResultUseCase,
            AudioPlayer audioPlayer
        )
        {
            _view = view;
            _typingProgressService = typingProgressService;
            _inGameStateUseCase = inGameStateUseCase;
            _getTypingResultUseCase = getTypingResultUseCase;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            _view.Set(string.Empty, string.Empty);

            _typingProgressService.OnUpdateTaskAsObservable()
                .Subscribe(progressData => _view.Set(progressData.Entered, progressData.NotEntered))
                .AddTo(_disposable);

            _typingProgressService.OnUpdateProgressAsObservable()
                .Where(_ => _inGameStateUseCase.IsState(InGameStateType.Playing))
                .Subscribe(progressData => _view.Set(progressData.Entered, progressData.NotEntered))
                .AddTo(_disposable);

            _inGameStateUseCase.OnChangeState()
                .Where(stateType => stateType == InGameStateType.Result)
                .Subscribe(_ => _view.Set(string.Empty, string.Empty))
                .AddTo(_disposable);

            _getTypingResultUseCase.OnFailedTyping()
                .Subscribe(_ => _audioPlayer.PlaySe(3))
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
