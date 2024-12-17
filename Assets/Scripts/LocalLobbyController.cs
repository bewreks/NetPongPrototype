using System;
using System.Linq;
using System.Threading.Tasks;
using Unity.Multiplayer.Playmode;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Lobbies;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace DefaultNamespace
{
    public class LocalLobbyController : MonoBehaviour
    {
        private bool _tryingToJoin = true;
    
        private string _playerId;
        
        private async void Awake()
        {
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
        
        private async Task Authorize()
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            _playerId = AuthenticationService.Instance.PlayerId;
        }

        private async Task<bool> ConnectToRoom()
        {
            try
            {
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
            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                if (id != NetworkManager.Singleton.LocalClientId)
                {
                    NetworkManager.Singleton.SceneManager.OnLoadComplete += (clientId, scene, mode) => Debug.Log($"{clientId} is loaded {scene} in {mode} mode");
                    NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);
                }
            };
            NetworkManager.Singleton.StartHost();
        }
        
        private void OnDestroy()
        {
            _tryingToJoin = false;
        }
    }
}
