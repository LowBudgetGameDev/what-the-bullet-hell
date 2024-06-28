using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnGameOver;
    public event EventHandler OnBossBattleStart;

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

    private void Start()
    {
        UpgradeManager.Instance.OnUpgradeLevelUp += GameManager_OnUpgradeLevelUp;
    }

    private void GameManager_OnUpgradeLevelUp(object sender, EventArgs e)
    {
        if (LevelManager.Instance.GetLevel() == 15)
        {
            OnBossBattleStart?.Invoke(this, EventArgs.Empty);
        }
    }
}
