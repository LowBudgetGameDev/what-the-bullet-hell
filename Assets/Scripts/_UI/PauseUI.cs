using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    private void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            MusicManager.Instance.SetVolume(value);
        });

        soundVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            SoundManager.Instance.SetVolume(value);
        });

        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            Time.timeScale = pauseUI.activeSelf ? 0f : 1f;
        }
    }
}
