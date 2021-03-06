﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllSkills : MonoBehaviour
{
    public static AllSkills instance;

    public List<Skill> allSkillList;

	// Use this for initialization
	void Awake ()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
	
    //powinno jako pierwsze właściwie. Tutaj dodajemy nowe skille, bo inaczej są one nieznane przez unity ;_;
    public void init()
    {
        allSkillList = new List<Skill>();
        allSkillList.Add(new PowerAttack());
        allSkillList.Add(new Defend());
        allSkillList.Add(new LesserDefend());
        allSkillList.Add(new Whirlwind());
        allSkillList.Add(new Fireball());
        allSkillList.Add(new Heal());
        allSkillList.Add(new MagicalFocus());
        allSkillList.Add(new BloodBoil());
        allSkillList.Add(new MindBlast());
        allSkillList.Add(new Backstab());
    }
}
