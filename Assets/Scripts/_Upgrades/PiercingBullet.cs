using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : BulletUpgrade
{
    private UpgradeSO upgrade;
    private int enemiesHit;

    private void OnEnable()
    {
        enemiesHit = 0;
    }

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Piercing_Bullets);
    }

    public override void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy)
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Piercing_Bullets);

        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
        }

        enemiesHit++;

        int maxEnemyHit = (int) (upgrade.GetLevel() * upgrade.levelUpAmount);

        if (enemiesHit > maxEnemyHit) ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
