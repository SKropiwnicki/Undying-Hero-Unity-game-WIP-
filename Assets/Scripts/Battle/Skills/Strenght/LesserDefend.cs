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
        source.changeShield(2 + Mathf.FloorToInt(source.strength / 3));
        source.currentAP -= APCost;
        Debug.Log("Nowe AP:" + source.currentAP);
    }
}
