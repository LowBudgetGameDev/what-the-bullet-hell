using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip bossTheme;

    private AudioSource audioSource;
    private AudioLowPassFilter lowPassFilter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        lowPassFilter = GetComponent<AudioLowPassFilter>();

        audioSource.clip = mainTheme;

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
}
