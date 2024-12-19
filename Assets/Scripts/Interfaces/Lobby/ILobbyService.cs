using System;
using Cysharp.Threading.Tasks;
using Interfaces;

namespace Lobby
{
    public interface ILobbyService : IModelContainer<ILobbyModel>, IDisposable
    {
        public UniTask<ILobbyModel> CreateLobby();
        public UniTask<ILobbyModel> JoinLobby();
        public UniTask<ILobbyModel> JoinLobby(string code);
        public UniTask LeaveLobby(ILobbyModel model);
    }
}