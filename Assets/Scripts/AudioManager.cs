using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        foreach (Sound curSound in sounds)
        {
            curSound.source = gameObject.AddComponent<AudioSource>();
            curSound.source.clip = curSound.audio;
            curSound.source.volume = curSound.volume;
            curSound.source.pitch = curSound.pitch;
            curSound.source.loop = curSound.loop;
            curSound.source.spatialBlend = curSound.spatialBlend;
            curSound.source.panStereo = curSound.stereoPan;
        }
    }
    
    public void PlaySound(string name)
    {
        foreach (Sound curSound in sounds)
        {
            if (curSound.name == name)
            {
                curSound.source.Play();
            }
        }
    }
    public void PlaySoundInSpace(string name, float balance)
    {
        foreach (Sound curSound in sounds)
        {
            if (curSound.name == name)
            {
                curSound.source.panStereo = balance;
                curSound.source.Play();
            }
        }
    }

    public void UpdateVolume(string name, float volume)
    {
        foreach (Sound curSound in sounds)
        {
            if (curSound.name == name)
            {
                curSound.source.volume = volume;
            }
        }
    }
}
