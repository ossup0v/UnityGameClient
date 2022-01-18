using System.Collections;
using System.Collections.Generic;
using Refactor;
using UnityEngine;

public class NetworkClientIniter : MonoBehaviour
{
    private UDPClient _udpClient;
    private TCPClient _tcpClient;

    private ClientRoomNetworkBytesReader _clientRoomNetworkBytesReader;
    private NetworkClientPacketsSender _networkClientPacketsSender;

    public int BufferSize { get; } = 1024;
    // [SerializeField] private NetworkClientProvider _networkClientProvider;

    private void Awake()
    {
        _clientRoomNetworkBytesReader = new ClientRoomNetworkBytesReader();
        _udpClient = new UDPClient(BufferSize, _clientRoomNetworkBytesReader);
        _tcpClient = new TCPClient(BufferSize, _clientRoomNetworkBytesReader);
        _networkClientPacketsSender = new NetworkClientPacketsSender(BufferSize, _udpClient, _tcpClient);
        TestConnect();
    }

    private void TestConnect()
    {
        var ip = "127.0.0.1";
        var port = 29000;
        _tcpClient.ConnectedToServer += OnConnectedToTCPServer;
        _udpClient.Connect(ip, port);
        _tcpClient.Connect(ip, port);
    }

    // private void Start()
    // {
    //     _networkClientProvider.NetworkClient.TCPClient.ConnectedToServer += OnConnectedToTCPServer;
    //     _networkClientProvider.NetworkClient.Connect("127.0.0.1", 29000);
    // }

    private void OnConnectedToTCPServer()
    {
        _tcpClient.ConnectedToServer -= OnConnectedToTCPServer;
        var helloWritePacket = new HelloWritePacket();
        _networkClientPacketsSender.SendTCP(helloWritePacket);
    }

    private void OnDisable()
    {
        _udpClient.CloseConnection();
        _tcpClient.CloseConnection();
        _clientRoomNetworkBytesReader.Dispose();        
    }
}
