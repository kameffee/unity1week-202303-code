using System;
using UniRx;
using Unity1week202303.InGame.State;
using Unity1week202303.TypingProgress;
using VContainer.Unity;

namespace Unity1week202303.InGame.Inputs
{
    public class PlayerInputPresenter : IInitializable, IDisposable
    {
        private readonly PlayerKeyboardInput _playerKeyboardInput;
        private readonly TypingProgressService _typingProgressService;
        private readonly InputFieldView _inputFieldView;
        private readonly InGameStateUseCase _inGameStateUseCase;
        private readonly ForceInputFieldModeUseCase _forceInputFieldModeUseCase;

        private readonly CompositeDisposable _disposable = new();

        public PlayerInputPresenter(
            PlayerKeyboardInput playerKeyboardInput,
            TypingProgressService typingProgressService,
            InputFieldView inputFieldView,
            InGameStateUseCase inGameStateUseCase,
            ForceInputFieldModeUseCase forceInputFieldModeUseCase)
        {
            _playerKeyboardInput = playerKeyboardInput;
            _typingProgressService = typingProgressService;
            _inputFieldView = inputFieldView;
            _inGameStateUseCase = inGameStateUseCase;
            _forceInputFieldModeUseCase = forceInputFieldModeUseCase;
        }

        public void Initialize()
        {
            _forceInputFieldModeUseCase.IsEnable
                .Subscribe(isEnable => _inputFieldView.SetEnable(isEnable))
                .AddTo(_disposable);

            _playerKeyboardInput.OnInputStringAsObservable()
                .Where(_ => _inGameStateUseCase.IsState(InGameStateType.Playing))
                .Subscribe(str => _typingProgressService.Input(str))
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
