using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAttack : MonoBehaviour, IBossAttack
{
    private Transform bulletPrefab;
    private float shootTimerMax = 2f;
    private int explosionDamage = 6;

    private float shootTimer;
    private bool isAttacking;

    private void Awake()
    {
        bulletPrefab = Resources.Load<Transform>("BossBomb");
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
        Vector2 maxBombPos = new Vector2(30, 20);
        Vector2 minBombPos = new Vector2(-30, -20);

        Vector2 bombExplosionPos = new Vector2(Random.Range(minBombPos.x, maxBombPos.x), Random.Range(minBombPos.y, maxBombPos.y));

        Vector3 randomPosOutsideCamera = new Vector3(Random.Range(-30f*16/9, 30f*16/9), Random.Range(0, 1) == 0 ? 22.5f : -22.5f);

        Transform bomb = ObjectPooler.Instance.InstantiateWithPool(bulletPrefab, randomPosOutsideCamera, Quaternion.identity);
        bomb.GetComponent<BombBullet>().SetUp(bombExplosionPos, explosionDamage, transform);
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
