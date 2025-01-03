﻿using System;
using Cysharp.Threading.Tasks;
using Interfaces;
using VContainer.Unity;

namespace Lobby
{
    public interface IRelayConnectionService : IDisposable
    {
        event Action<ulong> OnClientConnected; 
        
        UniTask CreateRoom();
        UniTask<bool> ConnectToRoom();
        void Initialize(IAuthorizationModel model);
    }
}
