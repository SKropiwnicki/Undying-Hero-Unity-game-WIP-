using UnityEngine;
using System.Collections;

public class PowerAttack : Skill
{
    public PowerAttack()
    {
        name = "PowerAttack";
        APCost = 6;
    }
    public override void action(Actor source, Actor target)
    {
        source.strength += 5;
        target.TakeDamage(source.attackPower*2, false);
        source.currentAP -= APCost;

        Debug.Log("Nowe AP:" + source.currentAP);
    }

    //to delete
    public override void action(Actor source)
    {
        source.TakeDamage(-20, false);
    }
}
