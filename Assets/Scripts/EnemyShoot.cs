using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private int numBullets = 1;
    [SerializeField] private int shootDamage = 1;
    [SerializeField] private float shootTimerMax = 1f;

    private float shootTimer = 0f;

    private void Awake()
    {
        shootTimer = shootTimerMax;
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
        Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        float pointAngleDegrees = UtilsClass.VectorToAngleDegrees(transform.up);

        if (pointAngleDegrees < 0f) pointAngleDegrees += 360f;

        bullet.GetComponent<Bullet>().SetUp(UtilsClass.AngleDegreesToVector(pointAngleDegrees + angleDegrees), shootDamage);
    }
}
