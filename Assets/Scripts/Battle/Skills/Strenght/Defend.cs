using UnityEngine;
using System.Collections;

public class Defend : Skill {

    public Defend()
    {
        name = "Defend";
        type = "DefensiveNoTarget";
        APCost = -3;
        displayName = "Defend";
    }
    public override void action(Actor source)
    {
        source.changeShield(10 + Mathf.FloorToInt(source.strength/4) );
        source.addBuff(50, 1, ref source.reductionInPercent, "reductionInPercent");
        source.APchange(-APCost);
        //source.currentAP -= APCost;  
        //Debug.Log("Nowe AP:" + source.currentAP);
    }
}
