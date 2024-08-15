using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip bossTheme;

    private AudioSource audioSource;
    private AudioLowPassFilter lowPassFilter;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();

        audioSource.clip = mainTheme;
        audioSource.volume = PlayerPrefs.GetFloat("musicVolume", 1f);

        audioSource.Play();
    }

    private void Start()
    {
        GameManager.Instance.OnBossBattleBegin += MusicManager_OnBossBattleBegin;
        GameManager.Instance.OnMaxLevelReached += MusicManager_OnMaxLevelReached;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            lowPassFilter.cutoffFrequency = 5000;
        }
        else
        {
            lowPassFilter.cutoffFrequency = 22000;
        }
    }

    private void MusicManager_OnMaxLevelReached(object sender, System.EventArgs e)
    {
        audioSource.Stop();
    }

    private void MusicManager_OnBossBattleBegin(object sender, System.EventArgs e)
    {
        audioSource.Stop();

        audioSource.clip = bossTheme;

        audioSource.Play();
    }

    public float GetVolume()
    {
        return audioSource.volume;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;

        PlayerPrefs.SetFloat("musicVolume", volume);
    }
}
