using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    public GameObject parent;
    public List<GameObject> currentButtons;
    private List<GameObject> allButtons;

    public bool isButtonClicked;

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

    #region txtPanel
    public GameObject txtPanel;
    private GameObject panelParent;
    private Rect rect;

    private void spawnTxt(ButtonSkills bs)
    {
        bs.txtPanel = Instantiate(txtPanel, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        bs.txtPanel.GetComponentInChildren<Text>().text = bs.txt;
        bs.txtPanel.transform.SetParent(panelParent.transform, false);
        bs.txtPanel.transform.position = new Vector3(bs.transform.position.x, bs.transform.position.y, 0);
        bs.txtPanel.transform.localPosition = new Vector3(bs.txtPanel.transform.localPosition.x + rect.width / 2, bs.txtPanel.transform.localPosition.y + rect.height / 2 + 69, 0);
        bs.txtPanel.SetActive(false);
    }
    #endregion

    public GameObject apPerTurnPrefab;
    private GameObject apPerTurnText;

    private void spawnApText(Actor actor, Transform t)
    {
        apPerTurnText = Instantiate(apPerTurnPrefab) as GameObject;
        apPerTurnText.transform.SetParent(panelParent.transform, false);
        apPerTurnText.GetComponent<Text>().text = "+ " + actor.perTurnAp + " AP per turn";
        Vector2 v2 = new Vector2(t.position.x, t.position.y);
        Rect rect = t.GetComponent<RectTransform>().rect;
        Debug.Log(v2.x + " " + v2.y);
        apPerTurnText.transform.position = new Vector2(v2.x, v2.y);
        apPerTurnText.transform.localPosition = new Vector2(apPerTurnText.transform.localPosition.x - (1.75f * rect.width), apPerTurnText.transform.localPosition.y + (0.83f * rect.height));
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        panelParent = GameObject.Find("Canvas");
        rect = txtPanel.GetComponent<RectTransform>().rect;
    }
    
    public void init()
    {
        currentButtons = new List<GameObject>();
        allButtons = new List<GameObject>();

        string path = "SkillButtons";
        Object[] skillButtons = Resources.LoadAll(path, typeof(GameObject));
        foreach (Object obj in skillButtons)
        {
            allButtons.Add(obj as GameObject);
        }
        
        spawnAp();
        
    }


    public void spawnButtons(Actor actor)
    {
        DestroyOldButtons();
        Destroy(apPerTurnText);
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

                        if(i == 0)
                        {
                            spawnApText(actor, button.transform);
                        }

                        spawnTxt(button.GetComponentInChildren<ButtonSkills>());
                        
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
