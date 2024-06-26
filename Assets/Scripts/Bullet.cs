using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;

    private IBulletUpgrade[] upgradeList;
    private Transform shooter;

    private float speed = 30f;
    private int damageAmount;
    private int damageMultiplier;

    private float hideTimer;

    public void SetUp(Vector3 shootDir, int damageAmount, Transform shooter)
    {
        trail.Clear();

        this.damageAmount = damageAmount;
        damageMultiplier = (int) (transform.localScale.x / 0.5f);
        damageMultiplier = Mathf.Clamp(damageMultiplier, 1, 99);
        this.shooter = shooter;

        GetComponent<Rigidbody2D>().velocity = shootDir * speed;

        upgradeList = GetComponents<IBulletUpgrade>();

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
        if (upgradeList.Length == 0)
        {
            if (collision.transform.TryGetComponent(out HealthSystem healthSystem))
            {
                healthSystem.Damage(damageAmount * damageMultiplier);
            }

            ObjectPooler.Instance.DestoryWithPool(transform);
        }

        bool hasPiercing = TryGetComponent(out PiercingBullet.PiercingBulletUpgrade piercing);

        foreach (IBulletUpgrade upgrade in upgradeList)
        {
            upgrade.OnCollided(collision, damageAmount * damageMultiplier, shooter, !hasPiercing);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet Despawner"))
        {
            ObjectPooler.Instance.DestoryWithPool(transform);
        }
    }
}
