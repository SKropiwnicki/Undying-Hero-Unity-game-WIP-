﻿using UnityEngine;
using System.Collections;

public class AutoAttack : Skill
{
    public float dmgRange = 0.15f;
    public int baseDmg = 1;

    public AutoAttack()
    {
        name = "AutoAttack";
        APCost = 0;
        type = "DamageTarget";
        displayName = "Normal Attack";
    }
    public  override void action(Actor source, Actor target)
    {
        int dmg = baseDmg + Mathf.FloorToInt(Random.Range(source.attackPower - (source.attackPower * dmgRange), source.attackPower + (source.attackPower * dmgRange)));
        bool isCritical = this.isCriticalHit(source.critChance);

        if (isCritical) dmg = dmg * 2;


        Debug.Log(source.name+ " zadaje " + dmg + " dla " + target.name + " CRIT: "+isCritical);
        if (source.animator)
        {
            source.onAttackEfx();
        }
        target.TakeDamage(dmg, isCritical);
    }
}
