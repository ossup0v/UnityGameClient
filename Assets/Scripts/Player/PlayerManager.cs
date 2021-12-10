using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public WeaponBase CurrentWeapon { get; private set; }
    private List<WeaponBase> AvailavleWeapons;


    public int Id;
    public string Username;
    public float CurrentHealth;
    public float MaxHealth;
    public MeshRenderer model;
    public GameObject HealthbarPrefab;
    public HealthbarScript Healthbar;
    public int ItemAmount = 0;

    public void Initialize(int id, string username, WeaponKind currentWeapon, List<WeaponBase> availableWeapons)
    {
        Id = id;
        Username = username;
        CurrentHealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
        AvailavleWeapons = availableWeapons;
        ChooseWeapon(currentWeapon);
    }

    public void ChooseWeapon(WeaponKind kind)
    {
        CurrentWeapon?.BulletPrefab?.SetActive(false);
        CurrentWeapon?.WeaponPrefab?.SetActive(false);
        CurrentWeapon = AvailavleWeapons.First(x => x.Kind == kind);
        CurrentWeapon.BulletPrefab.SetActive(true);
        CurrentWeapon.WeaponPrefab.SetActive(true);
    }

    public void SetHealth(float health)
    {
        CurrentHealth = health;
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
}
