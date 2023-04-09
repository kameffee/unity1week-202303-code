using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity1week202303.Audio;
using Unity1week202303.Scenes;
using VContainer.Unity;

namespace Unity1week202303.Title
{
    public class TitleMenuPresenter : IInitializable, IDisposable
    {
        private readonly TitleMenuView _menuView;
        private readonly SceneLoader _sceneLoader;
        private readonly AudioPlayer _audioPlayer;
        private readonly CompositeDisposable _disposable = new();

        public TitleMenuPresenter(
            TitleMenuView menuView,
            SceneLoader sceneLoader,
            AudioPlayer audioPlayer)
        {
            _menuView = menuView;
            _sceneLoader = sceneLoader;
            _audioPlayer = audioPlayer;
        }

        public void Initialize()
        {
            _menuView.OnClickStartAsObservable()
                .Subscribe(_ =>
                {
                    _audioPlayer.PlaySe(2);
                    _sceneLoader.LoadAsync(SceneDefine.InGame).Forget();
                })
                .AddTo(_disposable);
        }

        public void Dispose() => _disposable.Dispose();
    }
}
