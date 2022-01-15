using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event Action<int> GrenadeCountChanged = delegate { };
    public event Action PlayerDie = delegate { };
    public event Action PlayerRespawn = delegate { };
    public event Action InitializePostprocess = delegate { };
    public event Action StageDurationChanged = delegate { };
    public event Action StageIdChanged = delegate { };

    public int Team { get; private set; }
    //Weapons
    protected WeaponsController weaponsController;
    public Transform WeaponPosition;
    public List<GameObject> WeaponPrefabs;
    private int _grenadeCount = 0;
    public int GrenadeCount
    {
        get
        {
            return _grenadeCount;
        }
        set
        {
            if (value != _grenadeCount)
            {
                _grenadeCount = value;
                GrenadeCountChanged(value);
            }
        }
    }

    //Network
    public Guid Id;
    public string Username;

    //health
    protected HealthManager healthManager = new HealthManager();
    
    //model
    public MeshRenderer model;

    public int StageId;

    public long StageDurationTicks;
    
    //player bools
    private bool _isAlive = true;

    public virtual void Initialize(Guid id, string username, int team, WeaponKind currentWeapon)
    {
        Id = id;
        Username = username;
        Team = team;

        var weapons = WeaponPrefabs.Select(x => Instantiate(x, transform)).ToDictionary(x => x.GetComponent<WeaponBase>().Kind, x => x.GetComponent<WeaponBase>());

        foreach (var weapon in weapons.Values)
        {
            weapon.WeaponPrefab.enabled = false;
        }

        weaponsController = new WeaponsController(weapons, currentWeapon);

        ChooseWeapon(currentWeapon);

        InitializePostprocess();
    }

    public void ChooseWeapon(WeaponKind kind)
    {
        weaponsController.ChangeSelectedWeapon(kind, WeaponPosition);
    }

    public void SetHealth(float health)
    {
        healthManager.SetPureHealth(health);

        if (healthManager.IsDie)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isAlive == false)
            return;

        model.enabled = false;
        _isAlive = false;
        healthManager.SetPureHealth(healthManager.MinPlayerHealth);
        weaponsController.GetSelectedWeapon().Disable();
        PlayerDie();
    }

    public void Respawn()
    {
        model.enabled = true;
        _isAlive = true;
        weaponsController.GetSelectedWeapon().MakeActive(WeaponPosition);
        PlayerRespawn();
        SetHealth(healthManager.MaxPlayerHealth);
    }

    public void Shoot()
    {
        weaponsController.GetSelectedWeapon().Shoot();
    }

    public void Hit(WeaponKind weapon, Vector3 position)
    {
        weaponsController.GetSelectedWeapon().Hit(position);
    }

    public void SetBulletAmount(WeaponKind bullerFor, int maxBulletAmount, int currentBulletAmount)
    {
        weaponsController.SetBulletAmount(bullerFor, maxBulletAmount, currentBulletAmount);
    }

    public void SetStage(int stageId)
    {
        StageId = stageId;
        StageIdChanged();
    }

    internal void SetStageDurationTicks(long stageDurationTicks)
    {
        StageDurationTicks = stageDurationTicks;
        StageDurationChanged();
    }
}
