using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerManager : PlayerManager
{
    public GameObject HealthbarPrefab;
    public HealthbarScript Healthbar;

    public override void Initialize(int id, string username, WeaponKind currentWeapon)
    {
        OnDie += PlayerDie;
        OnRespawn += PlayerRespawn;
        base.HealthChanged += HealthChanged;

        Healthbar.SetMaxHealth(MaxHealth);
        base.Initialize(id, username, currentWeapon);
    }

    private void HealthChanged(float hp)
    { 
        Healthbar.SetHealth(hp);
    }

    private void PlayerDie()
    {
        HealthbarPrefab.SetActive(false);
    }

    private void PlayerRespawn()
    {
        HealthbarPrefab.SetActive(true);
    }

    private void OnDestroy()
    {
        OnDie -= PlayerDie;
        OnRespawn -= PlayerRespawn;
        base.HealthChanged -= HealthChanged;
    }
}