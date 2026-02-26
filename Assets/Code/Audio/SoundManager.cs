using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {get; private set; }
    private AudioSource source;
    private AudioSource musicSource;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        
        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }

    public void ChangeSoundVolume(float volume)
    {
        ChangeSourceVolume(1, volume, source, "source");
    }

    public void ChangeMusicVolume(float volume)
    {
        ChangeSourceVolume(0.3f, volume, musicSource, "musicSource");
    }
    
    public void ChangeSourceVolume(float baseVolume, float change, AudioSource targetSource, string volumeName)
    {
        float currentVolume  = PlayerPrefs.GetFloat(volumeName, 1);
        
        currentVolume += change;
        
        if (currentVolume > 1)
        {
            currentVolume = 0;
        }
        else if (currentVolume < 0)
        {
            currentVolume = 1;
        }
        
        float finalVolume = currentVolume * baseVolume;
        
        targetSource.volume = finalVolume;
        
        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
