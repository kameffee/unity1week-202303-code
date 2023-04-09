using System;
using UniRx;
using Unity1week202303.InGame.State;
using Unity1week202303.TypingProgress;
using VContainer.Unity;

namespace Unity1week202303.Sentences
{
    public class SentenceDisplayPresenter : IInitializable, IDisposable
    {
        private readonly SentenceDisplayView _view;
        private readonly TypingProgressService _typingProgressService;
        private readonly SentenceRepository _sentenceRepository;
        private readonly InGameStateUseCase _inGameStateUseCase;

        private readonly CompositeDisposable _disposable = new();

        public SentenceDisplayPresenter(
            SentenceDisplayView view,
            TypingProgressService typingProgressService,
            SentenceRepository sentenceRepository,
            InGameStateUseCase inGameStateUseCase)
        {
            _view = view;
            _typingProgressService = typingProgressService;
            _sentenceRepository = sentenceRepository;
            _inGameStateUseCase = inGameStateUseCase;
        }

        public void Initialize()
        {
            _view.SetSententce(string.Empty);
            _view.SetKanaText(string.Empty, string.Empty);

            _typingProgressService.OnUpdateTaskAsObservable()
                .Select(data => _sentenceRepository.Get(data.SentenceId))
                .Subscribe(sentenceData =>
                {
                    _view.SetSententce(sentenceData.DisplayText);
                    _view.SetKanaText(string.Empty, sentenceData.KanaText);
                })
                .AddTo(_disposable);

            _typingProgressService.OnUpdateProgressAsObservable()
                .Subscribe(progressData =>
                {
                    _view.SetKanaText(progressData.EnteredKanaText, progressData.NotEnteredKanaText);
                })
                .AddTo(_disposable);

            _inGameStateUseCase.OnChangeState()
                .Where(stateType => stateType == InGameStateType.Result)
                .Subscribe(_ =>
                {
                    _view.SetSententce(string.Empty);
                    _view.SetKanaText(string.Empty, string.Empty);
                })
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
