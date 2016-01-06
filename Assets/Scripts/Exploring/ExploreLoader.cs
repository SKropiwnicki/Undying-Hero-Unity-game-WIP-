using UnityEngine;
using System.Collections;

public class ExploreLoader : MonoBehaviour
{
    public AudioClip music;

    void Start()
    {
        StartCoroutine("start");
    }

    private IEnumerator start()
    {
        yield return new WaitForEndOfFrame();

        SoundManager.instance.playMusic(music);
    }
}