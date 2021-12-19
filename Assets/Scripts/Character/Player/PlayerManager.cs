using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Action<int> GrenadeCountChanged = delegate { };
    public Action PlayerDie = delegate { };
    public Action PlayerRespawn = delegate { };

    //Weapons
    private WeaponsController weaponsController;
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
    public int Id;
    public string Username;

    //health
    protected HealthManager healthManager = new HealthManager();
    
    //model
    public MeshRenderer model;

    //player bools
    private bool _isAlive = true;

    public virtual void Initialize(int id, string username, WeaponKind currentWeapon)
    {
        Id = id;
        Username = username;

        var weapons = WeaponPrefabs.Select(x => Instantiate(x, transform)).ToDictionary(x => x.GetComponent<WeaponBase>().Kind, x => x.GetComponent<WeaponBase>());

        foreach (var weapon in weapons.Values)
        {
            weapon.WeaponPrefab.enabled = false;
        }

        weaponsController = new WeaponsController(weapons, currentWeapon);

        ChooseWeapon(currentWeapon);
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
}
