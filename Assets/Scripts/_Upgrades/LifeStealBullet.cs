using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealBullet : MonoBehaviour, IUpgrade
{
    public class LifeStealBulletUpgrade : MonoBehaviour, IBulletUpgrade
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
            if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Life_Steal);

            if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Damage(damageAmount);

                float randomFloat = Random.Range(0f, 1f);

                if (randomFloat < upgrade.GetUpgradeAmount(isCounter))
                {
                    shooter.GetComponent<HealthSystem>().Heal(damageAmount);
                }
            }

            if (canDestroy) ObjectPooler.Instance.DestoryWithPool(transform);
        }
    }

    private UpgradeSO upgrade;
    private bool isCounter;

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Life_Steal);
    }

    public void OnShoot(Transform bullet)
    {
        if (bullet.GetComponent<LifeStealBulletUpgrade>() != null) return;

        LifeStealBulletUpgrade upgradeScript = bullet.gameObject.AddComponent<LifeStealBulletUpgrade>();

        upgradeScript.Setup(upgrade, isCounter);
    }

    public void SetIsCounter(bool isCounter)
    {
        this.isCounter = isCounter;
    }

    public void OnAdded()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Life_Steal);
    }

    public UpgradeSO GetUpgrade()
    {
        return upgrade;
    }
}
