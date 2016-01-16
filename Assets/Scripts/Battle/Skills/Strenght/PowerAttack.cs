using UnityEngine;
using System.Collections;

public class PowerAttack : Skill
{
    public PowerAttack()
    {
        name = "PowerAttack";
        type = "DamageTarget";
        APCost = 6;
        displayName = "Power Attack";
    }
    public override void action(Actor source, Actor target)
    {
       
        target.TakeDamage(source.attackPower*2, false);
        source.APchange(-APCost);
        source.onAttackEfx(); //dodane by Vuko
        //Debug.Log("Nowe AP:" + source.currentAP);
    }

    //to delete
    public override void action(Actor source)
    {
        source.TakeDamage(-20, false);
    }
}
