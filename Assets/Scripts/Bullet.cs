using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 50f;

    private Vector3 shootDir;

    public void SetUp(Vector3 shootDir)
    {
        this.shootDir = shootDir;

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += shootDir * speed * Time.deltaTime;
    }
}
