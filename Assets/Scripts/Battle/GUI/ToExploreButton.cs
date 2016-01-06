using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class ToExploreButton : MonoBehaviour, IPointerClickHandler
{
    public AudioClip okSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.instance.playOnGui(okSound);
        ExploreToBattle.instance.destroy();
        EndBattlePanel.instance.destroy();
        SceneManager.LoadScene("Exploring");
    }
}
