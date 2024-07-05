using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeAttack : MonoBehaviour, IBossAttack
{
    private Transform spikes;
    private Transform spikesInstance;

    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        spikes = Resources.Load<Transform>("Spikes");
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Attack()
    {
        spikesInstance = Instantiate(spikes, transform);

        PhysicsMaterial2D material = new PhysicsMaterial2D();
        material.bounciness = 1;
        material.friction = 0;
        rigidbody2D.sharedMaterial = material;

        float moveSpeed = 25f;
        rigidbody2D.velocity = UtilsClass.AngleDegreesToVector(Random.Range(0, 360)) * moveSpeed;
        rigidbody2D.angularVelocity = 120f;
    }

    public void StopAttack()
    {
        Destroy(spikesInstance.gameObject);

        rigidbody2D.sharedMaterial = null;

        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.angularVelocity = 0f;
        rigidbody2D.rotation = 0f;
    }
}
