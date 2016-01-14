using UnityEngine;
using System.Collections;

public class LesserDefend :Skill {

    public LesserDefend()
    {
        name = "LesserDefend";
        type = "DefensiveNoTarget";
        APCost = -3;
    }
    public override void action(Actor source)
    {
        source.changeShield(6 + Mathf.FloorToInt(source.strength / 5));
        source.APchange(-APCost);
        //Debug.Log("Nowe AP:" + source.currentAP);
    }
}
