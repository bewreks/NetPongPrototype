using Cysharp.Threading.Tasks;
using PoolsAndFactories;
using VContainer;
namespace Lobby
{
    public class SinglePlayerLobbyService : ILobbyService
    {
        [Inject] private ObjectPool<LobbyModel, ConnectionParams> _pool;

        public UniTask<ILobbyModel> CreateLobby()
        {
            var lobbyModel = _pool.Get(new ConnectionParams
            {
                IpV4 = "127.0.0.1",
                Port = 7777
            });
            return UniTask.FromResult<ILobbyModel>(lobbyModel);
        }

        public UniTask<ILobbyModel> FindLobby()
        {
            return CreateLobby();
        }

        public UniTask<ILobbyModel> FindLobby(string code)
        {
            return CreateLobby();
        }

        public void Dispose()
        {
            
        }
    }

}
