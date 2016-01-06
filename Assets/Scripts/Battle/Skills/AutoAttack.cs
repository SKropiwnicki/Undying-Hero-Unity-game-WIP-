using UnityEngine;
using System.Collections;

public class AutoAttack : Skill
{
    public AutoAttack()
    {
        name = "AutoAttack";
        APCost = -3;
    }
    public  override void action(Actor source, Actor target)
    {
        Debug.Log(source.name+ " zadaje " + source.attackPower + " dla " + target.name);
        if (source.animator)
        {
            source.onAttackAnimation();
        }
        target.TakeDamage(source.attackPower);
        source.currentAP -= APCost;
        Debug.Log("Nowe AP:" + source.currentAP);
    }
}
