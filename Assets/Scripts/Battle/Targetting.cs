﻿using UnityEngine;
using System.Collections;

public class Targetting : MonoBehaviour {

    private bool isTargetting;
    private bool isSkillUsed;
    private string skillName;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

       if (Input.GetMouseButtonDown(0) && isTargetting)
        {
            isSkillUsed = false;
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 10f);
            Debug.Log("Position of click" + new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y)); //do wyjebania potem     
            if (hit.collider != null  && hit.transform.tag == "Target")
            {

                Actor source = TurnManagement.instance.getCurrentActor();
               
                foreach (Skill skill in source.skills)
                {
                    Debug.Log(skillName + " " + skill.name);
                    if (skill.name == skillName)
                    {
                        isSkillUsed = true;
                        skill.action(source, hit.collider.gameObject.GetComponent<Actor>());
                    }
                    if (isSkillUsed) break;
                }
            
                isTargetting = false;
            }
        }

    }

    public void checkTarget(string buttonName)
    {
        skillName = buttonName;
        isTargetting = true;
    }
}
