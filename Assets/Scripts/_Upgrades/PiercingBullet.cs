using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingBullet : MonoBehaviour, IUpgrade
{
    public class PiercingBulletUpgrade : MonoBehaviour, IBulletUpgrade
    {
        private UpgradeSO upgrade;
        private int enemiesHit;
        private bool isCounter;

        public void Setup(UpgradeSO upgrade, bool isCounter)
        {
            this.upgrade = upgrade;
            this.isCounter = isCounter;
        }

        private void Start()
        {
            upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Piercing_Bullets);
        }

        public void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy)
        {
            if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Piercing_Bullets);

            if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Damage(damageAmount);
            }

            enemiesHit++;

            int maxEnemyHit = (int) upgrade.GetUpgradeAmount(isCounter);

            if (enemiesHit > maxEnemyHit) ObjectPooler.Instance.DestoryWithPool(transform);
        }
    }

    private UpgradeSO upgrade;
    private bool isCounter;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Piercing_Bullets);
    }

    public void OnShoot(Transform bullet)
    {
        if (bullet.GetComponent<PiercingBulletUpgrade>() != null) return;

        PiercingBulletUpgrade upgradeScript = bullet.gameObject.AddComponent<PiercingBulletUpgrade>();

        upgradeScript.Setup(upgrade, isCounter);
    }

    public void SetIsCounter(bool isCounter)
    {
        this.isCounter = isCounter;
    }

    public void OnAdded()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Piercing_Bullets);
    }

    public UpgradeSO GetUpgrade()
    {
        return upgrade;
    }
}
