using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : BulletUpgrade
{
    public override void OnCollided(Collision2D collision, int damageAmount, Transform shooter)
    {
        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
        }

        ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
