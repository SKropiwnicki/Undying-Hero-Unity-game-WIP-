using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ExploreLoader : MonoBehaviour
{
    public AudioClip music;

    private OkPanel okPanel;
    private UnityAction okAction; 

    void Awake()
    {
        okPanel = OkPanel.instance();
        okAction = new UnityAction(okFunction);
    }

    void okFunction()
    {
        Debug.Log("111111111111111");
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
            okPanel.make("Ta gra to straszna jest, wiesz?\nKrzywdy wielkie spotkac Cie, a strach i rozpacz ogarna serce Twe!", okAction);
        }

        yield return new WaitForEndOfFrame();

        SoundManager.instance.playMusic(music);
    }
}