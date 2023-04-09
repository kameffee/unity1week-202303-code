using Unity1week202303.Audio;
using Unity1week202303.Scenes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Unity1week202303.Installer
{
    public class RootLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private AudioResource _audioResource;

        [SerializeField]
        private SceneTransitionView _transitionViewPrefab;

        protected override void Configure(IContainerBuilder builder)
        {
            AudioInstall(builder);
        }

        private void AudioInstall(IContainerBuilder builder)
        {
            // Audio
            builder.Register<AudioResource>(resolver =>
            {
                var resource = Instantiate(_audioResource);
                DontDestroyOnLoad(resource.gameObject);
                return resource;
            }, Lifetime.Singleton);

            builder.Register<AudioResourceLoader>(Lifetime.Singleton);
            builder.Register<AudioPlayer>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<AudioSettingsService>(Lifetime.Singleton);

            builder.RegisterComponentOnNewGameObject<BgmPlayer>(Lifetime.Singleton).DontDestroyOnLoad();
            builder.RegisterComponentOnNewGameObject<SePlayer>(Lifetime.Singleton).DontDestroyOnLoad();

            // SceneLoad
            builder.Register<SceneManagerEvents>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.RegisterComponentInNewPrefab<SceneTransitionView>(_transitionViewPrefab, Lifetime.Singleton).DontDestroyOnLoad();
        }
    }
}
