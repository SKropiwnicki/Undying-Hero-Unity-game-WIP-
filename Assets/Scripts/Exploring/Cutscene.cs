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

    public Image image;

    //[SerializeField]
    //[TextArea(8, 8)]
    public List<string> strs;

    public Button showAllButton;
    public Button skipButton;

    public GameObject objectWithText;
    private Text dialogArea;

	public static bool clicked;
    public AudioClip nextPageSound;

    public UnityAction afterFunc;

    void Awake()
    {
        dialogArea = objectWithText.GetComponentInChildren<Text>();

		UnityAction ua = new UnityAction (showAll);
		showAllButton.onClick.AddListener (ua);

		ua = new UnityAction (skip);
		skipButton.onClick.AddListener (ua);
    }

    public void make()
    {
        if (strs.Count == 0)
        {
            this.gameObject.SetActive(false);
            return;
        }
        this.gameObject.SetActive(true);
        StartCoroutine("doIt");
    }

    public void make(UnityAction ua)
    {
        this.afterFunc = ua;
        make();
    }

	public IEnumerator doIt()
	{
        speedx = speed;
		string str = "";
        for (int j = 0; j < strs.Count; j++)
        {
            int i = 0;
            while (i < strs[j].Length)
            {
                //znak rozpoczynajacy komendy i nie wyjdziemy poza string przy sprawdzaniu komendy
                if(strs[j][i] == '<' && strs[j].Length > i+5)
                {
                    #region cmd
                    if(strs[j][i + 1] == 'C'
                        && strs[j][i + 2] == 'L'
                        && strs[j][i + 3] == 'E'
                        && strs[j][i + 4] == 'A'
                        && strs[j][i + 5] == 'R')
                    {
                        speedx = -1; // przy speedzie <0 wymuszam czekanie na przycisk
                        str = "";
                        i+=6;
                    }

                    else if (strs[j][i + 1] == 'C'
                        && strs[j][i + 2] == 'L'
                        && strs[j][i + 3] == 'I'
                        && strs[j][i + 4] == 'C'
                        && strs[j][i + 5] == 'K')
                    {
                        if (speedx != 0)
                        {
                            speedx = -1; // przy speedzie <0 wymuszam czekanie na przycisk
                        }
                        i+=6;
                    }
                    if(strs[j].Length <= i) //gdyby komenda byla ostatnimi znakami stringa to lecimy do kolejnego
                    {
                        continue; //break to samo da
                    }
                    #endregion
                }
                str += strs[j][i]; //dodajemy po literce
                i++; //^
                if (speedx > 0) //wyswietlamy co trzeba i czekamy.. EFEKTY hue hue
                {
                    dialogArea.text = str;
                    yield return new WaitForSeconds(speedx);
                }
                else if(speedx < 0) // przy speedzie < 0 wymuszam czekanie na przycisk
                {
                    clicked = false;
                    while (!clicked)
                    {
                        yield return new WaitForSeconds(0.1f);
                    }
                    SoundManager.instance.playOnGui(nextPageSound);
                    speedx = speed; //a potem jade normalnie
                }
            }
            //jesli zaladowalismy stringa - nic innego przy speedzie 0 sie nie dzieje
            if (speedx == 0)
            {
                dialogArea.text = str; //to wyswietlamy go - tak dziala showAll
            }
            str += "\n"; //nowy string, nowa linia
        }
        //wypisalismy co bylo trzeba
        //wiec teraz czekamy na klik myszki i wywolujemy skip - czyli nic innego jak setActive(false);
        clicked = false;
        while (!clicked)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (clicked) //the end
        {
            SoundManager.instance.playOnGui(nextPageSound);
            skip();
        }
    }

    private void showAll()
    {
        speedx = 0;
    }

    private void skip()
    {
        this.gameObject.SetActive(false);

        if(afterFunc != null)
        {
            afterFunc.Invoke();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clicked = true;
    }
}
