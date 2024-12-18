using Authorize;
using Interfaces;
using VContainer;
using VContainer.Unity;

namespace Lobby
{
    public class LobbyScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LobbyController>(Lifetime.Singleton)
                   .AsImplementedInterfaces()
                   .AsSelf();
            builder.Register<IAuthorization, AutoAuthorization>(Lifetime.Scoped)
                   .AsImplementedInterfaces()
                   .AsSelf();
            builder.Register<IRelayConnection, LocalRelayConnection>(Lifetime.Scoped);
        }
    }
}
