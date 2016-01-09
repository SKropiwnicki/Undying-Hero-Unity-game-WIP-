using UnityEngine;
using System.Collections;

public class Fireball : Skill
{
    private float dmgRange = 0.40f;
    private float criticalDamageMultiply = 1.5f;
    private float intelligenceMultiply = 1.5f;
    public Fireball()
    {
        name = "Fireball";
        type = "DamageTarget";
        APCost = 4;
    }
    public override void action(Actor source, Actor target)
    {

        int baseDmg = Mathf.FloorToInt(source.intelligence * intelligenceMultiply);


        if (source.animator)
        {
            source.onAttackEfx();
        }



        int dmg = Mathf.FloorToInt(Random.Range(source.attackPower - (baseDmg * dmgRange), source.attackPower + (baseDmg * dmgRange)));
        bool isCritical = this.isCriticalHit(source.critChance);

        if (isCritical) dmg = Mathf.FloorToInt(dmg * criticalDamageMultiply);

        Debug.Log(source.name + " zadaje " + dmg + " dla " + target.name + " CRIT: " + isCritical);

        target.TakeDamage(dmg, isCritical);


        source.currentAP -= APCost;

        Debug.Log("Nowe AP:" + source.currentAP);
    }
}
