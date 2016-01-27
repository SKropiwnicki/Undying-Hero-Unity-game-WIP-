using UnityEngine;
using System.Collections;

public class LesserDefend : Skill
{
    private int baseShield = 4;

    public LesserDefend()
    {
        name = "LesserDefend";
        type = "DefensiveNoTarget";
        APCost = -3;
        displayName = "Lesser Defend";
    }
    public override void action(Actor source)
    {
        source.changeShield(baseShield + Mathf.FloorToInt(source.strength / 4));
        source.addBuff(33, 1, ref source.reductionInPercent, "reductionInPercent");
        source.APchange(-APCost);
        ////Debug.Log("Nowe AP:" + source.currentAP);
    }
}
