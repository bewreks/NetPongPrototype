using Authorize;
using Interfaces;
using Lobby;
using Messages;
using VContainer;
using VContainer.Unity;

namespace Screens.StartScreen
{
    public class StartScreenScope : LifetimeScope
    {
        protected override async void Configure(IContainerBuilder builder)
        {
            var preloading = new Preloading.Preloading();
            preloading.AddStep(new StartMessagesLoadingStep(), 0);
            await preloading.StartLoading(builder);

            builder.Register<StartScreenController>(Lifetime.Singleton)
                   .AsImplementedInterfaces()
                   .AsSelf();
            builder.Register<IAuthorizationService, AutoAuthorizationService>(Lifetime.Scoped)
                   .AsImplementedInterfaces()
                   .AsSelf();
            builder.Register<IRelayConnectionService, LocalRelayConnectionServiceService>(Lifetime.Scoped);
        }
    }
}