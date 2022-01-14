using System;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientReceiver", menuName = "Network/NetworkClientReceiver", order = 0)]
    public class NetworkClientReceiver : ScriptableObject
    {
        [System.NonSerialized] private ClientRoomNetworkPacketReceiver _clientRoomNetworkPacketReceiver;
        public ClientRoomNetworkPacketReceiver ClientRoomNetworkPacketReceiver => _clientRoomNetworkPacketReceiver;
        
        public void Init()
        {
            _clientRoomNetworkPacketReceiver = new ClientRoomNetworkPacketReceiver();
        }
    }
}