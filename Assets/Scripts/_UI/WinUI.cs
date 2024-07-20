using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            GameSceneManager.ChangeScene(GameSceneManager.Scene.MainMenu);
        });

        GameManager.Instance.OnWin += (object sender, EventArgs e) =>
        {
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        };

        gameObject.SetActive(false);
    }
}
