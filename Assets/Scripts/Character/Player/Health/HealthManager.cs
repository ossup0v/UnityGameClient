using System;
using UnityEngine;

public class HealthManager
{
    public bool IsDie => CurrentPlayerHealth <= MinPlayerHealth;

    public event Action<float> HealthChanged = delegate { };

    private float _currentPlayerHealth = 100.0f;

    public float CurrentPlayerHealth 
    { 
        get
        {
            return _currentPlayerHealth;
        }
        private set 
        {
            _currentPlayerHealth = value;
        }
    }

    public float MaxPlayerHealth { get; private set; } = 100;
    public float MinPlayerHealth { get; private set; } = 0;

    public HealthManager(float maxPlayerHealth = 100)
    {
        MaxPlayerHealth = maxPlayerHealth;
        _currentPlayerHealth = maxPlayerHealth;
    }

    public void SetPureHealth(float newPlayerHealth)
    {
        CurrentPlayerHealth = newPlayerHealth;
        HealthChanged(newPlayerHealth);
    }
}
