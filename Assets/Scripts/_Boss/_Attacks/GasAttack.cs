using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasAttack : MonoBehaviour, IBossAttack
{
    private Transform bulletPrefab;
    private float shootTimerMax = 3f;
    private int poisonInterval = 1;

    private float shootTimer;
    private bool isAttacking;

    private void Awake()
    {
        bulletPrefab = Resources.Load<Transform>("GasBullet");
    }

    private void Update()
    {
        if (!isAttacking) return;

        shootTimer -= Time.deltaTime;

        if (shootTimer < 0f)
        {
            Shoot();
            shootTimer += shootTimerMax;
        }
    }

    private void Shoot()
    {
        Transform bullet = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<GasBullet>().SetUp(UtilsClass.AngleDegreesToVector(Random.Range(0f, 360f)), poisonInterval, transform);
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
