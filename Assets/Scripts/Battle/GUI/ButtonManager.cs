using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    public GameObject AutoAttackButton;
    public GameObject PowerAttackButton;
    public GameObject DefendButton;
    public GameObject parent;
    public List<GameObject> currentButtons;
    private List<GameObject> allButtons;

    public float buttonOffsetX = 2.0f;
    
    public float x = -4f;
    public float y = -6f;

    #region AP

    private List<Image> apList;
    public int allAp;
    public int widthAp;
    public GameObject parentAP;
    public Image emptyAp;
    public Image greenAp;
    public Image redAp;

    private void spawnAp()
    {
        apList = new List<Image>();
        for (int i = 0; i < allAp; i++)
        {
            Image go = Instantiate(emptyAp, new Vector3(0, 0, 0), Quaternion.identity) as Image;
            go.transform.SetParent(parentAP.transform, false);

            Vector3 worldPos = new Vector3(x - 2.25f + ((widthAp / 100.0f) * i), y + 1.25f, 0);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            go.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

            apList.Add(go);
        }
    }

    private void updateAp(int ap, int mapAp)
    {
        for(int i = 0; i < allAp; i++)
        {
            if(i < ap)
            {
                apList[i].sprite = greenAp.sprite;
            }
            else if(i < mapAp)
            {
                apList[i].sprite = redAp.sprite;
            }
            else
            {
                apList[i].sprite = emptyAp.sprite;
            }
        }
    }

    #endregion

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
    public void init()
    {
        currentButtons = new List<GameObject>();
        allButtons = new List<GameObject>();
        allButtons.Add(AutoAttackButton);
        allButtons.Add(PowerAttackButton);

        spawnAp();
        allButtons.Add(DefendButton);
    }
    //jak dochodza nowe skille to buttony dla nich tutaj dodajemy 

    public void spawnButtons(Actor actor)
    {
        DestroyOldButtons();
        updateAp(0, 0);

        if (!TurnManagement.instance.isBattleFinished  && actor.isControllable)
        {
            updateAp(actor.currentAP, actor.maxAP);
            int i = 0;
            foreach (Skill skill in actor.skills)
            {
                //Debug.Log("Mam skill o nazwie " + skill.name + (" i chce dla niego buttona"));
                foreach (GameObject buttonPrefab in allButtons)
                {
                    
                    if (skill.name == buttonPrefab.GetComponent<ButtonSkills>().skillName)
                    {
                        //Debug.Log("Generuje button dla skilla " + skill.name );
                        GameObject button = Instantiate(buttonPrefab, new Vector3(0.0f, 0.0f, 0), Quaternion.identity) as GameObject;
                        button.transform.SetParent(this.gameObject.transform, false);

                        Vector3 worldPos = new Vector3(x + (2.0f * i), y, 0);
                        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                        button.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

                        currentButtons.Add(button);
                        
                        //Sprawdzanie kosztu AP
                        if (skill.APCost > actor.currentAP)
                        {
                            button.GetComponent<ButtonSkills>().defaultColor = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                            button.GetComponent<ButtonSkills>().enoughAP = false;
                            button.transform.GetComponent<Image>().color = button.GetComponent<ButtonSkills>().defaultColor;
                        }
                        else button.GetComponent<ButtonSkills>().enoughAP = true;
                    }

                }
                i++;
            }
           // checkButtonsAP(actor);
        }
    }

    private void DestroyOldButtons()
    {
        if (currentButtons.Count > 0)
        {
            foreach (GameObject button in currentButtons)
            {
                Destroy(button);
            }
        }
    }


    /*
    Buttony maja prefaby  w którym jest przypisane wywołanie skryptu z targetting i tam też nazwa danego skilla.
    Buttony sie beda instantiate'owac przy zmianie tury, a metody do tego będą stąd wywołowany
    Dwa warunki: If actor jest controllable  i ze skille nie moga przekraczac miejsca

    */
}
