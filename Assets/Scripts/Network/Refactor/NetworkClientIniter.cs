using System.Collections;
using System.Collections.Generic;
using Refactor;
using UnityEngine;

public class NetworkClientIniter : MonoBehaviour
{
    [SerializeField] private NetworkClientProvider _networkClientProvider;

    private void Awake()
    {
        var networkClient = _networkClientProvider.NetworkClient;
        networkClient.Init();

    }

    private void Start()
    {
        _networkClientProvider.NetworkClient.TCPClient.ConnectedToServer += OnConnectedToTCPServer;
        _networkClientProvider.NetworkClient.Connect("127.0.0.1", 29000);
    }

    private void OnConnectedToTCPServer()
    {
        _networkClientProvider.NetworkClient.TCPClient.ConnectedToServer -= OnConnectedToTCPServer;
        var helloWritePacket = new HelloWritePacket();
        _networkClientProvider.NetworkClient.NetworkClientSender.NetworkClientPacketsSender.SendTCP(helloWritePacket);
    }

    private void OnDisable()
    {
        _networkClientProvider.NetworkClient.Disconnect();
    }
}
