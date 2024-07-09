using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeBulletAttack : MonoBehaviour, IBossAttack
{
    private Transform bulletPrefab;
    private float shootTimerMax = 5f;
    private int shootDamage = 1;

    private float shootTimer;
    private bool isAttacking;

    private void Awake()
    {
        bulletPrefab = Resources.Load<Transform>("HugeBullet");
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
        Transform bullet1 = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, new Vector3(-64f, 20f, 0f), Quaternion.identity);
        bullet1.GetComponent<Bullet>().SetUp(Vector3.right, shootDamage, transform);

        Transform bullet2 = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, new Vector3(64f, 0f, 0f), Quaternion.identity);
        bullet2.GetComponent<Bullet>().SetUp(Vector3.left, shootDamage, transform);

        Transform bullet3 = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, new Vector3(-64f, -20f, 0f), Quaternion.identity);
        bullet3.GetComponent<Bullet>().SetUp(Vector3.right, shootDamage, transform);
    }

    public void Attack()
    {
        isAttacking = true;
        transform.position = new Vector3(0f, 250f, 0f);
    }

    public void StopAttack()
    {
        isAttacking = false;
        transform.position = Vector3.zero;
    }
}
