using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private int collisionDamage = 1;
    [SerializeField] private int xpGained = 1;

    private new Rigidbody2D rigidbody2D;

    private Transform playerTransform;
    private Vector3 dirToPlayer;

    private Action killAction;

    public void SetUp(Transform playerTransform, Action killAction)
    {
        this.playerTransform = playerTransform;
        this.killAction = killAction;
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        movementSpeed *= Random.Range(0.9f, 1.1f);
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = dirToPlayer * movementSpeed;
    }

    private void Update()
    {
        if (playerTransform == null)
        {
            dirToPlayer = Vector3.zero;
            return;
        }

        dirToPlayer = (playerTransform.position - transform.position).normalized;

        transform.up = dirToPlayer;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent(out PlayerController player))
        {
            player.GetComponent<HealthSystem>().Damage(collisionDamage);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy Despawner"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (GetComponent<HealthSystem>().GetHealth() == 0) LevelManager.Instance.GainXp(xpGained);

        killAction();
    }
}
