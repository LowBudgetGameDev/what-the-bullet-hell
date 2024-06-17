using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private int numBullets = 1;
    [SerializeField] private int shootDamage = 1;
    [SerializeField] private float shootTimerMax = 1f;
    private float originalShootTimerMax;

    private float shootTimer = 0f;
    private int bulletsPerShot;

    private void Awake()
    {
        shootTimer = shootTimerMax;
        originalShootTimerMax = shootTimerMax;
        bulletsPerShot = 1;
    }

    public void DecreaseShootTimeMax(float multiplier)
    {
        shootTimerMax = originalShootTimerMax * multiplier;
        shootTimer = 0f;
    }

    public void SetBulletsPerShot(int amount)
    {
        bulletsPerShot = amount;
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

            foreach (IUpgrade upgrade in GetComponents<IUpgrade>())
            {
                upgrade.OnShoot(bullet);
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
