using UnityEngine;
using System.Collections;

public class PowerAttack : Skill
{
    public PowerAttack()
    {
        name = "PowerAttack";
    }
    public override void action(Actor source, Actor target)
    {
        target.TakeDamage(source.attackPower*2);
    }
}
