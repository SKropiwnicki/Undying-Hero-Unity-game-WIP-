﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JustAutoAttack : MonoBehaviour, IAI
{
    Actor dis;
    void Awake()
    {
        dis = GetComponent<Actor>();
        if (dis)
        {
            dis.hasSpecialAI = true;
        }
        dis.ai = this;
    }

    public void specialAI()
    {
        List<Actor> actors = new List<Actor>(TurnManagement.instance.actors);
        foreach (Actor target in actors )
        {
            if (target.name == "Hero1" || target.name == "Hero2")
            {
                //TurnManagement.instance.nextTurn();
                dis.skills[0].useSkill(dis, target);  // To moze sprawic problemy jesli autoattack nie jest na 0 pozycji. ALE MUSI BYC.
            }
        }
    }
}
