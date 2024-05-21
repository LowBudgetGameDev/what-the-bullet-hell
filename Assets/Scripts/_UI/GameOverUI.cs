using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameSceneManager.ChangeScene(GameSceneManager.Scene.MainMenu);
        });

        GameManager.Instance.OnGameOver += (object sender, EventArgs e) =>
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        };

        gameObject.SetActive(false);
    }
}
