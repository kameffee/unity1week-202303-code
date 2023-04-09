using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Unity1week202303.InGame.PreCount
{
    public class PreCountPresenter : IInitializable
    {
        private readonly Func<PreCountView> _viewFactory;

        private PreCountView _view;

        public PreCountPresenter(Func<PreCountView> viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void Initialize()
        {
            _view = _viewFactory.Invoke();
        }

        public async UniTask Play(CancellationToken cancellationToken)
        {
            await _view.Play(cancellationToken);
        }
    }
}
