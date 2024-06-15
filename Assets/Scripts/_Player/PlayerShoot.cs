using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    private List<UpgradeSO> specialBulletUpgradeList;

    private int damageAmount = 1;
    private float sizeMultiplier;
    private int bulletsPerShot;

    private void Awake()
    {
        specialBulletUpgradeList = new List<UpgradeSO>();
        bulletsPerShot = 1;
    }

    private void Start()
    {
        GetComponent<PlayerController>().OnShoot += PlayerShoot_OnShoot;
        UpgradeManager.Instance.OnUpgradeUnlocked += PlayerShoot_OnUpgradeUnlocked;
        UpgradeManager.Instance.OnUpgradeLevelUp += PlayerShoot_OnUpgradeLevelUp;
    }

    private void PlayerShoot_OnUpgradeLevelUp(object sender, System.EventArgs e)
    {
        if (this == null) return;

        LargeBullet largeBulletUpgrade = GetComponent<LargeBullet>();

        if (largeBulletUpgrade != null) sizeMultiplier = largeBulletUpgrade.GetBulletScaledSize();

        ShotgunUpgrade shotgunUpgrade = GetComponent<ShotgunUpgrade>();
        
        if (shotgunUpgrade != null) bulletsPerShot = shotgunUpgrade.GetBulletAmount();
    }

    private void PlayerShoot_OnUpgradeUnlocked(object sender, UpgradeManager.UpgradeUnlockedEventArgs e)
    {
        if (e.upgrade == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Health) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Fire_Rate) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Large_Bullets) ||
            e.upgrade == UpgradeManager.Instance.GetUpgradeSO(Upgrade.Shotgun)) return;

        specialBulletUpgradeList.Add(e.upgrade);
    }

    private void PlayerShoot_OnShoot(object sender, System.EventArgs e)
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Transform bullet = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, transform.position, Quaternion.identity);

            bullet.localScale = new Vector3(sizeMultiplier / 2 + 0.5f, sizeMultiplier / 2 + 0.5f, sizeMultiplier / 2 + 0.5f);

            foreach (UpgradeSO upgrade in specialBulletUpgradeList)
            {
                if (bullet.gameObject.GetComponent(upgrade.GetScriptType()) != null) continue;

                bullet.gameObject.AddComponent(upgrade.GetScriptType());
            }

            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

            Vector3 dirToMouse = (mouseWorldPosition - transform.position).normalized;

            if (bulletsPerShot == 1)
            {
                bullet.GetComponent<Bullet>().SetUp(dirToMouse, damageAmount, transform);
                return;
            }

            float shootAngle = UtilsClass.VectorToAngleDegrees(dirToMouse);

            bullet.GetComponent<Bullet>().SetUp(UtilsClass.AngleDegreesToVector(shootAngle + i * 120 / (bulletsPerShot - 1) - 60), damageAmount, transform);
        }
    }
}
