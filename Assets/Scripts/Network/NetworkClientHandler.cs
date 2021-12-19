using System.Net;
using UnityEngine;

public class NetworkClientHandler : MonoBehaviour
{
    public static void Welcome(Packet packet)
    {
        Debug.Log("Welcome received");
        var message = packet.ReadString();
        var myId = packet.ReadInt();

        Debug.Log($"Message from server {message}");
        NetworkClient.Instance.MyId = myId;
        NetworkClientSend.WelcomeReceived();

        NetworkClient.Instance.Udp.Connect(((IPEndPoint)NetworkClient.Instance.Tcp.Socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet packet)
    {
        Debug.Log("Spawn player called");

        var id = packet.ReadInt();
        var username = packet.ReadString();
        var position = packet.ReadVector3();
        var rotation = packet.ReadQuaternion();
        var currentWeaponKind = packet.ReadInt();

        GameManager.Instance.SpawnPlayer(id, username, (WeaponKind)currentWeaponKind, position, rotation);
    }

    public static void PlayerPosition(Packet packet)
    {
        var id = packet.ReadInt();
        var position = packet.ReadVector3();
        try
        {
            GameManager.Players[id].transform.position = position;
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
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
        var playerId = packet.ReadInt();
        var weaponKind = packet.ReadInt();

        GameManager.GetPlayer(playerId).ChooseWeapon((WeaponKind)weaponKind);
    }

    public static void PlayerShoot(Packet packet)
    {
        var playerId = packet.ReadInt();

        GameManager.GetPlayer(playerId).Shoot();
    }

    public static void PlayerHit(Packet packet)
    {
        var playerId = packet.ReadInt();
        var weaponKind = (WeaponKind)packet.ReadInt();
        var position = packet.ReadVector3();

        GameManager.GetPlayer(playerId).Hit(weaponKind, position);
    }

    public static void BotChooseWeapon(Packet packet)
    {
        var botId = packet.ReadInt();
        var weaponKind = packet.ReadInt();

        GameManager.GetBot(botId).ChooseWeapon((WeaponKind)weaponKind);
    }

    public static void BotShoot(Packet packet)
    {
        var botId = packet.ReadInt();

        GameManager.GetBot(botId).Shoot();
    }

    public static void BotHit(Packet packet)
    {
        var botId = packet.ReadInt();
        var weaponKind = (WeaponKind)packet.ReadInt();
        var position = packet.ReadVector3();

        GameManager.GetBot(botId).Hit(weaponKind, position);
    }

    public static void RatingTableInit(Packet packet)
    {
        var entities = new RatingEntity[packet.ReadInt()];

        for (int i = 0; i < entities.Length; i++)
        {
            entities[i] = new RatingEntity();
            entities[i].Id = packet.ReadInt();
            entities[i].Username = packet.ReadString();
            entities[i].Killed = packet.ReadInt();
            entities[i].Died = packet.ReadInt();
        }

        RatingManager.Init(entities);
    }

    public static void RatingTableUpdateKillAndDeath(Packet packet)
    {
        var killerId = packet.ReadInt();
        var diedId = packet.ReadInt();

        RatingManager.UpdateKillAndDeath(killerId, diedId);
    }

    public static void RatingTableUpdateDeath(Packet packet)
    {
        var diedId = packet.ReadInt();

        RatingManager.UpdateDeath(diedId);
    }

    public static void RatingTableNewPlayer(Packet packet)
    {
        var entity = new RatingEntity();

        entity.Id = packet.ReadInt();
        entity.Username = packet.ReadString();

        RatingManager.Update(entity);
    }

    public static void PlayerGrenadeCount(Packet packet)
    {
        var playerId = packet.ReadInt();
        var grenadeCount = packet.ReadInt();

        GameManager.GetPlayer(playerId).GrenadeCount = grenadeCount;
    }

    public static void InitMap(Packet packet)
    {
        MapManager.Instance.InitializeMap(packet.ReadString());
    }

    internal static void SpawnBot(Packet packet)
    {
        var botId = packet.ReadInt();
        var botPosition = packet.ReadVector3();
        var botWeaponKind = packet.ReadInt();

        GameManager.Instance.SpawnBot(botId, (WeaponKind)botWeaponKind, botPosition);
    }

    internal static void BotPosition(Packet packet)
    {
        var botId = packet.ReadInt();
        var botPosition = packet.ReadVector3();

        GameManager.Bots[botId].transform.position = botPosition;
    }

    internal static void BotRotation(Packet packet)
    {
        var botId = packet.ReadInt();
        var botRotation = packet.ReadQuaternion();

        GameManager.Bots[botId].transform.rotation = botRotation;
    }

    internal static void BotHealth(Packet packet)
    {
        var botId = packet.ReadInt();
        var botHealth = packet.ReadFloat();

        GameManager.Bots[botId].SetHealth(botHealth);
    }

    public static void RatingTableKilledBots(Packet packet)
    {
        var killerId = packet.ReadInt();
        var killCount = packet.ReadInt();

        RatingManager.UpdateBotKills(killerId, killCount);
    }
}