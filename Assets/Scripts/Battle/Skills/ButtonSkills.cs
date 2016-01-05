using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonSkills : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string skillName;
    private Image img;
    private Color defaultColor;

    void Awake ()
    {
        img = transform.GetComponent<Image>();
        defaultColor = img.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.color = new Color(1.0f, 0.2f, 0.2f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.color = defaultColor;
    }
}
