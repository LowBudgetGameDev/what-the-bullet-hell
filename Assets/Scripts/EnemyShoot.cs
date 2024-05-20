using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private int numBullets = 1;
    [SerializeField] private int shootDamage = 1;
    [SerializeField] private float shootTimerMax = 1f;
    private float originalShootTimerMax;

    private List<UpgradeSO> specialBulletUpgradeList;

    private float shootTimer = 0f;
    private float sizeMultiplier;
    private int bulletsPerShot;

    private void Awake()
    {
        shootTimer = shootTimerMax;
        originalShootTimerMax = shootTimerMax;
    }

    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeUnlocked += EnemyShoot_OnUpgradeUnlocked;
        UpgradeManager.Instance.OnUpgradeLevelUp += EnemyShoot_OnUpgradeLevelUp;
    }

    private void DecreaseShootTimeMax()
    {
        if (this == null) return;

        FireRateUpgrade fireRateUpgrade = GetComponent<FireRateUpgrade>();

        if (fireRateUpgrade == null) return;

        shootTimerMax = originalShootTimerMax * fireRateUpgrade.GetFireRateMultiplier();
        shootTimer = 0f;
    }

    private void EnemyShoot_OnUpgradeLevelUp(object sender, EventArgs e)
    {
        if (this == null) return;

        LargeBullet largeBulletUpgrade = GetComponent<LargeBullet>();

        if (largeBulletUpgrade != null) sizeMultiplier = largeBulletUpgrade.GetBulletScaledSize();

        ShotgunUpgrade shotgunUpgrade = GetComponent<ShotgunUpgrade>();

        if (shotgunUpgrade != null) bulletsPerShot = shotgunUpgrade.GetBulletAmount();

        DecreaseShootTimeMax();
    }

    private void EnemyShoot_OnUpgradeUnlocked(object sender, UpgradeManager.UpgradeUnlockedEventArgs e)
    {
        if (e.upgrade.counter == UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Health) ||
            e.upgrade.counter == UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Fire_Rate) ||
            e.upgrade.counter == UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Large_Bullets) ||
            e.upgrade.counter == UpgradeManager.Instance.GetUpgradeSO(UpgradeManager.Upgrade.Shotgun)) return;

        specialBulletUpgradeList.Add(e.upgrade.counter);
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer < 0f)
        {
            for (int i = 0; i < numBullets; i++) Shoot(360f / numBullets * i);

            shootTimer += shootTimerMax;
        }
    }

    private void Shoot(float angleDegrees)
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {
            Transform bullet = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, transform.position, Quaternion.identity);

            bullet.localScale = new Vector3(sizeMultiplier / 2 + 0.5f, sizeMultiplier / 2 + 0.5f, sizeMultiplier / 2 + 0.5f);

            foreach (UpgradeSO upgrade in specialBulletUpgradeList)
            {
                MonoScript upgradeScript = (MonoScript)upgrade.script;

                if (bullet.gameObject.GetComponent(upgradeScript.GetClass()) != null) continue;

                bullet.gameObject.AddComponent(upgradeScript.GetClass());
            }

            float pointAngleDegrees = UtilsClass.VectorToAngleDegrees(transform.up);

            if (pointAngleDegrees < 0f) pointAngleDegrees += 360f;

            if (bulletsPerShot == 1)
            {
                bullet.GetComponent<Bullet>().SetUp(UtilsClass.AngleDegreesToVector(pointAngleDegrees + angleDegrees), shootDamage, transform);
                return;
            }

            bullet.GetComponent<Bullet>().SetUp(UtilsClass.AngleDegreesToVector(pointAngleDegrees + i * 120 / (bulletsPerShot - 1) - 60 + angleDegrees), shootDamage, transform);
        }
    }
}
