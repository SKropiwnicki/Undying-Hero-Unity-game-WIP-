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
    public bool isChosen;
    private Image img;
    public Color defaultColor;

    [HideInInspector]
    public GameObject txtPanel;
    public string txt;

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
        txtPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.color = defaultColor;
        txtPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (enoughAP)
        {
            if (!ButtonManager.instance.isButtonClicked && !isChosen)  // Jesli zaden przycisk nie jest wybrany
            {
                Debug.Log("Button nie jest clicked i nie jest chosen");
                isChosen = true;
                defaultColor = new Color(0.0f, 0.8f, 0.0f, 1.0f);
                ButtonManager.instance.isButtonClicked = true;
            }
            else if (ButtonManager.instance.isButtonClicked && !isChosen) // Jesli jakis przycisk jest juz wybrany, ale to nie ten to nic sie ma nie dziac
            {
                Debug.Log("Button jakis jest klikniety ale ten nie jest chosen");
            }
            else if (ButtonManager.instance.isButtonClicked && isChosen) // Jeśli ten przycisk byl wcisniety i zostaje wcisniety drugi raz
            {
                defaultColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                ButtonManager.instance.isButtonClicked = false;
                isChosen = false;
            }

        }
    }

}
