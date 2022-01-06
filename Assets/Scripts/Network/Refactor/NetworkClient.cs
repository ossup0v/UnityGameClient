using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClient", menuName = "Network/NetworkClient", order = 0)]
    public class NetworkClient : NetworkClientBase, IBytesReadable
    {
        private const int BufferSize = 1024;
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        public void Init()
        {
            _udpClient = new UDPClient(BufferSize, this);
            _tcpClient = new TCPClient(BufferSize, this);
        }

        public void Read(byte[] bytes)
        {
            var packetNumber = 0;
        }
    }
}