using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour, IUpgrade
{
    public class ExplosiveBulletUpgrade : MonoBehaviour, IBulletUpgrade
    {
        private Transform explosionParticles;
        private UpgradeSO upgrade;
        private bool isCounter;

        public void Setup(Transform explosionParticles, UpgradeSO upgrade, bool isCounter)
        {
            this.explosionParticles = explosionParticles;
            this.upgrade = upgrade;
            this.isCounter = isCounter;
        }

        public void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canDestroy)
        {
            if (upgrade == null) upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Explosive_Bullets);

            Transform particles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
            particles.localScale *= 0.25f * upgrade.GetLevel(isCounter);

            float explosionRadius = upgrade.GetUpgradeAmount(isCounter);

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

    private Transform explosionParticles;
    private UpgradeSO upgrade;
    private bool isCounter;

    private void Awake()
    {
        explosionParticles = Resources.Load<Transform>("ExplosionParticles");
    }

    private void Start()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Explosive_Bullets);
    }

    public void OnShoot(Transform bullet)
    {
        if (bullet.GetComponent<ExplosiveBulletUpgrade>() != null) return;

        ExplosiveBulletUpgrade upgradeScript = bullet.gameObject.AddComponent<ExplosiveBulletUpgrade>();

        upgradeScript.Setup(explosionParticles, upgrade, isCounter);
    }

    public void SetIsCounter(bool isCounter)
    {
        this.isCounter = isCounter;
    }

    public void OnAdded()
    {
        upgrade = UpgradeManager.Instance.GetUpgradeSO(Upgrade.Explosive_Bullets);
    }
}
