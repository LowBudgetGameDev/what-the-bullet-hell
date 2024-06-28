using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnGameOver;
    public event EventHandler OnMaxLevelReached;
    public event EventHandler OnBossBattleBegin;

    [SerializeField] private GameObject mainVirtualCamera;
    [SerializeField] private GameObject bossVirtualCamera;
    [SerializeField] private Transform bulletBorder;

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
            OnMaxLevelReached?.Invoke(this, EventArgs.Empty);

            FunctionTimer.CreateFunctionTimer(ChangeToBossCamera, 3f);
        }
    }

    private void ChangeToBossCamera()
    {
        playerTransform.position = new Vector3(0f, -25f, 0f);

        bossVirtualCamera.SetActive(true);
        mainVirtualCamera.SetActive(false);
        bulletBorder.localScale *= 1.2f;

        FunctionTimer.CreateFunctionTimer(() =>
        {
            OnBossBattleBegin?.Invoke(this, EventArgs.Empty);
        }, 2f);
    }
}
