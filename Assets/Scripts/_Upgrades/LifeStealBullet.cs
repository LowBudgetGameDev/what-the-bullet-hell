using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeStealBullet : BulletUpgrade
{
    public override void OnCollided(Collision2D collision, int damageAmount, Transform shooter)
    {
        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
            shooter.GetComponent<HealthSystem>().Heal(damageAmount);
        }

        ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
