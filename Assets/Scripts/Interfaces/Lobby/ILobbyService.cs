using System;
using Cysharp.Threading.Tasks;
using Interfaces;

namespace Lobby
{
    public interface ILobbyService : IDisposable
    {
        public UniTask<ILobbyModel> CreateLobby();
        public UniTask<ILobbyModel> FindLobby();
        public UniTask<ILobbyModel> FindLobby(string code);
    }
}