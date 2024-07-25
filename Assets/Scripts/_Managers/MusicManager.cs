using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainTheme;
    [SerializeField] private AudioClip bossTheme;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.PlayOneShot(mainTheme);
    }

    private void Start()
    {
        GameManager.Instance.OnBossBattleBegin += MusicManager_OnBossBattleBegin;
    }

    private void MusicManager_OnBossBattleBegin(object sender, System.EventArgs e)
    {
        audioSource.Stop();

        audioSource.PlayOneShot(bossTheme);
    }
}
