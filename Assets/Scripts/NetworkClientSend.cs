using System;
using UnityEngine;

public class NetworkClientSend
{
    #region Send
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        NetworkClient.Instance.Tcp.SendData(packet);
    }

    private static void SendUDPData(Packet packet)
    {
        packet.WriteLength();
        NetworkClient.Instance.Udp.SendData(packet);
    }
    #endregion

    #region Packets
    public static void WelcomeReceived()
    {
        using (var packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(NetworkClient.Instance.MyId);
            packet.Write(UIManager.Instance.UsernameFiled.text);

            SendTCPData(packet);
        }
    }

    public static void PlayerMovement(bool[] input)
    {
        using (var packet = new Packet((int)ClientPackets.playerMovement))
        {
            packet.Write(input.Length);
            foreach (var duration in input)
            {
                packet.Write(duration);
            }
            packet.Write(GameManager.Players[NetworkClient.Instance.MyId].transform.rotation);

            SendUDPData(packet);
        }
    }

    public static void PlayerShoot(Vector3 duraction)
    {
        Debug.Log($"Shoot duraction {duraction}");
        using (var packet = new Packet(ClientPackets.playerShooting))
        {
            packet.Write(duraction);

            SendTCPData(packet);
        }
    }

    public static void PlayerThrowItem(Vector3 duraction)
    {
        using (var packet = new Packet(ClientPackets.playerThrowItem))
        {
            packet.Write(duraction);

            SendTCPData(packet);
        }
    }
    #endregion
}
