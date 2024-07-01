using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event EventHandler OnShoot;

    private new Rigidbody2D rigidbody2D;

    private float movementSpeed = 20f;
    private float shootTimerMax = 0.25f;
    private float originalShootTimerMax;

    private float shootTimer;

    private bool isPushedBack;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        originalShootTimerMax = shootTimerMax;
    }

    public void DecreaseShootTimeMax(float multiplier)
    {
        shootTimerMax = originalShootTimerMax * multiplier;
        shootTimer = 0f;
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (isPushedBack) return;

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
        if (shootTimer >= 0f) shootTimer -= Time.deltaTime;

        if (shootTimer < 0f && Input.GetMouseButton(0))
        {
            OnShoot?.Invoke(this, EventArgs.Empty);
            shootTimer += shootTimerMax;
        }
    }

    public void ApplyForce(Vector2 force, ForceMode2D forceMode2D, float duration)
    {
        isPushedBack = true;
        rigidbody2D.AddForce(force, forceMode2D);

        FunctionTimer.CreateFunctionTimer(() => { isPushedBack = false; }, duration);
    }
}
