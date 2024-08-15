using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        musicVolumeSlider.value = MusicManager.Instance.GetVolume();

        musicVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            MusicManager.Instance.SetVolume(value);
        });

        soundVolumeSlider.value = SoundManager.Instance.GetVolume();

        soundVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            SoundManager.Instance.SetVolume(value);
        });

        pauseUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (Time.timeScale == 1f || pauseUI.activeSelf))
        {
            if (pauseUI.activeSelf)
            {
                animator.SetTrigger("Close");

                FunctionTimer.Create(() =>
                {
                    pauseUI.SetActive(false);
                    Time.timeScale = 1f;
                }, 0.75f);
            }
            else
            {
                pauseUI.SetActive(true);
                animator.SetTrigger("Open");
                Time.timeScale = 0f;
            }
        }
    }
}
