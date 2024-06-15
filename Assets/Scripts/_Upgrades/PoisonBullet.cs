using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBullet : BulletUpgrade
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Poison);
    }

    public override void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy)
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Poison);

        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            float poisonDuration = upgrade.GetLevel() * upgrade.levelUpAmount;

            healthSystem.Poison(1f, poisonDuration);
        }

        if (canDestroy) ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
