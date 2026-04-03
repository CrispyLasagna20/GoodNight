using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] public class Sound
{
    public AudioClip audio;
    [HideInInspector] public AudioSource source;
    public string name;

    [Range(0f, 1f)] public float volume;
    [Range(0.1f, 2f)] public float pitch;
    public bool loop;
    [Range(0f, 1f)] public float spatialBlend;
    [Range(-1f, 1f)] public float stereoPan;
}
