using System;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientReceiver", menuName = "Network/NetworkClientReceiver", order = 0)]
    public class NetworkClientReceiver : ScriptableObject
    {
        [System.NonSerialized] private ClientRoomNetworkPacketsReceiver _clientRoomNetworkPacketReceiver;
        public ClientRoomNetworkPacketsReceiver ClientRoomNetworkPacketReceiver => _clientRoomNetworkPacketReceiver;
        
        public void Init()
        {
            _clientRoomNetworkPacketReceiver = new ClientRoomNetworkPacketsReceiver();
        }
    }
}