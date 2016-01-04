using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonSkills : MonoBehaviour
{
    public string skillName;
    private Image img;

	// Use this for initialization
	void Awake ()
    {
        img = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //img.color = new Color(1.0f, 0.2f, 0.2f, 1f);
    }

    void OnMouseEnter()
    {
        Debug.Log("OnMouseOver");
        img.color = new Color(1.0f, 0.2f, 0.2f, 1f);

    }
    void OnMouseExit()
    {
        Debug.Log("OnMouseExitxd");
        img.color = new Color(0.0f, 0.0f, 0.0f, 1f);
    }
        
}
