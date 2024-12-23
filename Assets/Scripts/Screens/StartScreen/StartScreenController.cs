using System;
using System.Collections.Generic;
using Interfaces;
using Lobby;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Screens.StartScreen
{
    public class StartScreenController : IInitializable, IDisposable
    {
        [Inject] private IRelayConnectionService _connectionService;
        [Inject] private IAuthorizationService _authorizationService;
        [Inject] private LifetimeScope _scope;

        private List<ulong> _ids;

        public async void Initialize()
        {
            _ids = new List<ulong>();
            _connectionService.OnClientConnected += OnClientConnected;
            var model = await _authorizationService.GetModel();
            _connectionService.Initialize(model);
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
            _connectionService.OnClientConnected -= OnClientConnected;
        }
    }
}