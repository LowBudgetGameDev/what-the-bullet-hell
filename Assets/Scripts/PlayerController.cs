using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event EventHandler OnShoot;

    private new Rigidbody2D rigidbody2D;

    private float movementSpeed = 20f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rigidbody2D.velocity = movementVector.normalized * movementSpeed;
    }

    private void HandleAiming()
    {
        Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();

        Vector3 dirToMouse = (mouseWorldPosition - transform.position).normalized;

        transform.up = dirToMouse;
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnShoot?.Invoke(this, EventArgs.Empty);
        }
    }
}
