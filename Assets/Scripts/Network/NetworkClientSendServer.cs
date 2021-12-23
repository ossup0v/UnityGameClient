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

    internal static void JoinGameRoom()
    {
        using (var packet = new Packet((int)ClientToServer.joinGameRoom))
        {
            SendTCPData(packet);
        }
    }
    
    #endregion
}
