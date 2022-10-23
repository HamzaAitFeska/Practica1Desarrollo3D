using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class SoundEffects 
{
    // Start is called before the first frame update
    public string  name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public AudioMixerGroup group;
    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
