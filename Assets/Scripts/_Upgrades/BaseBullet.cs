using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : BulletUpgrade
{
    public override void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canShoot)
    {
        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
        }

        ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
