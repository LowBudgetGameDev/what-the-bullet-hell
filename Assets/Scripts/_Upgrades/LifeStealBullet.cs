using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealBullet : BulletUpgrade
{
    private UpgradeSO upgrade;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Life_Steal);
    }

    public override void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy)
    {
        if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Life_Steal);

        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);

            float randomFloat = Random.Range(0f, 1f);

            if (randomFloat < UpgradeManager.Instance.GetLevelOfUpgrade(upgrade) * upgrade.levelUpAmount)
            {
                shooter.GetComponent<HealthSystem>().Heal(damageAmount);
            }
        }

        if (canDestroy) ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
