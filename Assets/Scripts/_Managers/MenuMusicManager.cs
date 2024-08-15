using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    public static MenuMusicManager Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        audioSource.volume = PlayerPrefs.GetFloat("musicVolume", 1f);
    }

    public void MuteMenuMusic()
    {
        audioSource.volume = audioSource.volume == 0f ? PlayerPrefs.GetFloat("musicVolume", 1f) : 0f;
    }
}
