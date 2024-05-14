using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event EventHandler OnShoot;

    private float movementSpeed = 20f;

    private void Update()
    {
        HandleMovement();
        HandleAiming();
        HandleShooting();
    }

    private void HandleMovement()
    {
        Vector3 movementVector = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            movementVector.y += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementVector.y += -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementVector.x += -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x += 1;
        }

        transform.position += movementVector.normalized * movementSpeed * Time.deltaTime;
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
