using System;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientSender", menuName = "Network/NetworkClientSender", order = 0)]
    public class NetworkClientSender : ScriptableObject
    {
        [System.NonSerialized] private NetworkClientPacketsSender _networkClientPacketsSender;
        public INetworkClientPacketsSender NetworkClientPacketsSender => _networkClientPacketsSender;

        public void Init(int bufferSize, UDPClient udpClient, TCPClient tcpClient)
        {
            _networkClientPacketsSender = new NetworkClientPacketsSender(bufferSize, udpClient, tcpClient);
        }
    }
}