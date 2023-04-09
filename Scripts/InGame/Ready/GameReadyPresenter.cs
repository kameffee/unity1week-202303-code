using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Unity1week202303.InGame.Ready
{
    public class GameReadyPresenter : IDisposable
    {
        private readonly Func<GameReadyView> _viewFactory;
        private readonly CompositeDisposable _disposable;

        private GameReadyView _view;

        public GameReadyPresenter(Func<GameReadyView> viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public async UniTask Show()
        {
            _view = _viewFactory.Invoke();
            await _view.Show();
        }

        public async UniTask Hide()
        {
            await _view.Hide();
            GameObject.Destroy(_view);
        }

        public void Dispose() => _disposable?.Dispose();
    }
}
