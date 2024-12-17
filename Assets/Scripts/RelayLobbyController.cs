using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
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

public class RelayLobbyController : MonoBehaviour
{
    
    private const string RoomName = "RoomName";
    
    private UnityTransport _transport;
    private bool _tryingToJoin = true;
    
    private string _playerId;
    private Lobby _lobby;

    private async void Awake()
    {
        _transport = FindAnyObjectByType<UnityTransport>();

        await Authorize();

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

    private async Task<bool> ConnectToRoom()
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
    
    private async Task CreateRoom()
    {
        var allocation = await RelayService.Instance.CreateAllocationAsync(2, "europe-central2");
        var code = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
        var options = new CreateLobbyOptions
        {
            Data = new Dictionary<string, DataObject> { { RoomName, new DataObject(DataObject.VisibilityOptions.Public, code) } }
        };
        _lobby = await LobbyService.Instance.CreateLobbyAsync("Lobby", 2, options);
        
        StartCoroutine(HeartbeatLobbyCoroutine(_lobby.Id, 15));
        
        _transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port, allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
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

    private async Task Authorize()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        _playerId = AuthenticationService.Instance.PlayerId;
    }

    private void OnDestroy()
    {
        try
        {
            StopAllCoroutines();
            if (_lobby != null)
            {
                if (_lobby.HostId == _playerId)
                {
                    LobbyService.Instance.DeleteLobbyAsync(_lobby.Id);
                }
                else
                {
                    LobbyService.Instance.RemovePlayerAsync(_lobby.Id, _playerId);
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
