using UnityEngine;
using System.Collections;

public class Defend : Skill {

    public Defend()
    {
        name = "Defend";
        type = "DefensiveNoTarget";
        APCost = -4;
    }
    public override void action(Actor source)
    {
        source.changeShield(10 + Mathf.FloorToInt(source.strength/4) );

        source.APchange(-APCost);
        //source.currentAP -= APCost;  
        //Debug.Log("Nowe AP:" + source.currentAP);
    }
}
