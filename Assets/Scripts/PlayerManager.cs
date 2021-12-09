using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int Id;
    public string Username;
    public float CurrentHealth;
    public float MaxHealth;
    public MeshRenderer model;
    public GameObject HealthbarPrefab;
    public HealthbarScript Healthbar;
    public int ItemAmount = 0;
    public void Initialize(int id, string username)
    {
        Id = id;
        Username = username;
        CurrentHealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
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
