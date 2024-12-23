using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Interfaces;
using Unity.Multiplayer.Playmode;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Lobby
{
    public class UnityRelayConnectionServiceService : IRelayConnectionService
    {
        private const string RoomName = "RoomName";
        
        private bool _tryingToJoin = true;
        private Unity.Services.Lobbies.Models.Lobby _lobby;
        private UnityTransport _transport;
        private IAuthorizationModel _model;

        public event Action<ulong> OnClientConnected = _ => {};

        public async void Initialize(IAuthorizationModel model)
        {
            _model = model;
            _transport = Object.FindAnyObjectByType<UnityTransport>();
            
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

        public async UniTask CreateRoom()
        {
            var allocation = await RelayService.Instance.CreateAllocationAsync(2, "europe-central2");
            var code = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            var options = new CreateLobbyOptions
            {
                Data = new Dictionary<string, DataObject> { { RoomName, new DataObject(DataObject.VisibilityOptions.Public, code) } }
            };
            _lobby = await LobbyService.Instance.CreateLobbyAsync("Lobby", 2, options);
        
            _transport.StartCoroutine(HeartbeatLobbyCoroutine(_lobby.Id, 15));
        
            _transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                OnClientConnected(id);
                if (id != NetworkManager.Singleton.LocalClientId)
                {
                    NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
                }
            };
            NetworkManager.Singleton.StartHost();
        }
        
        private IEnumerator HeartbeatLobbyCoroutine(string lobbyId, float seconds)
        {
            var delay = new WaitForSecondsRealtime(seconds);
            while (true)
            {
                LobbyService.Instance.SendHeartbeatPingAsync(lobbyId);
                yield return delay;
            }
        }

        public async UniTask<bool> ConnectToRoom()
        {
            try
            {
                _lobby = await LobbyService.Instance.QuickJoinLobbyAsync();

                var allocation = await RelayService.Instance.JoinAllocationAsync(_lobby.Data[RoomName].Value);

                _transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);

                NetworkManager.Singleton.StartClient();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            try
            {
                OnClientConnected = _ => {};
                _transport.StopAllCoroutines();
                if (_lobby != null)
                {
                    if (_lobby.HostId == _model.PlayerId)
                    {
                        LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);
                    }
                    else
                    {
                        LobbyService.Instance.RemovePlayerAsync(_lobby.Id, _model.PlayerId);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            _tryingToJoin = false;
        }
    }
}
