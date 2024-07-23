using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonBullet : MonoBehaviour, IUpgrade
{
    public class PoisonBulletUpgrade : MonoBehaviour, IBulletUpgrade
    {
        private UpgradeSO upgrade;
        private bool isCounter;

        public void Setup(UpgradeSO upgrade, bool isCounter)
        {
            this.upgrade = upgrade;
            this.isCounter = isCounter;
        }

        public void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy)
        {
            if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Poison);

            if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
            {
                float poisonDuration = upgrade.GetUpgradeAmount(isCounter);

                healthSystem.Poison(damageAmount / 2, 1f, poisonDuration);
            }

            if (canDestroy) ObjectPooler.Instance.DestoryWithPool(transform);
        }
    }

    private UpgradeSO upgrade;
    private bool isCounter;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Poison);
    }

    public void OnShoot(Transform bullet)
    {
        if (bullet.GetComponent<PoisonBulletUpgrade>() != null) return;

        PoisonBulletUpgrade upgradeScript = bullet.gameObject.AddComponent<PoisonBulletUpgrade>();

        upgradeScript.Setup(upgrade, isCounter);
    }

    public void SetIsCounter(bool isCounter)
    {
        this.isCounter = isCounter;
    }

    public void OnAdded()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Poison);
    }

    public UpgradeSO GetUpgrade()
    {
        return upgrade;
    }
}
