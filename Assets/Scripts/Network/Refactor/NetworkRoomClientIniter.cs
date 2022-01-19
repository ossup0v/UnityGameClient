using System.Collections;
using System.Collections.Generic;
using Refactor;
using UnityEngine;

public class NetworkRoomClientIniter : MonoBehaviour
{
    [SerializeField] private NetworkRoomClientProvider _networkRoomClientProvider;

    private void Awake()
    {
        _networkRoomClientProvider.CreateNetworkRoomClient();
    }

    private void OnDestroy()
    {
        _networkRoomClientProvider.NetworkRoomClient.CloseConnection();
    }
}
