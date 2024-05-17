using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 50f;
    private int damageAmount;

    private float hideTimer;

    public void SetUp(Vector3 shootDir, int damageAmount)
    {
        this.damageAmount = damageAmount;

        GetComponent<Rigidbody2D>().velocity = shootDir * speed;

        hideTimer = 5f;
    }

    private void Update()
    {
        hideTimer -= Time.deltaTime;

        if (hideTimer < 0f) ObjectPooler.Instance.DestoryWithPool(transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
        }

        ObjectPooler.Instance.DestoryWithPool(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObjectPooler.Instance.DestoryWithPool(transform);
    }
}
