using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClient", menuName = "Network/NetworkClient", order = 0)]
    public class NetworkClient : ScriptableObject
    {
        [SerializeField] private NetworkClientReceiver _networkClientReceiver;
        [SerializeField] private NetworkClientSender _networkClientSender;

        private const int BufferSize = 1024;
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        // public NetworkClientReceiver NetworkClientReceiver => _networkClientReceiver;
        // public NetworkClientSender NetworkClientSender => _networkClientSender;

        public void Init()
        {
            _networkClientReceiver.Init();
            _udpClient = new UDPClient(BufferSize, _networkClientReceiver);
            _tcpClient = new TCPClient(BufferSize, _networkClientReceiver);
        }
    }
}