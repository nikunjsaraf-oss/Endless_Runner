using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip audioClip;
    
    [Range(0, 1)]
    public float volume;

    [Range(0.1f, 3)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
