using System.Net;
using UnityEngine;

public class NetworkClientHandler : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        var message = packet.ReadString();
        var myId = packet.ReadInt();

        Debug.Log($"Message from server {message}");
        NetworkClient.Instance.MyId = myId;
        NetworkClientSend.WelcomeReceived();

        NetworkClient.Instance.Udp.Connect(((IPEndPoint)NetworkClient.Instance.Tcp.Socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet packet)
    {
        var id = packet.ReadInt();
        var username = packet.ReadString();
        var position = packet.ReadVector3();
        var rotation = packet.ReadQuaternion();

        GameManager.Instance.SpawnPlayer(id, username, position, rotation);
    }

    public static void PlayerPosition(Packet packet)
    {
        var id = packet.ReadInt();
        var position = packet.ReadVector3();

        GameManager.Players[id].transform.position = position;
    }

    public static void PlayerRotation(Packet packet)
    {
        var id = packet.ReadInt();
        var rotation = packet.ReadQuaternion();

        GameManager.Players[id].transform.rotation = rotation;
    }

    public static void PlayerDisconnected(Packet packet)
    {
        var id = packet.ReadInt();

        Destroy(GameManager.Players[id].gameObject);
        GameManager.Players.Remove(id);
    }

    public static void PlayerHealth(Packet packet)
    {
        var id = packet.ReadInt();
        var health = packet.ReadFloat();

        GameManager.Players[id].SetHealth(health);
    }

    public static void PlayerRespawned(Packet packet)
    {
        var id = packet.ReadInt();

        GameManager.Players[id].Respawn();
    }

    public static void CreateItemSpawner(Packet packet)
    {
        var spawnerId = packet.ReadInt();
        var position = packet.ReadVector3();
        var hasItem = packet.ReadBool();

        GameManager.Instance.CreateItemSpawner(spawnerId, hasItem, position);
    }

    public static void ItemSpawned(Packet packet)
    {
        var spawnerId = packet.ReadInt();

        GameManager.ItemSpawners[spawnerId].ItemSpawned();
    }

    public static void ItemPickup(Packet packet)
    {
        var spawnerId = packet.ReadInt();
        var playerId = packet.ReadInt();

        GameManager.Players[playerId].ItemAmount++;
        GameManager.ItemSpawners[spawnerId].ItemPickup();
    }

    public static void SpawnProjectile(Packet packet)
    {
        var projectileId = packet.ReadInt();
        var position = packet.ReadVector3();
        var playerThrowedId = packet.ReadInt();

        GameManager.Instance.SpawnProjectile(projectileId, position);
        GameManager.Players[playerThrowedId].ItemAmount--;
    }

    public static void ProjectilePosition(Packet packet)
    {
        var projectieId = packet.ReadInt();
        var position = packet.ReadVector3();

        GameManager.Proectiles[projectieId].transform.position = position;
    }

    public static void ProjectileExploded(Packet packet)
    {
        var projectieId = packet.ReadInt();
        var position = packet.ReadVector3();

        GameManager.Proectiles[projectieId].Explode(position);
    }
}