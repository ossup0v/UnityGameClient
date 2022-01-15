using System;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientReceiver", menuName = "Network/NetworkClientReceiver", order = 0)]
    public class NetworkClientReceiver : ScriptableObject
    {
        [System.NonSerialized] private ClientRoomNetworkBytesReader _clientRoomNetworkBytesReader;
        public IPacketHandlersHolder PacketHandlersHolder => _clientRoomNetworkBytesReader;
        public IBytesReadable BytesReader => _clientRoomNetworkBytesReader;
        
        public void Init()
        {
            _clientRoomNetworkBytesReader = new ClientRoomNetworkBytesReader();
        }
    }
}