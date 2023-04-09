using System;
using UniRx;

namespace Unity1week202303.InGame.State
{
    public class InGameStateUseCase
    {
        private ReactiveProperty<InGameStateType> _currentGameState = new(InGameStateType.None);

        public bool IsState(InGameStateType targetState)
        {
            return _currentGameState.Value == targetState;
        }

        public void SetState(InGameStateType inGameStateType)
        {
            _currentGameState.Value = inGameStateType;
        }

        public IObservable<InGameStateType> OnChangeState() => _currentGameState;
    }
}
