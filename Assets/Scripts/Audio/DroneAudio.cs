using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAudio : MonoBehaviour
{
    public AudioSource droneIdle, droneAlert, droneShooting, droneDestroyed;

    public static DroneAudio instance;

    private void Awake()
    {
        if (instance == null)
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
