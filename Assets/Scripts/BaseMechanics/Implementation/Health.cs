using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth;

    public float CurrentHealth { get; private set; }

    public bool IsDead { get; private set; }
    
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void ChangeHealth(float value)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + value, 0, MaxHealth);
        
        Debug.Log("Take damage: " + value + ", remain: " + CurrentHealth, this);

        if ((int) CurrentHealth == 0)
        {
            Debug.Log("Dead", this);
            OnDeath?.Invoke();
            IsDead = true;
        }
    }
}
