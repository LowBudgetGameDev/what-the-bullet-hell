using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;

    private Transform shooter;

    private float speed = 50f;
    private float poisonInterval;
    private int poisonDamage = 40;
    private float poisonTime = 3f;

    private float hideTimer;

    public void SetUp(Vector3 shootDir, float poisonInterval, Transform shooter)
    {
        trail.Clear();

        this.poisonInterval = poisonInterval;
        this.shooter = shooter;

        GetComponent<Rigidbody2D>().velocity = shootDir * speed;

        hideTimer = 5f;

        trail.widthMultiplier = transform.localScale.x;
    }

    private void Start()
    {
        LevelManager.Instance.OnLevelUp += Bullet_OnLevelUp;
    }

    private void Bullet_OnLevelUp(object sender, System.EventArgs e)
    {
        ObjectPooler.Instance.DestoryWithPool(transform);
    }

    private void Update()
    {
        if (shooter == null) ObjectPooler.Instance.DestoryWithPool(transform);

        hideTimer -= Time.deltaTime;

        if (hideTimer < 0f) ObjectPooler.Instance.DestoryWithPool(transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Poison(poisonDamage, poisonInterval, poisonTime);
        }
    }
}
