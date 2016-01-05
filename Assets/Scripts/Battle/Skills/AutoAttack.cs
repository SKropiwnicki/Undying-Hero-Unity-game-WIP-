using UnityEngine;
using System.Collections;

public class AutoAttack : Skill
{
    public AutoAttack()
    {
        name = "AutoAttack";
    }
    public  override void action(Actor source, Actor target)
    {
        Debug.Log(source.name+ " zadaje " + source.attackPower + " dla " + target.name);
        if (source.animator)
        {
            source.onAttackAnimation();
        }
        target.TakeDamage(source.attackPower);
    }
}
