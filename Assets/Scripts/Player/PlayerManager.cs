using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Action<float> OnHealthChange = delegate { };
    
    private WeaponsController weaponsController;
    public Transform WeaponPosition;
    
    public int Id;
    public string Username;
    public float CurrentHealth;
    public float MaxHealth;
    public MeshRenderer model;
    public GameObject HealthbarPrefab;
    public HealthbarScript Healthbar;
    public int ItemAmount = 0;

    public void Initialize(int id, string username, WeaponKind currentWeapon, Dictionary<WeaponKind, WeaponBase> availableWeapons)
    {
        Id = id;
        Username = username;
        CurrentHealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
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
        Healthbar.SetHealth(CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        model.enabled = false;
        HealthbarPrefab.SetActive(false);
    }

    public void Respawn()
    {
        model.enabled = true;
        HealthbarPrefab.SetActive(true);
        SetHealth(MaxHealth);
    }

    public void Shoot()
    {
        weaponsController.GetSelectedWeapon().Shoot();
    }
}
