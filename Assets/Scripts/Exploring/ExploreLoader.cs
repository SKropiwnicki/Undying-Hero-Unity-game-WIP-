using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ExploreLoader : MonoBehaviour
{
    public AudioClip music;

    public string startingText;
    private OkPanel okPanel;
    private UnityAction okAction; 

    void Awake()
    {
        okPanel = OkPanel.instance();
        okAction = new UnityAction(okFunction);
    }

    void okFunction()
    {
    }

    void Start()
    {
        StartCoroutine("start");
    }

    private IEnumerator start()
    {
        yield return new WaitForEndOfFrame();

        if (!BattleToExplore.wasGenerated)
        {
            okPanel.make(startingText, okAction);
        }

        yield return new WaitForEndOfFrame();

        SoundManager.instance.playMusic(music);
    }
}