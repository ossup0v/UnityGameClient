using System;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientSender", menuName = "Network/NetworkClientSender", order = 0)]
    public class NetworkClientSender : ScriptableObject
    {
        [System.NonSerialized] private NetworkClientPacketsSender _networkClientPacketsSender;
        public NetworkClientPacketsSender NetworkClientPacketsSender => _networkClientPacketsSender;

        public void Init(UDPClient udpClient, TCPClient tcpClient)
        {
            _networkClientPacketsSender = new NetworkClientPacketsSender(udpClient, tcpClient);
        }
    }
}