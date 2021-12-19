using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static Dictionary<int, BotManager> Bots = new Dictionary<int, BotManager>();
    public static Dictionary<int, PlayerManager> Players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ItemSpawner> ItemSpawners = new Dictionary<int, ItemSpawner>();
    public static Dictionary<int, ProjectileManager> Proectiles = new Dictionary<int, ProjectileManager>();

    public PlayerManager CurrentPlayer => Players[NetworkClient.Instance.MyId];

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

    public static PlayerManager GetPlayer(int playerId)
    {
        return Players[playerId];
    }

    public static BotManager GetBot(int botId)
    {
        return Bots[botId];
    }

    public void SpawnPlayer(int id, string username, WeaponKind currentWeapon, Vector3 position, Quaternion rotation)
    {
        var player = NetworkClient.Instance.MyId == id ?
            Instantiate(LocalPlayerPrefab, position, rotation) :
            Instantiate(OtherPlayerPrefab, position, rotation);

        var playerManager = player.GetComponent<PlayerManager>();

        playerManager.Initialize(id, username, currentWeapon);

        Players.Add(id, playerManager);
    }

    public void SpawnBot(int id, WeaponKind currentWeapon, Vector3 position)
    {
        var bot = Instantiate(BotPrefab, position, Quaternion.identity);

        var botManager = bot.GetComponent<BotManager>();

        botManager.Initialize(id, currentWeapon);

        Bots.Add(id, botManager);
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
