using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interfaces;
using PoolsAndFactories;
using VContainer;
namespace Lobby
{
    public class LobbyServiceLoader : ILoadingStep
    {
        public int Weight => 1;

        public UniTask Load(IContainerBuilder builder, Action<int> callback, CancellationToken token)
        {
            builder.Register<Factory<LobbyModel>>(Lifetime.Singleton);
            builder.Register<ObjectPool<LobbyModel, ConnectionParams>>(Lifetime.Singleton);
            
            return UniTask.CompletedTask;
        }
    }
}
