using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class ExploreLoader : MonoBehaviour
{
    public AudioClip music;
    
    private string startText;
    private OkPanel okPanel;
    private UnityAction okAction; 

    void Awake()
    {
        okPanel = OkPanel.instance();
        okAction = new UnityAction(okFunction);
        if(Connector.wasGeneratedMapToExplore)
        {
            startText = Connector.dungeon.startText;
        }
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

        if (Connector.wasGeneratedMapToExplore)
        {
            okPanel.make(InspectorStringAssistant.instance.make(startText), okAction);
        }

        yield return new WaitForEndOfFrame();

        SoundManager.instance.playMusic(music);
    }
}