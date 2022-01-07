using System.Net;
using UnityEngine;

public class NetworkClientHandler : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        Debug.Log("Welcome received");
        var message = packet.ReadString();
        var myId = packet.ReadGuid();
        var from = packet.ReadInt();

        Debug.Log($"Message from server {message}");
        NetworkManager.Instance.ServerClient.MyId = myId;
        NetworkManager.Instance.RoomClient.MyId = myId;
        Debug.Log($"Welcome received, in packet {myId}");

        if (from == 1)
        {
            NetworkClientSendServer.WelcomeReceived();
            NetworkManager.Instance.ServerClient.Udp.Connect(((IPEndPoint)NetworkManager.Instance.ServerClient.Tcp.Socket.Client.LocalEndPoint).Port);
        }
        else if (from == 2)
        {
            NetworkClientSendRoom.WelcomeReceived();
            NetworkManager.Instance.RoomClient.Udp.Connect(((IPEndPoint)NetworkManager.Instance.RoomClient.Tcp.Socket.Client.LocalEndPoint).Port);
        }
        else
            Debug.LogError($"unexcepected from - {from}, can't connect");
    }

    public static void SpawnPlayer(Packet packet)
    {
        Debug.Log("Spawn player called");

        var id = packet.ReadGuid();
        var username = packet.ReadString();
        var team = packet.ReadInt();
        var position = packet.ReadVector3();
        var rotation = packet.ReadQuaternion();
        var currentWeaponKind = packet.ReadInt();

        GameManager.Instance.SpawnPlayer(id, username, team, (WeaponKind)currentWeaponKind, position, rotation);
    }

    public static void PlayerPosition(Packet packet)
    {
        var id = packet.ReadGuid();
        var position = packet.ReadVector3();
        try
        {
            GameManager.Players[id].transform.position = position;
        }
        catch (System.Exception ex)
        {
            //Debug.LogError(ex.Message);
        }
    }

    public static void PlayerRotation(Packet packet)
    {
        var id = packet.ReadGuid();
        var rotation = packet.ReadQuaternion();

        try
        {
            GameManager.Players[id].transform.rotation = rotation;
        }
        catch (System.Exception ex)
        {
            //Debug.LogError(ex.Message);
        }
    }

    public static void PlayerDisconnected(Packet packet)
    {
        var id = packet.ReadGuid();

        Destroy(GameManager.Players[id].gameObject);
        GameManager.Players.Remove(id);
    }

    public static void PlayerHealth(Packet packet)
    {
        var id = packet.ReadGuid();
        var health = packet.ReadFloat();

        GameManager.Players[id].SetHealth(health);
    }

    public static void PlayerRespawned(Packet packet)
    {
        var id = packet.ReadGuid();

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

        GameManager.ItemSpawners[spawnerId].ItemPickup();
    }

    public static void SpawnProjectile(Packet packet)
    {
        var projectileId = packet.ReadInt();
        var position = packet.ReadVector3();
        var playerThrowedId = packet.ReadInt();

        GameManager.Instance.SpawnProjectile(projectileId, position);
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
        try
        {
            GameManager.Proectiles[projectieId].Explode(position);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public static void PlayerChooseWeapon(Packet packet)
    {
        var playerId = packet.ReadGuid();
        var weaponKind = packet.ReadInt();

        GameManager.GetPlayer(playerId)?.ChooseWeapon((WeaponKind)weaponKind);
    }

    public static void PlayerShoot(Packet packet)
    {
        var playerId = packet.ReadGuid();

        GameManager.GetPlayer(playerId)?.Shoot();
    }

    public static void PlayerHit(Packet packet)
    {
        var playerId = packet.ReadGuid();
        var weaponKind = (WeaponKind)packet.ReadInt();
        var position = packet.ReadVector3();

        GameManager.GetPlayer(playerId)?.Hit(weaponKind, position);
    }

    public static void BotChooseWeapon(Packet packet)
    {
        var botId = packet.ReadGuid();
        var weaponKind = packet.ReadInt();

        GameManager.GetBot(botId)?.ChooseWeapon((WeaponKind)weaponKind);
    }

    public static void BotShoot(Packet packet)
    {
        var botId = packet.ReadGuid();

        GameManager.GetBot(botId)?.Shoot();
    }

    public static void BotHit(Packet packet)
    {
        var botId = packet.ReadGuid();
        var weaponKind = (WeaponKind)packet.ReadInt();
        var position = packet.ReadVector3();

        GameManager.GetBot(botId)?.Hit(weaponKind, position);
    }

    public static void RatingTableInit(Packet packet)
    {
        var entities = new RatingEntity[packet.ReadInt()];

        for (int i = 0; i < entities.Length; i++)
        {
            entities[i] = new RatingEntity();
            entities[i].Id = packet.ReadGuid();
            entities[i].Username = packet.ReadString();
            entities[i].Killed = packet.ReadInt();
            entities[i].Died = packet.ReadInt();
        }

        RatingManager.Init(entities);
    }

    public static void RatingTableUpdateKillAndDeath(Packet packet)
    {
        var killerId = packet.ReadGuid();
        var diedId = packet.ReadGuid();

        RatingManager.UpdateKillAndDeath(killerId, diedId);
    }

    public static void RatingTableUpdateDeath(Packet packet)
    {
        var diedId = packet.ReadGuid();

        RatingManager.UpdateDeath(diedId);
    }

    public static void RatingTableNewPlayer(Packet packet)
    {
        var entity = new RatingEntity();

        entity.Id = packet.ReadGuid();
        entity.Username = packet.ReadString();

        RatingManager.Update(entity);
    }

    public static void PlayerGrenadeCount(Packet packet)
    {
        var playerId = packet.ReadGuid();
        var grenadeCount = packet.ReadInt();

        var player = GameManager.GetPlayer(playerId);

        if (player != null)
            player.GrenadeCount = grenadeCount;

    }

    public static void InitMap(Packet packet)
    {
        MapManager.Instance.InitializeMap(packet.ReadString());
    }

    internal static void SpawnBot(Packet packet)
    {
        var botId = packet.ReadGuid();
        var botPosition = packet.ReadVector3();
        var botWeaponKind = packet.ReadInt();

        GameManager.Instance.SpawnBot(botId, (WeaponKind)botWeaponKind, botPosition);
    }

    internal static void BotPosition(Packet packet)
    {
        var botId = packet.ReadGuid();
        var botPosition = packet.ReadVector3();

        var bot = GameManager.GetBot(botId);

        if (bot != null)
            bot.transform.position = botPosition;
    }

    internal static void BotRotation(Packet packet)
    {
        var botId = packet.ReadGuid();
        var botRotation = packet.ReadQuaternion();

        var bot = GameManager.GetBot(botId);

        if (bot != null)
            bot.transform.rotation = botRotation;
    }

    internal static void BotHealth(Packet packet)
    {
        var botId = packet.ReadGuid();
        var botHealth = packet.ReadFloat();

        GameManager.GetBot(botId)?.SetHealth(botHealth);
    }

    public static void RatingTableKilledBots(Packet packet)
    {
        var killerId = packet.ReadGuid();
        var killCount = packet.ReadInt();

        RatingManager.UpdateBotKills(killerId, killCount);
    }

    public static void PlayerScale(Packet packet)
    {
        Vector3 scale = packet.ReadVector3();
        var playerId = packet.ReadGuid();
        GameManager.GetPlayer(playerId)?.transform.lossyScale.Set(scale.x, scale.y, scale.z);
    }

    public static void ConnectToRoom(Packet packet)
    {
        string roomHost = packet.ReadString();
        int team = packet.ReadInt();
        int roomPort = packet.ReadInt();

        MetagameUI.Instance.DisableAll();
        NetworkManager.Instance.Team = team;
        NetworkManager.Instance.RoomClient.ConnectToServer(roomHost, roomPort);
    }

    public static void RoomList(Packet packet)
    {
        int roomsCount = packet.ReadInt();
        RoomListEntity[] rooms = new RoomListEntity[roomsCount];

        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i] = new RoomListEntity
            {
                RoomId = packet.ReadGuid(),
                Port = packet.ReadInt(),
                UserOwner = packet.ReadString(),
                Mode = packet.ReadString(),
                Title = packet.ReadString(),
                AvailableUserCount = packet.ReadInt(),
                UserInRoomCount = packet.ReadInt(),
            };
        }

        RoomManager.Instance.Fill(rooms);
    }

    public static void GameRoomSessionEnd(Packet packet)
    {
        MapManager.Instance.DestroyMap();
        
        foreach (var player in GameManager.Players)
        {
            Destroy(player.Value.gameObject);
        }

        GameManager.Players.Clear();

        foreach (var bot in GameManager.Bots)
        {
            Destroy(bot.Value.gameObject);
        }

        GameManager.Bots.Clear();

        foreach (var projectile in GameManager.Proectiles)
        {
            Destroy(projectile.Value.gameObject);
        }

        GameManager.Proectiles.Clear();

        foreach (var spawner in GameManager.ItemSpawners)
        {
            Destroy(spawner.Value.gameObject);
        }

        GameManager.ItemSpawners.Clear();

        MetagameUI.Instance.EnableAll();

        NetworkManager.Instance.RoomClient.Disconnect();
    }

    public static void HandleResponce(Packet packet)
    {
        NetworkResponseService<PacketResponse>
            .Instance
            .OnReceivePacket(
                PacketResponse.CreateFromPacket(packet));
    }
}