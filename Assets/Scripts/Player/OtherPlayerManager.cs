using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerManager : PlayerManager
{
    public GameObject HealthbarPrefab;
    public HealthbarScript Healthbar;

    public override void Initialize(int id, string username, WeaponKind currentWeapon, Dictionary<WeaponKind, WeaponBase> availableWeapons)
    {
        OnDie += OnPlayerDie;
        OnRespawn += OnPlayerRespawn;
        OnHealthChange += OnHealthChanged;

        Healthbar.SetMaxHealth(MaxHealth);
        base.Initialize(id, username, currentWeapon, availableWeapons);
    }

    private void OnHealthChanged(float hp)
    { 
        Healthbar.SetHealth(hp);
    }

    private void OnPlayerDie()
    {
        HealthbarPrefab.SetActive(false);
    }

    private void OnPlayerRespawn()
    {
        HealthbarPrefab.SetActive(true);
    }

    private void OnDestroy()
    {
        OnDie -= OnPlayerDie;
        OnRespawn -= OnPlayerRespawn;
        OnHealthChange -= OnHealthChanged;
    }
}