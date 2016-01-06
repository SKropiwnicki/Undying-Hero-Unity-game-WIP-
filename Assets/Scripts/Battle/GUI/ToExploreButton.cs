using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class ToExploreButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExploreToBattle.instance.destroy();
        EndBattlePanel.instance.destroy();
        SceneManager.LoadScene("Exploring");
    }
}
