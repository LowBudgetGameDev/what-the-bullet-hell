using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

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
            Heal(maxHealth);
        };
    }

    public void AddBonusHealth(int amount)
    {
        maxHealth = originalMaxHealth + amount;
        health = maxHealth;
    }

    public void Damage(int amount)
    {
        health -= amount;

        health = Mathf.Clamp(health, 0, maxHealth);

        OnDamaged?.Invoke(this, EventArgs.Empty);
        OnHealthChanged?.Invoke(this, EventArgs.Empty);

        if (health == 0)
        {
            Die();
        }

        SoundManager.Instance.PlayRandomSoundOfType(SoundManager.SoundType.Hit, 0.75f);
        CinemachineShake.Instance.ShakeCamera(3f, 0.25f);
    }

    public void Poison(int poisonDamage, float damageInterval, float duration)
    {
        RepeatedFunctionTimer.Create(() =>
        {
            if (this == null) return;

            Damage(poisonDamage);
        }, damageInterval, duration, true);
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
