﻿using UnityEngine;
using System.Collections;

public class Fireball : Skill
{
    public float dmgRange = 0.15f;
    public Fireball()
    {
        name = "Fireball";
        APCost = 2;
    }
    public override void action(Actor source, Actor target)
    {
        int dmg = Mathf.FloorToInt(Random.Range(source.attackPower - (source.attackPower * dmgRange), source.attackPower + (source.attackPower * dmgRange)));
        bool isCritical = this.isCriticalHit(source.critChance);

        if (isCritical) dmg = dmg * 2;


        Debug.Log(source.name + " zadaje " + dmg + " dla " + target.name + " CRIT: " + isCritical);
        if (source.animator)
        {
            source.onAttackEfx();
        }
        target.TakeDamage(dmg, isCritical);
        source.currentAP -= APCost;
        Debug.Log("Nowe AP:" + source.currentAP);
    }
}
