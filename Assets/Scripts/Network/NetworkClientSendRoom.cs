using System;
using UnityEngine;

public class NetworkClientSendRoom
{
    #region Send
    private static void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        NetworkManager.Instance.RoomClient.Tcp.SendData(packet);
    }

    private static void SendUDPData(Packet packet)
    {
        packet.WriteLength();
        NetworkManager.Instance.RoomClient.Udp.SendData(packet);
    }
    #endregion

    #region Packets
    public static void WelcomeReceived()
    {
        using (var packet = new Packet((int)ClientToServer.welcomeReceived))
        {
            packet.Write(NetworkManager.Instance.RoomClient.MyId);

            SendTCPData(packet);
        }
    }

    internal static void PlayerChangeWeapon(int leftOrRight)
    {
        using (var packet = new Packet((int)ClientToGameRoom.playerChangeWeapon))
        {
            packet.Write(leftOrRight);

            SendTCPData(packet);
        }
    }

    public static void PlayerMovement(bool[] input)
    {
        using (var packet = new Packet((int)ClientToGameRoom.playerMovement))
        {
            packet.Write(input.Length);
            foreach (var duration in input)
            {
                packet.Write(duration);
            }
            packet.Write(GameManager.Players[NetworkManager.Instance.RoomClient.MyId].transform.rotation);

            SendUDPData(packet);
        }
    }

    public static void PlayerShoot(Vector3 duraction)
    {
        Debug.Log($"Shoot duraction {duraction}");
        using (var packet = new Packet(ClientToGameRoom.playerShooting))
        {
            packet.Write(duraction);

            SendTCPData(packet);
        }
    }

    public static void PlayerThrowItem(Vector3 duraction)
    {
        using (var packet = new Packet(ClientToGameRoom.playerThrowItem))
        {
            packet.Write(duraction);

            SendTCPData(packet);
        }
    }

    public static void PlayerRespawn()
    {
        using (var packet = new Packet(ClientToGameRoom.playerRespawn))
        {
            SendTCPData(packet);
        }
    }
    #endregion
}
