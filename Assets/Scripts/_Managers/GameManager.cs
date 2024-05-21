using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnGameOver;

    private Transform playerTransform;

    private void Awake()
    {
        Instance = this;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        playerTransform.GetComponent<HealthSystem>().OnDied += (object sender, EventArgs e) =>
        {
            OnGameOver?.Invoke(this, EventArgs.Empty);
        };
    }
}
