using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;

    public GameObject AutoAttackButton;
    public GameObject PowerAttackButton;
    public GameObject parent;
    public List<GameObject> currentButtons;
    private List<GameObject> allButtons;

    public float buttonOffsetX = 2.0f;

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
    }
    //jak dochodza nowe skille to buttony dla nich tutaj dodajemy 



    public void spawnButtons(Actor actor)
    {
        DestroyOldButtons();

        if (!TurnManagement.instance.isBattleFinished)
        {

            int i = 0;
            foreach (Skill skill in actor.skills)
            {
                Debug.Log("Szukanie buttona dla skilla: " + skill.name);

                foreach (GameObject buttonPrefab in allButtons)
                {
                    Debug.Log("Ogarniam buttonek dla : " + allButtons[0].GetComponent<ButtonSkills>().skillName);
                    if (skill.name == buttonPrefab.GetComponent<ButtonSkills>().skillName)
                    {
                        GameObject button = Instantiate(buttonPrefab, new Vector3(0.0f, 0.0f, 0), Quaternion.identity) as GameObject;
                        button.transform.SetParent(this.gameObject.transform, false);

                        Vector3 worldPos = new Vector3(-6.0f + (2.0f * i), -4.0f, 0);
                        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
                        button.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

                        currentButtons.Add(button);
                    }

                }
                i++;
            }
        }
    }

    private void DestroyOldButtons()
    {
        if (currentButtons.Count > 0)
        {
            foreach (GameObject button in currentButtons)
            {
                Debug.Log("Done");
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
