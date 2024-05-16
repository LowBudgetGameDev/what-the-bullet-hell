using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 50f;
    private int damageAmount;

    public void SetUp(Vector3 shootDir, int damageAmount)
    {
        this.damageAmount = damageAmount;

        GetComponent<Rigidbody2D>().velocity = shootDir * speed;

        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
        }

        Destroy(gameObject);
    }
}
