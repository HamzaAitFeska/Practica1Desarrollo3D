using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioMixer ambient, effects;
    public AudioSource weaponShoot, weaponReload, weaponEmpty, itemHealth, itemAmmo, itemShield, doorAutomaticOpening, doorAutomaticClosing, doorLockedOpening, trapActive, footstepDirt1, footstepDirt2, footstepDirt3, footstepDirt4, footstepMetal1, footstepMetal2, footstepMetal3, footstepMetal4, uiWarning, uiClick, KeyPickup, playerHurt, playerDeath;

    public static AudioController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void Play(AudioSource audio)
    {
        audio.Play();
    }

    public void PlayOneShot(AudioSource audio)
    {
        audio.PlayOneShot(audio.clip);
    }

    public void Stop(AudioSource audio)
    {
        audio.Stop();
        
    }

    public void Pause(AudioSource audio)
    {
        audio.Pause();
    }



}
