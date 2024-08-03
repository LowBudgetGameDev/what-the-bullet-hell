using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public enum Sound
    {
        Big_Boom_1,
        Big_Boom_2,
        Big_Boom_3,
        Bomb_Tick_1,
        Bomb_Tick_2,
        Bomb_Tick_3,
        Boom_1,
        Boom_2,
        Boom_3,
        Boom_4,
        Boom_5,
        Boss_Bounce_1,
        Boss_Bounce_2,
        Boss_Heal,
        Boss_Lazer,
        Boss_Shoot_1,
        Boss_Shoot_2,
        Boss_Shoot_3,
        Death_1,
        Death_2,
        Death_3,
        Death_4,
        Death_5,
        Hit_1,
        Hit_2,
        Hit_3,
        Hit_4,
        Hit_5,
        Large_Boss_Shoot,
        Poison_Shoot_1,
        Poison_Shoot_2,
        Poison_Shoot_3,
        Shoot_1,
        Shoot_2,
        Shoot_3,
        Shoot_4,
        Shoot_5,
        Unlock_1,
        Unlock_2,
        Unlock_3,
        Unlock_4,
        Unlock_5,
        Upgrade_1,
        Upgrade_2,
        Upgrade_3,
        Upgrade_4,
        Upgrade_5
    }

    public enum SoundType
    {
        Big_Boom,
        Bomb_Tick,
        Boom,
        Boss_Bounce,
        Boss_Shoot,
        Death,
        Hit,
        Poison_Shoot,
        Shoot,
        Unlock,
        Upgrade
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
                if (sound.ToString().Contains(type.ToString()) && sound.ToString()[0] == type.ToString()[0])
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
