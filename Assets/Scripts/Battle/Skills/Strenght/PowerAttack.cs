using UnityEngine;
using System.Collections;

public class PowerAttack : Skill
{
    private float attackPowerMultiply = 1.75f;

    public PowerAttack()
    {
        name = "PowerAttack";
        type = "DamageTarget";
        APCost = 6;
        displayName = "Power Attack";
    }
    public override void action(Actor source, Actor target)
    {
        target.TakeDamage(Mathf.FloorToInt(source.attackPower * attackPowerMultiply), false);
        source.APchange(-APCost);
        source.onAttackEfx(); //dodane by Vuko
        //Debug.Log("Nowe AP:" + source.currentAP);
    }

}
