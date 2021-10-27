using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioSource GameAudioSource;
    public AudioClip LoseSound, EatSound;
    private void Awake() 
    {
        Instance = this;
    }
    public void PlaySound(string soundEventName)
    {
        switch (soundEventName)
        {
            case "Die":
                PlaySoundByClip(LoseSound);
                break;
            case "Eat":
                PlaySoundByClip(EatSound);
                break;
        }
    }
    private void PlaySoundByClip(AudioClip clip)
    {
        GameAudioSource.Stop();
        GameAudioSource.clip = clip;
        GameAudioSource.Play();
    }
}
