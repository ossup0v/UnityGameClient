using System;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "ClientRoomRoomEnter", menuName = "Network/ClientRoomRoomEnter", order = 0)]
    public class ClientRoomEnterRoom : ScriptableObject
    {
        public event Action EnteredRoom;

        [SerializeField] private string _serverRoomIP = "127.0.0.1"; // TODO: убрать отсюда
        [SerializeField] private int _serverRoomPort = 29000;
        [SerializeField] private NetworkRoomClientProvider _networkRoomClientProvider;
        private HelloReadPacketReceiver _helloReadPacketReceiver;

        [System.NonSerialized] private bool _connected;

        protected INetworkClient _networkClient => _networkRoomClientProvider.NetworkRoomClient;

        public bool TryConnectAndEnterRoom()
        {
            if (_connected)
            {
                return false;
            }
            _networkClient.Connect(_serverRoomIP, _serverRoomPort, OnConnectedToUDPServer, OnConnectedToTcpServer);
            _helloReadPacketReceiver = new HelloReadPacketReceiver(_networkClient.NetworkClientPacketsSender, _networkClient.PacketHandlersHolder);
            _helloReadPacketReceiver.PacketReceived += OnHelloPacketReceived;

            void OnConnectedToUDPServer()
            {

            }

            void OnConnectedToTcpServer()
            {
                var helloPacket = new HelloWritePacket();
                _networkClient.NetworkClientPacketsSender.SendTCP(helloPacket);
            }

            _connected = true;

            return true;
        }

        private void OnHelloPacketReceived()
        {
            EnteredRoom?.Invoke();
            _helloReadPacketReceiver.PacketReceived -= OnHelloPacketReceived;
            _helloReadPacketReceiver.Dispose();
            _helloReadPacketReceiver = null;
            GetClientCharacterData();
        }

        private void GetClientCharacterData()
        {
            var getClientCharacterDataPacket = new GetClientCharacterDataWritePacket();
            _networkClient.NetworkClientPacketsSender.SendTCP(getClientCharacterDataPacket);
        }
    }
}