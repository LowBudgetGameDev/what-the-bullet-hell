using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private Button muteButton;

    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            GameSceneManager.ChangeScene(GameSceneManager.Scene.MainScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        muteButton.onClick.AddListener(() =>
        {
            MenuMusicManager.Instance.MuteMenuMusic();
        });
    }
}
