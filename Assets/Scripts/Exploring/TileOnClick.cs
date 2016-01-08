using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class TileOnClick : MonoBehaviour, IPointerClickHandler
{
    public static bool wasClicked;
    public static TileOnClick tile;
    public string direction;

    public void OnPointerClick(PointerEventData eventData)
    {
        wasClicked = true;
        tile = this;
    }
}
