using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManyBulletsAttack : MonoBehaviour, IBossAttack
{
    private Transform bulletPrefab;
    private new Rigidbody2D rigidbody2D;

    private int numBullets = 20;
    private float shootTimerMax = 0.25f;
    private int shootDamage = 10;

    private float shootTimer;
    private bool isAttacking;

    private void Awake()
    {
        bulletPrefab = Resources.Load<Transform>("BossBullet");
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isAttacking) return;

        shootTimer -= Time.deltaTime;

        if (shootTimer < 0f)
        {
            SoundManager.Instance.PlayRandomSoundOfType(SoundManager.SoundType.Boss_Shoot, 0.75f);
            CinemachineShake.Instance.ShakeCamera(3f, 0.25f);
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
        rigidbody2D.angularVelocity = -90f;
    }

    public void BetterAttack()
    {
        Attack();
    }

    public void StopAttack()
    {
        isAttacking = false;
        rigidbody2D.angularVelocity = 0f;
        rigidbody2D.rotation = 0f;
    }
}
