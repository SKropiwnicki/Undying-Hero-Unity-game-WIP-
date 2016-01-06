using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource efxSource;
    public AudioSource musicSource;
    public AudioSource onDeathSource;
    public AudioSource onGuiSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        /*
        if (FindObjectsOfType(GetType()).Length > 1)
         {
             Destroy(gameObject);
         }
         */
    }

    public void playSingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void playOnGui(AudioClip clip)
    {
        onGuiSource.clip = clip;
        onGuiSource.Play();
    }

    public void playOnDeath(AudioClip clip)
    {
        onDeathSource.clip = clip;
        onDeathSource.Play();
    }

    public void playMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
}
