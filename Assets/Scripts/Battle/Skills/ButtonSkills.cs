using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSkills : MonoBehaviour
{
    public string skillName;
    private Image img;
    private Color defaultColor;

    private bool overTrigger;

    private EventSystem EventSystem;
    
    void Awake ()
    {
        img = GetComponent<Image>();
        defaultColor = img.color;
    }

    void Start()
    {
        EventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }
	
	void Update ()
    {
        if (EventSystem.IsPointerOverGameObject())
        {
            if (!overTrigger)
            {
                Debug.Log("OnMouseOver");
                img.color = new Color(1.0f, 0.2f, 0.2f, 1f);
                overTrigger = true;
            }
        }
        else
        {
            if (overTrigger)
            {
                Debug.Log("OnMouseExit");
                img.color = defaultColor;
                overTrigger = false;
            }
        }
    }
}
