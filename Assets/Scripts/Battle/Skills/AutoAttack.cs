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
        target.TakeDamage(source.attackPower);
    }
}
