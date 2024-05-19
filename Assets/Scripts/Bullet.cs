using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletUpgrade[] upgradeList;
    private Transform shooter;

    private float speed = 50f;
    private int damageAmount;

    private float hideTimer;

    public void SetUp(Vector3 shootDir, int damageAmount, Transform shooter)
    {
        this.damageAmount = damageAmount;
        this.shooter = shooter;

        GetComponent<Rigidbody2D>().velocity = shootDir * speed;

        upgradeList = GetComponents<BulletUpgrade>();

        hideTimer = 5f;
    }

    private void Update()
    {
        if (shooter == null) ObjectPooler.Instance.DestoryWithPool(transform);

        hideTimer -= Time.deltaTime;

        if (hideTimer < 0f) ObjectPooler.Instance.DestoryWithPool(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (upgradeList.Length == 0)
        {
            if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Damage(damageAmount);
            }

            ObjectPooler.Instance.DestoryWithPool(transform);
        }

        bool hasPiercing = TryGetComponent(out PiercingBullet piercing);

        foreach (BulletUpgrade upgrade in upgradeList)
        {
            upgrade.OnCollided(collision, damageAmount, shooter, hasPiercing);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet Despawner"))
        {
            ObjectPooler.Instance.DestoryWithPool(transform);
        }
    }
}
