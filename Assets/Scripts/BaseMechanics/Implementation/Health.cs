using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth;

    public float CurrentHealth { get; private set; }

    public bool IsDead = false;
    
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void ChangeHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);

        if (!IsDead && CurrentHealth <= 0)
        {
            IsDead = true;
            OnDeath?.Invoke();
        }
        else if (CurrentHealth > 0)
        {
            IsDead = false;
        }
    }
}
