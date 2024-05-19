using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : BulletUpgrade
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Explosive_Bullets);
    }

    public override void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy)
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Explosive_Bullets);

        float explosionRadius = UpgradeManager.Instance.GetLevelOfUpgrade(upgrade) * upgrade.levelUpAmount;

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.TryGetComponent(out HealthSystem health))
            {
                health.Damage(damageAmount);
            }
        }

        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
        }

        if (canDestroy) ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
