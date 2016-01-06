using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ButtonSkills : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string skillName;
    public bool enoughAP;
    public bool needNoTarget;
    public bool isOn;
    private Image img;
    public Color defaultColor;

    void Awake()
    {
        img = transform.GetComponent<Image>();
        defaultColor = img.color;


    }


    void Update()
    {
        if (isOn) if (!enoughAP) isOn = false;
        if (!isOn) if (enoughAP) isOn = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enoughAP) img.color = new Color(0.8f, 0.8f, 0.8f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (enoughAP)
        {
            if (!ButtonManager.instance.isButtonClicked)
            {
                defaultColor = new Color(0.0f, 0.8f, 0.0f, 1.0f);
                ButtonManager.instance.isButtonClicked = true;
            }
            else
            {
                ButtonManager.instance.isButtonClicked = false;
                defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }

        }
    }

}
