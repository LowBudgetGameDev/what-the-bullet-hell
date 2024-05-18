using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;
    [SerializeField] private int collisionDamage = 1;

    private new Rigidbody2D rigidbody2D;

    private Transform playerTransform;
    private Vector3 dirToPlayer;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

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

        if (collision.gameObject.layer == 1 << 10)
        {
            Destroy(gameObject);
        }
    }
}
