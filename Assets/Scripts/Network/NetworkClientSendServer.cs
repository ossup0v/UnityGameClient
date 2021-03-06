using System;
using UnityEngine;

public class NetworkClientSendServer
{
    #region Send
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        NetworkManager.Instance.ServerClient.Tcp.SendData(packet);
    }

    private static void SendUDPData(Packet packet)
    {
        packet.WriteLength();
        NetworkManager.Instance.ServerClient.Udp.SendData(packet);
    }

    #endregion

    #region Packets
    public static void WelcomeReceived()
    {
        using (var packet = new Packet((int)ToServerFromClient.welcomeReceived))
        {
            packet.Write(NetworkManager.Instance.ServerClient.MyId);
            packet.Write(NetworkManager.Instance.Username ?? "DummyUserName");

            SendTCPData(packet);
        }
    }

    internal static void Register(string login, string password, string username, Action<bool, string> registerCallback)
    {
        using (var packet = new Packet((int)ToServerFromClient.registerUser))
        {
            var packetId = Guid.NewGuid();
            packet.Write(packetId);
            packet.Write(login);
            packet.Write(password);
            packet.Write(username);

            SendTCPData(packet);

            NetworkResponseService<PacketResponse>.Instance.SubscribeCallback(packetId, (response) =>
            {
                var result = response.ReadBool();
                var message = string.Empty;

                if (!result)
                    message = response.ReadString();

                registerCallback(result, message);

            }, null, false);
        }
    }

    internal static void JoinGameRoom(Guid roomId)
    {
        using (var packet = new Packet((int)ToServerFromClient.joinGameRoom))
        {
            packet.Write(roomId);

            SendTCPData(packet);
        }
    }

    public static void Login(string login, string password, Action<bool, string> loginCallback)
    {
        using (var packet = new Packet((int)ToServerFromClient.loginUser))
        {
            var packetId = Guid.NewGuid();
            packet.Write(packetId);
            packet.Write(login);
            packet.Write(password);

            SendTCPData(packet);

            NetworkResponseService<PacketResponse>.Instance.SubscribeCallback(packetId, (response) =>
            {
                var result = response.ReadBool();
                var message = string.Empty;

                if (!result)
                    message = response.ReadString();

                loginCallback(result, message);

            }, null, false);
        }
    }

    public static void CreateGameRoom(string mode, string title, int maxPlayerCount)
    {
        using (var packet = new Packet((int)ToServerFromClient.createGameRoom))
        {
            packet.Write(mode);
            packet.Write(title);
            packet.Write(maxPlayerCount);

            SendTCPData(packet);
        }
    }

    public static void StartSearchRoomGame()
    {
        using (var packet = new Packet((int)ToServerFromClient.startSearchGameRoom))
        {
            SendTCPData(packet);
        }
    }

    public static void CancelSearchRoomGame()
    {
        using (var packet = new Packet((int)ToServerFromClient.cancelSearchGameRoom))
        {
            SendTCPData(packet);
        }
    }
    #endregion
}
