using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDied;

    [SerializeField] private int maxHealth = 5;

    private int originalMaxHealth;

    private int health;

    private float poisonTimer;
    private float poisonDamageTimer;
    private float poisonDamageTimerMax;

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

    private void Update()
    {
        if (poisonTimer <= 0f) return;

        poisonTimer -= Time.deltaTime;
        poisonDamageTimer -= Time.deltaTime;

        if (poisonDamageTimer < 0f)
        {
            Damage(1);
            poisonDamageTimer += poisonDamageTimerMax;
        }
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

    public void Poison(float damageInterval, float duration)
    {
        poisonTimer = duration;
        poisonDamageTimerMax = damageInterval;
        poisonDamageTimer = poisonDamageTimerMax;
    }

    public void Heal(int amount)
    {
        health += amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Die()
    {
        OnDied?.Invoke(this, EventArgs.Empty);
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
