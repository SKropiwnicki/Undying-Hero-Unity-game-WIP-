using UnityEngine;
using System.Collections;

public class MindBlast : Skill 
{
    private float dmgRange = 0.10f;
    private float criticalDamageMultiply = 2.0f;
    private int minDmg = 30;
    private float healthPercent = 0.6f;
    private int intelligenceDiffMultiply = 4;
    //private float intelligenceMultiply = 1.1f;
    public MindBlast()
    {
        name = "MindBlast";
        type = "DamageTarget";
        APCost = 6;
        displayName = "Mind Blast";
    }
    public override void action(Actor source, Actor target)
    {

        int baseDmg = Mathf.FloorToInt((source.intelligence - target.intelligence)*intelligenceDiffMultiply);
            //  //Debug.Log("Base dmg w fireballu:" + baseDmg + " Int ziomka: " + source.intelligence);
        if (baseDmg <= 0) baseDmg = 1;
        if (baseDmg >= target.maxHealth * healthPercent && baseDmg > minDmg)
        {
            baseDmg = Mathf.FloorToInt(target.maxHealth * healthPercent);
        }

        if (source.animator)
        {
            source.onAttackEfx();
        }



        int dmg = Mathf.FloorToInt(Random.Range(baseDmg - (baseDmg * dmgRange), baseDmg + (baseDmg * dmgRange)));
        bool isCritical = this.isCriticalHit(source.critChance);

        if (isCritical) dmg = Mathf.FloorToInt(dmg * criticalDamageMultiply);

        // //Debug.Log(source.name + " zadaje fireballem" + dmg + " dla " + target.name + " CRIT: " + isCritical);

        target.TakeDamage(dmg, isCritical);


        source.APchange(-APCost);

        ////Debug.Log("Nowe AP:" + source.currentAP);
    }
}