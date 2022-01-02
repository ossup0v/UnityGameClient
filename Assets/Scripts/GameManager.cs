using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private static Dictionary<Guid, BotManager> _bots = new Dictionary<Guid, BotManager>();
    public static Dictionary<Guid, PlayerManager> Players = new Dictionary<Guid, PlayerManager>();
    public static Dictionary<int, ItemSpawner> ItemSpawners = new Dictionary<int, ItemSpawner>();
    public static Dictionary<int, ProjectileManager> Proectiles = new Dictionary<int, ProjectileManager>();

    //TODO: убрать потом отсюда
    [SerializeField] private NetworkClientUpdater _networkClientUpdater;

    public PlayerManager CurrentPlayer => Players[NetworkManager.Instance.ServerClient.MyId];
    public static BotManager GetBot(Guid id)
    {
        _bots.TryGetValue(id, out var bot);
        return bot;
    }
    public static void RemoveBot(Guid id)
    { 
        _bots.Remove(id);
    }

    public GameObject BotPrefab;
    public GameObject LocalPlayerPrefab;
    public GameObject OtherPlayerPrefab;
    public GameObject ItemSpawnerPrefab;
    public GameObject ProjectilePrefab;
    public WeaponFactory WeaponFactory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError($"{nameof(GameManager)} singletone error!");
            Destroy(this);
        }
    }

    public static PlayerManager GetPlayer(Guid playerId)
    {
        Players.TryGetValue(playerId, out var player);
        return player;
    }

    public void SpawnPlayer(Guid id, string username, WeaponKind currentWeapon, Vector3 position, Quaternion rotation)
    {
        var player = NetworkManager.Instance.ServerClient.MyId == id ?
            Instantiate(LocalPlayerPrefab, position, rotation) :
            Instantiate(OtherPlayerPrefab, position, rotation);

        var playerManager = player.GetComponent<PlayerManager>();

        playerManager.Initialize(id, username, currentWeapon);

        Players.Add(id, playerManager);
        // TODO: это временно
        _networkClientUpdater.Enable();
    }

    public void SpawnBot(Guid id, WeaponKind currentWeapon, Vector3 position)
    {
        var bot = Instantiate(BotPrefab, position, Quaternion.identity);

        var botManager = bot.GetComponent<BotManager>();

        botManager.Initialize(id, currentWeapon);

        _bots.Add(id, botManager);
    }

    public void CreateItemSpawner(int spawnerId, bool hasItem, Vector3 position)
    {
        var spawnerPrefab = Instantiate(ItemSpawnerPrefab, position, ItemSpawnerPrefab.transform.rotation);
        var spawner = spawnerPrefab.GetComponent<ItemSpawner>();
        spawner.Initialize(spawnerId, hasItem);

        ItemSpawners.Add(spawnerId, spawner);
    }

    public void SpawnProjectile(int id, Vector3 position)
    {
        var projectile = Instantiate(ProjectilePrefab, position, Quaternion.identity);
        var projectileManager = projectile.GetComponent<ProjectileManager>();
        projectileManager.Initialize(id);
        Proectiles.Add(id, projectileManager);
    }
}
