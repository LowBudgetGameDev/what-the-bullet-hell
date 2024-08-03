using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileAttack : MonoBehaviour, IBossAttack
{
    private Transform bulletPrefab;

    private int numBullets = 10;
    private float shootTimerMax = 3f;
    private int shootDamage = 50;

    private float shootTimer;
    private bool isAttacking;

    private void Awake()
    {
        bulletPrefab = Resources.Load<Transform>("HomingBullet");
    }

    private void Update()
    {
        if (!isAttacking) return;

        shootTimer -= Time.deltaTime;

        if (shootTimer < 0f)
        {
            SoundManager.Instance.PlayRandomSoundOfType(SoundManager.SoundType.Boss_Shoot, 0.9f);
            Shoot();
            shootTimer += shootTimerMax;
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < numBullets; i++)
        {
            Transform bullet = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, transform.position, Quaternion.identity);

            float angleDegrees = 360f / numBullets * i;
            float pointAngleDegrees = UtilsClass.VectorToAngleDegrees(transform.up);

            if (pointAngleDegrees < 0f) pointAngleDegrees += 360f;

            bullet.GetComponent<Bullet>().SetUp(UtilsClass.AngleDegreesToVector(pointAngleDegrees + angleDegrees), shootDamage, transform);
        }
    }

    public void Attack()
    {
        isAttacking = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
    }
}
