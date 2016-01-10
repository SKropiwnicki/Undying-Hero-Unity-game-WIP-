using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class OnDungClick : MonoBehaviour, IPointerClickHandler
{
    public static DungeonStats dungeon;

    public void OnPointerClick(PointerEventData eventData)
    {
        dungeon = GetComponent<DungeonStats>();
        Connector.instance.beforeExploreFromMap();
        SceneManager.LoadScene("Exploring");
    }
}
