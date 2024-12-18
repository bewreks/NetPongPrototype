using Unity.Multiplayer.Center.NetcodeForGameObjectsExample;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(ClientNetworkTransform))]
public class Player : NetworkBehaviour
{
    private void OnServerInitialized()
    {
        if (!IsOwner) Destroy(this);
    }

    private void Update()
    {
        if (!Application.isFocused) return;
        var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        point.z = 0;
        transform.position = point;
    }
}