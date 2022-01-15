using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Refactor
{
    // TODO вынесли куда-то, не делать ScriptableObject возможно для этого
    [CreateAssetMenu(fileName = "NetworkClient", menuName = "Network/NetworkClient", order = 0)]
    public class NetworkClient : ScriptableObject
    {
        [SerializeField] private NetworkClientReceiver _networkClientReceiver;
        [SerializeField] private NetworkClientSender _networkClientSender;

        [NonSerialized] private UDPClient _udpClient;
        [NonSerialized] private TCPClient _tcpClient;

        [field: NonSerialized]
        public int BufferSize { get; } = 1024;
        public NetworkClientReceiver NetworkClientReceiver => _networkClientReceiver;
        public NetworkClientSender NetworkClientSender => _networkClientSender;

        public UDPClient UDPClient => _udpClient;
        public TCPClient TCPClient => _tcpClient;

        public void Init()
        {
            _networkClientReceiver.Init();
            _udpClient = new UDPClient(BufferSize, _networkClientReceiver.BytesReader);
            _tcpClient = new TCPClient(BufferSize, _networkClientReceiver.BytesReader);
            _networkClientSender.Init(BufferSize, _udpClient, _tcpClient);
        }

        public void Connect(string ip, int port)
        {
            _udpClient.Connect(ip, port);
            _tcpClient.Connect(ip, port);
        }

        public void Disconnect()
        {
            _udpClient.CloseConnection();
            _tcpClient.CloseConnection();
        }
    }
}