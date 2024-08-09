using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private Transform explosionParticles;
    [SerializeField] private GameObject explosionIndicator;

    private new Rigidbody2D rigidbody2D;

    private Transform shooter;

    private float decelerationAmount;
    private int maxDamageAmount;
    private float explosionRadius = 36f;
    private Vector3 explosionPos;

    private float explosionTimer;
    private bool reachedPos;

    public void SetUp(Vector3 explosionPos, int maxDamageAmount, Transform shooter)
    {
        trail.Clear();

        rigidbody2D = GetComponent<Rigidbody2D>();

        this.maxDamageAmount = maxDamageAmount;
        this.shooter = shooter;
        this.explosionPos = explosionPos;

        Vector2 shootDir = (explosionPos - transform.position).normalized;

        float initSpeed = Vector3.Distance(explosionPos, transform.position) * 3;
        decelerationAmount = 2 * (initSpeed - Vector3.Distance(explosionPos, transform.position));

        rigidbody2D.velocity = shootDir * initSpeed;

        explosionTimer = 1f;

        trail.widthMultiplier = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if (reachedPos) return;

        rigidbody2D.velocity =
            rigidbody2D.velocity.normalized *
            (rigidbody2D.velocity.magnitude - decelerationAmount * Time.fixedDeltaTime);

        if (Vector3.Distance(rigidbody2D.position, explosionPos) <= 0.25f)
        {
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.position = explosionPos;
            reachedPos = true;
            explosionIndicator.SetActive(true);
            SoundManager.Instance.PlayRandomSoundOfType(SoundManager.SoundType.Bomb_Tick);
        }
    }

    private void Update()
    {
        if (shooter == null) ObjectPooler.Instance.DestoryWithPool(transform);

        if (reachedPos) explosionTimer -= Time.deltaTime;

        if (explosionTimer < 0f)
        {
            SoundManager.Instance.PlayRandomSoundOfType(SoundManager.SoundType.Big_Boom, 0.8f);
            CinemachineShake.Instance.ShakeCamera(15f, 0.5f);
            Explode();
        }
    }

    private void Explode()
    {
        Transform particles = Instantiate(explosionParticles, transform.position, Quaternion.identity);
        particles.localScale = Vector3.one * 3f;

        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.TryGetComponent(out HealthSystem health))
            {
                int damageChangeInterval = Mathf.FloorToInt(explosionRadius / maxDamageAmount);

                int damageAmount = maxDamageAmount - 
                    Mathf.FloorToInt(Vector3.Distance(transform.position, collider2D.transform.position) / damageChangeInterval);

                health.Damage(damageAmount);
            }
        }

        ObjectPooler.Instance.DestoryWithPool(transform);
    }

    private void OnDisable()
    {
        reachedPos = false;
        explosionIndicator.SetActive(false);
    }
}
