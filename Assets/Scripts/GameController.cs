using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class GameController : NetworkBehaviour
{
    public TMP_Text ping;

    private void Update()
    {
        if (NetworkManager.Singleton.IsConnectedClient)
        {
            ping.text = $"{NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetCurrentRtt(NetworkManager.Singleton.CurrentSessionOwner)} ms";
        }
    }
}
