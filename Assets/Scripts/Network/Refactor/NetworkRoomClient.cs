using System;

namespace Refactor
{
    public class NetworkRoomClient : INetworkClient
    {
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        private ClientRoomNetworkBytesReader _clientRoomNetworkBytesReader;
        private NetworkClientPacketsSender _networkClientPacketsSender;        

        public IPacketHandlersHolder PacketHandlersHolder => _clientRoomNetworkBytesReader;
        public INetworkClientPacketsSender NetworkClientPacketsSender => _networkClientPacketsSender;

        public int BufferSize { get; } = 1024;

        public NetworkRoomClient()
        {
            _clientRoomNetworkBytesReader = new ClientRoomNetworkBytesReader();
            _udpClient = new UDPClient(BufferSize, _clientRoomNetworkBytesReader);
            _tcpClient = new TCPClient(BufferSize, _clientRoomNetworkBytesReader);
            _networkClientPacketsSender = new NetworkClientPacketsSender(BufferSize, _udpClient, _tcpClient);
        }

        public void Connect(string ip, int port, Action connectedToUDPServer, Action connectedToTcpServer)
        {
            _udpClient.ConnectedToServer += OnConnectedToUDPServer;
            _udpClient.Connect(ip, port);
            _tcpClient.ConnectedToServer += OnConnectedToTCPServer;
            _tcpClient.Connect(ip, port);

            void OnConnectedToUDPServer()
            {
                _udpClient.ConnectedToServer -= OnConnectedToUDPServer;
                connectedToUDPServer?.Invoke();
            }

            void OnConnectedToTCPServer()
            {
                _tcpClient.ConnectedToServer -= OnConnectedToTCPServer;
                connectedToTcpServer?.Invoke();
            }
        }

        public void CloseConnection()
        {
            _udpClient.CloseConnection();
            _tcpClient.CloseConnection();
        }
    }
}