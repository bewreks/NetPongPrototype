using Interfaces.PoolsAndFactories;
namespace Lobby
{
    public class LobbyModel : ILobbyModel, IPoolableObject<ConnectionParams>
    {
        public void Reinitialize(ConnectionParams tArgs)
        {
            
        }

        public void Dispose()
        {
            
        }

        public void Reinitialize()
        {
            
        }

        public void OnReturnToPool()
        {
            
        }

        public void Initialize(ConnectionParams t)
        {
            
        }
    }
}
