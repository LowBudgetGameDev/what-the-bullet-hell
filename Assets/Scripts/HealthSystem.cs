using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;

    [SerializeField] private int maxHealth = 5;

    private int health;

    private void Awake()
    {
        health = maxHealth;
    }

    public void Damage(int amount)
    {
        health -= amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;

        health = Mathf.Clamp(health, 0, maxHealth);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthNormalized()
    {
        return (float)health / maxHealth;
    }
}
