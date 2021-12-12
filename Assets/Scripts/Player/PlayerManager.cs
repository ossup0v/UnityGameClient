using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Action<float> OnHealthChange = delegate { };
    public Action OnDie = delegate { };
    public Action OnRespawn = delegate { };

    private WeaponsController weaponsController;
    public Transform WeaponPosition;
    
    public int Id;
    public string Username;
    public float CurrentHealth;
    public float MaxHealth;
    public MeshRenderer model;
    public int ItemAmount = 0;

    public virtual void Initialize(int id, string username, WeaponKind currentWeapon, Dictionary<WeaponKind, WeaponBase> availableWeapons)
    {
        Id = id;
        Username = username;
        CurrentHealth = MaxHealth;
        weaponsController = new WeaponsController(availableWeapons, currentWeapon);

        ChooseWeapon(currentWeapon);
    }

    public void ChooseWeapon(WeaponKind kind)
    {
        weaponsController.ChangeSelectedWeapon(kind, WeaponPosition);
    }

    public void SetHealth(float health)
    {
        CurrentHealth = health;
        OnHealthChange(health);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        model.enabled = false;
        OnDie();
    }

    public void Respawn()
    {
        model.enabled = true;
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
