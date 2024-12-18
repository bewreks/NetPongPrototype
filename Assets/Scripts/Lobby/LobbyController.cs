using System;
using System.Collections.Generic;
using Interfaces;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Lobby
{
    public class LobbyController : IInitializable, IDisposable
    {
        [Inject] private IRelayConnection _connection;
        [Inject] private IAuthorization _authorization;
        [Inject] private LifetimeScope _scope;

        private List<ulong> _ids;

        public async void Initialize()
        {
            _ids = new List<ulong>();
            _connection.OnClientConnected += OnClientConnected;
            var model = await _authorization.GetModel();
            _connection.Initialize(model);
        }

        private void OnClientConnected(ulong id)
        {
            _ids.Add(id);
            if (_ids.Count == 2)
            {
                NetworkManager.Singleton.SceneManager.OnLoadComplete += (clientId, scene, mode) => Debug.Log($"{clientId} is loaded {scene} in {mode} mode");
                NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }

        public void Dispose()
        {
            _connection.OnClientConnected -= OnClientConnected;
        }
    }
}
