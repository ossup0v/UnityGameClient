using System;
using System.Collections.Generic;
using UnityEngine;

public class OtherPlayerManager : PlayerManager
{
    public GameObject HealthbarPrefab;
    public HealthbarScript Healthbar;
    public MeshRenderer EyesRenderer;

    public override void Initialize(Guid id, string username, int team, WeaponKind currentWeapon)
    {
        PlayerDie += OnPlayerDie;
        PlayerRespawn += OnPlayerRespawn;
        healthManager.HealthChanged += OnHealthChanged;

        Healthbar.SetMaxHealth(healthManager.MaxPlayerHealth);
        base.Initialize(id, username, team, currentWeapon);
    }

    private void OnHealthChanged(float hp)
    { 
        Healthbar.SetHealth(hp);
    }

    private void OnPlayerDie()
    {
        HealthbarPrefab.SetActive(false);
        EyesRenderer.enabled = false;
    }

    private void OnPlayerRespawn()
    {
        HealthbarPrefab.SetActive(true);
        EyesRenderer.enabled = true;
    }

    private void OnDestroy()
    {
        PlayerDie -= OnPlayerDie;
        PlayerRespawn -= OnPlayerRespawn;
        healthManager.HealthChanged -= OnHealthChanged;
    }
}