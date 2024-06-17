using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour, IBulletUpgrade
{
    public void OnCollided(Collider2D collision, int damageAmount, Transform shooter, bool canShoot)
    {
        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
        }

        ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
