using System;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientReceiver", menuName = "Network/NetworkClientReceiver", order = 0)]
    public class NetworkClientReceiver : ScriptableObject
    {
        [System.NonSerialized] private ClientRoomNetworkPacketsReceiver _clientRoomNetworkPacketReceiver;
        public IPacketHandlersHolder PacketHandlersHolder => _clientRoomNetworkPacketReceiver;
        public IBytesReadable BytesReader => _clientRoomNetworkPacketReceiver;
        
        public void Init()
        {
            _clientRoomNetworkPacketReceiver = new ClientRoomNetworkPacketsReceiver();
        }
    }
}