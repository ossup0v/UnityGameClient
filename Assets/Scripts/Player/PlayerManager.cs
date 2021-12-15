using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Action<float> HealthChanged = delegate { };
    public Action<int> GrenadeCountChanged = delegate { };
    public Action OnDie = delegate { };
    public Action OnRespawn = delegate { };

    private WeaponsController weaponsController;
    public Transform WeaponPosition;
    public List<GameObject> WeaponPrefabs;

    public int Id;
    public string Username;
    public float CurrentHealth;
    public float MaxHealth;
    public MeshRenderer model;
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
                GrenadeCountChanged(value);
                _grenadeCount = value;
            }
        } 
    }

    public virtual void Initialize(int id, string username, WeaponKind currentWeapon)
    {
        Id = id;
        Username = username;
        CurrentHealth = MaxHealth;

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
        CurrentHealth = health;
        HealthChanged(health);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        model.enabled = false;
        weaponsController.GetSelectedWeapon().Disable();
        OnDie();
    }

    public void Respawn()
    {
        model.enabled = true;
        weaponsController.GetSelectedWeapon().MakeActive(WeaponPosition);
        OnRespawn();
        SetHealth(MaxHealth);
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
