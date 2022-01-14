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

        public static NetworkClient S_NetworkClient; // TODO убрать

        private const int BufferSize = 1024;
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        [NonSerialized]
        private bool inited = false; // TODO: переделать

        internal void Send(PacketBase packetBase)
        {
            packetBase.SerializePacket();
            var bytesList = new List<byte>(packetBase.GetBytes());
            var bytesToSend = new byte[packetBase.ReadWritePosition + 4];
            bytesList.InsertRange(0, BitConverter.GetBytes(packetBase.ReadWritePosition));
            var bytes = bytesList.ToArray();
            Array.Copy(bytes, 0, bytesToSend, 0, packetBase.ReadWritePosition);
            _tcpClient.Send(bytesToSend);
            Debug.Log("sending " + bytesToSend.Length);
        }

        public NetworkClientReceiver NetworkClientReceiver => _networkClientReceiver;
        public NetworkClientSender NetworkClientSender => _networkClientSender;

        public void Init()
        {
            S_NetworkClient = this;
            _networkClientReceiver.Init();
            _udpClient = new UDPClient(BufferSize, _networkClientReceiver.ClientRoomNetworkPacketReceiver);
            _tcpClient = new TCPClient(BufferSize, _networkClientReceiver.ClientRoomNetworkPacketReceiver);
            // TODO переделать
            var qwe = new GameObject("test").AddComponent<WelcomeNetworkMono>();
        }

        public void Connect(string ip, int port)
        {
            if (inited == false)
            {
                Init();
                inited = true;
            }
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