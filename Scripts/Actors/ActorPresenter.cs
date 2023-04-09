using UniRx;
using Unity1week202303.InGame.TypingProgress;
using Unity1week202303.TypingProgress;
using UnityEngine;
using VContainer.Unity;

namespace Unity1week202303.Actors
{
    public class ActorPresenter : IInitializable
    {
        private readonly ActorView _actorView;
        private readonly TypingProgressService _progressService;
        private readonly GetTypingResultUseCase _getTypingResultUseCase;
        private readonly CompositeDisposable _disposable = new();

        public ActorPresenter(
            ActorView actorView,
            TypingProgressService progressService,
            GetTypingResultUseCase getTypingResultUseCase)
        {
            _actorView = actorView;
            _progressService = progressService;
            _getTypingResultUseCase = getTypingResultUseCase;
        }

        public void Initialize()
        {
            _getTypingResultUseCase.OnSuccessTyping()
                .Subscribe(_ => _actorView.SetReaction(true))
                .AddTo(_disposable);
            
            _getTypingResultUseCase.OnFailedTyping()
                .Subscribe(_ => _actorView.SetReaction(false))
                .AddTo(_disposable);
        }
    }
}
