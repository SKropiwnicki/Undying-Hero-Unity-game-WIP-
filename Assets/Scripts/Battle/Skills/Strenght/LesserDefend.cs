﻿using UnityEngine;
using System.Collections;

public class LesserDefend :Skill {

    public LesserDefend()
    {
        name = "LesserDefend";
        type = "DefensiveNoTarget";
        APCost = -3;
        displayName = "Lesser Defend";
    }
    public override void action(Actor source)
    {
        source.changeShield(6 + Mathf.FloorToInt(source.strength / 5));
        source.addBuff(33, 1, ref source.reductionInPercent, "reductionInPercent");
        source.APchange(-APCost);
        ////Debug.Log("Nowe AP:" + source.currentAP);
    }
}
