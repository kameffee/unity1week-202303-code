using Unity1week202303.Audio;
using Unity1week202303.Config;
using Unity1week202303.Title;
using VContainer;
using VContainer.Unity;

namespace Unity1week202303.Installer
{
    public class TitleLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<AudioConfigView>();
            builder.RegisterEntryPoint<ConfigPresenter>();

            builder.RegisterComponentInHierarchy<TitleMenuView>();
            builder.RegisterEntryPoint<TitleMenuPresenter>();
            
            builder.RegisterBuildCallback(resolver =>
            {
                resolver.Resolve<AudioPlayer>().PlayBgm(0);
            });
        }
    }
}
