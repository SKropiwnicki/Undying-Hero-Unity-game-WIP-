using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class Cutscene : MonoBehaviour, IPointerClickHandler
{
    public float speed = 0.2f;
    private float speedx;

    //[SerializeField]
    //[TextArea(8, 8)]
    public List<string> strs;

    public Button showAllButton;
    public Button skipButton;

    public GameObject objectWithText;
    private Text dialogArea;

	public static bool clicked;

    void Awake()
    {
        dialogArea = objectWithText.GetComponentInChildren<Text>();

		UnityAction ua = new UnityAction (showAll);
		showAllButton.onClick.AddListener (ua);

		ua = new UnityAction (skip);
		skipButton.onClick.AddListener (ua);
    }

	public IEnumerator doIt()
	{
        if(strs.Count == 0)
        {
            this.gameObject.SetActive(false);
            yield break;
        }
        this.gameObject.SetActive(true);
        speedx = speed;
		yield return new WaitForSeconds (0.1f);
		string str = "";
        for (int j = 0; j < strs.Count; j++) 
		{
            int i = 0;
            if(strs[j].StartsWith("CLEAR"))
            {
                speedx = speed;
                str = "";
                i = 5;
            }
			while (i < strs [j].Length)
			{
				str += strs [j] [i];
				i++;
                if (speedx != 0)
                {
                    dialogArea.text = str;
                    yield return new WaitForSeconds(speedx);
                }
			}
            if(speedx == 0)
            {
                dialogArea.text = str;
                speedx = speed;
            }
            clicked = false;
            while (!clicked) 
			{
				yield return new WaitForSeconds (0.1f);
			}
		}
        if(clicked)
        {
            skip();
        }
	}

    private void showAll()
    {
        speedx = 0;
    }

    private void clear()
    {
        dialogArea.text = "";
    }

    private void skip()
    {
        //Destroy (this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = true;
    }
}
