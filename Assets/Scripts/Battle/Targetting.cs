using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour
{
    public static Targetting instance;

    private bool isTargetting;
    private bool isSkillUsed;
    private bool needNoTarget;
    private string skillName;

    private bool isMessageDisplayed;
	
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (BattleLoader.loaded && !TurnManagement.instance.isBattleFinished)
        {
            
            //Dla skilli bez targetu
            if (needNoTarget && isTargetting)
            {
                ButtonManager.instance.isButtonClicked = false; //odlikujemy button bo juz jego funkcje zostana na pewno wykonane.

                isSkillUsed = false;
                Actor source = TurnManagement.instance.getCurrentActor();
                List<Skill> sourceskills = new List<Skill>(source.skills);
               // Debug.Log("Wszedlem do skilla bez targetingu" + skillName);

                foreach (Skill skill in sourceskills)
                {
                    Debug.Log(skill.name + " == "  + skillName);
                    if (skill.name == skillName)
                    {
                        isSkillUsed = true;
                        //Debug.Log("Wywoluje selfclick z  targettingu: " + skill.name);
                        skill.useSkill(source);
                    }
                    if (isSkillUsed || TurnManagement.instance.isBattleFinished) break;
                }

                isTargetting = false;
            }
            //Dla skilli z targetem (uzywamy raycast)
            else if (isTargetting && ButtonManager.instance.isButtonClicked)
            {
                if (!isMessageDisplayed)
                {
                    TextSpawner.instance.spawn(this.transform, "Choose your target", Color.green, 72);
                    isMessageDisplayed = true;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    isSkillUsed = false;
                    RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 10f);
                    //Debug.Log("Position of click" + new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y)); //do wyjebania potem     
                    if (hit.collider != null && hit.transform.tag == "Target")
                    {
                        ButtonManager.instance.isButtonClicked = false; //Odklikujemy button jak juz mamy cel dla niego

                        Actor source = TurnManagement.instance.getCurrentActor();
                        List<Skill> sourceskills = new List<Skill>(source.skills);

                        foreach (Skill skill in sourceskills)
                        {

                            if (skill.name == skillName)
                            {
                                isSkillUsed = true;
                                isMessageDisplayed = false;
                                // Debug.Log("Wywoluje z targettingu: " + skill.name);
                                skill.useSkill(source, hit.collider.gameObject.GetComponent<Actor>());
                            }
                            if (isSkillUsed || TurnManagement.instance.isBattleFinished) break;
                        }

                        isTargetting = false;
                    }
                }
            }
        }
    }

    public void checkTarget(string buttonName, bool needNoTarget)
    {
        this.needNoTarget = needNoTarget;
        skillName = buttonName;
        isTargetting = true;
    }
}
