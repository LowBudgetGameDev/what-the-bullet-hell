using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    private Transform playerTransform;
    private new Rigidbody2D rigidbody2D;

    private float homingTimer;
    private float homingSpeed = 30f;

    private bool isHoming;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        homingTimer = 2f;
        isHoming = true;
    }

    private void FixedUpdate()
    {
        if (!isHoming) return;

        Vector2 dirToPlayer = (playerTransform.position - transform.position).normalized;

        rigidbody2D.velocity = dirToPlayer * homingSpeed;
    }

    private void Update()
    {
        if (!isHoming) return;

        homingTimer -= Time.deltaTime;

        if (homingTimer < 0f)
        {
            isHoming = false;
        }
    }
}
