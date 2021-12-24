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
        using (var packet = new Packet((int)ClientToServer.welcomeReceived))
        {
            packet.Write(NetworkManager.Instance.ServerClient.MyId);
            packet.Write(NetworkManager.Instance.Username ?? "DummyUserName");

            SendTCPData(packet);
        }
    }

    internal static void Register(string login, string password, string username)
    {
        using (var packet = new Packet((int)ClientToServer.registerUser))
        {
            packet.Write(login);
            packet.Write(password);
            packet.Write(username);

            SendTCPData(packet);
        }
    }

    internal static void JoinGameRoom(Guid roomId)
    {
        using (var packet = new Packet((int)ClientToServer.joinGameRoom))
        {
            packet.Write(roomId);

            SendTCPData(packet);
        }
    }

    public static void Login(string login, string password)
    {
        using (var packet = new Packet((int)ClientToServer.loginUser))
        {
            packet.Write(login);
            packet.Write(password);

            SendTCPData(packet);
        }
    }

    public static void CreateGameRoom(string mode, string title, string maxPlayerCount)
    {
        using (var packet = new Packet((int)ClientToServer.createGameRoom))
        {
            packet.Write(mode);
            packet.Write(title);
            packet.Write(maxPlayerCount);

            SendTCPData(packet);
        }
    }
    #endregion
}
