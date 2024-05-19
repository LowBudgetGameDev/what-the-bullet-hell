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

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        originalShootTimerMax = shootTimerMax;
    }

    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeLevelUp += (object sender, EventArgs e) =>
        {
            DecreaseShootTimeMax();
        };

        DecreaseShootTimeMax();
    }

    private void DecreaseShootTimeMax()
    {
        if (this == null) return;

        FireRateUpgrade healthUpgrade = GetComponent<FireRateUpgrade>();

        if (healthUpgrade == null) return;

        shootTimerMax = originalShootTimerMax * healthUpgrade.GetFireRateMultiplier();
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
        if (Input.GetMouseButtonUp(0)) shootTimer = 0f;

        if (!Input.GetMouseButton(0)) return;

        shootTimer -= Time.deltaTime;

        if (shootTimer < 0f)
        {
            OnShoot?.Invoke(this, EventArgs.Empty);
            shootTimer += shootTimerMax;
        }
    }
}
