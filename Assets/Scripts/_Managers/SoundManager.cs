using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        Shoot_1,
        Shoot_2,
        Shoot_3,
        Shoot_4,
        Shoot_5
    }

    public enum SoundType
    {
        Shoot
    }

    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;

    private Dictionary<Sound, AudioClip> soundAudioClipDictionary;
    private Dictionary<SoundType, List<Sound>> soundTypeDictionary;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        soundAudioClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in Enum.GetValues(typeof(Sound)))
        {
            soundAudioClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }

        soundTypeDictionary = new Dictionary<SoundType, List<Sound>>();

        foreach (SoundType type in Enum.GetValues(typeof(SoundType)))
        {
            soundTypeDictionary[type] = new List<Sound>();

            foreach (Sound sound in Enum.GetValues(typeof(Sound)))
            {
                if (sound.ToString().Contains(type.ToString()))
                {
                    soundTypeDictionary[type].Add(sound);
                }
            }
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundAudioClipDictionary[sound]);
    }

    public void PlayRandomSoundOfType(SoundType type)
    {
        PlaySound(soundTypeDictionary[type][Random.Range(0, soundTypeDictionary[type].Count)]);
    }
}
