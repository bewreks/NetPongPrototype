using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using Interfaces;
using Unity.Multiplayer.Playmode;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lobby
{
    public class LocalRelayConnection : IRelayConnection
    {
        private bool _tryingToJoin = true;

        public event Action<ulong> OnClientConnected = _ => {};

        public async void Initialize(IAuthorizationModel model)
        {
            if (CurrentPlayer.ReadOnlyTags().Contains("client"))
            {
                while (_tryingToJoin)
                {
                    _tryingToJoin = !await ConnectToRoom();
                }
                return;
            }

            if (CurrentPlayer.ReadOnlyTags().Contains("host"))
            {
                await CreateRoom();
            }
        }

        public UniTask CreateRoom()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                OnClientConnected(id);
                if (id != NetworkManager.Singleton.LocalClientId)
                {
                    NetworkManager.Singleton.SceneManager.OnLoadComplete += (clientId, scene, mode) => Debug.Log($"{clientId} is loaded {scene} in {mode} mode");
                    NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
                }
            };
            NetworkManager.Singleton.StartHost();
            return UniTask.CompletedTask; 
        }

        public UniTask<bool> ConnectToRoom()
        {
            try
            {
                NetworkManager.Singleton.StartClient();
            }
            catch (Exception)
            {
                return UniTask.FromResult(false);
            }

            return UniTask.FromResult(true);
        }

        public void Dispose()
        {
            OnClientConnected = _ => {};
            _tryingToJoin = false;
        }
    }
}
