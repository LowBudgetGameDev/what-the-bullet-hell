using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthChanged;

    [SerializeField] private int maxHealth = 5;

    private int originalMaxHealth;

    private int health;

    private void Awake()
    {
        health = maxHealth;
        originalMaxHealth = maxHealth;
    }

    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeLevelUp += (object sender, EventArgs e) =>
        {
            AddBonusHealth();
        };
    }

    private void AddBonusHealth()
    {
        if (this == null) return;

        HealthUpgrade healthUpgrade = GetComponent<HealthUpgrade>();

        if (healthUpgrade == null) return;

        maxHealth = originalMaxHealth + healthUpgrade.GetExtraHealthAmount();
        health = maxHealth;
    }

    public void Damage(int amount)
    {
        health -= amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        OnHealthChanged?.Invoke(this, EventArgs.Empty);

        if (health == 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        health += amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
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
