using UnityEngine;
using UnityEngine.EventSystems;

public class MyEventListener : MonoBehaviour, IPointerClickHandler
{
    private float clickTime;            // time of click
    public bool onClick = true;            // is click allowed on button?
    public bool onDoubleClick = false;    // is double-click allowed on button?


    public void OnPointerClick(PointerEventData data)
    {
        int clickCount = 1; // single click

        // get interval between this click and the previous one (check for double click)
        float interval = data.clickTime - clickTime;

        // if this is double click, change click count
        if (interval < 1.0 && interval > 0 && onDoubleClick)
            clickCount = 2;

        // reset click time
        clickTime = data.clickTime;

        // single click
        if (onClick && clickCount == 1)
        {
            ButtonSkills szkil = this.GetComponent<ButtonSkills>();
            Targetting.instance.checkTarget(szkil.skillName);  
        }

        // double click
        if (onDoubleClick && clickCount == 2)
        {
            // enter code here
        }

    }
}
