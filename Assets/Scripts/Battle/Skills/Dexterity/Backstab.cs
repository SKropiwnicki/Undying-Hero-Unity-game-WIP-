using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Backstab : Skill {

    public float dmgRange = 0.10f;
    private float dexterityMultiply = 1.0f;
    private float criticalMultiply = 2.0f;
    private float noShieldBonus = 1.25f;
    public Backstab()
    {
        name = "Backstab";
        APCost = 5;
        type = "DamageTarget";
        displayName = "Backstab";
    }
    public override void action(Actor source, Actor Target)
    {
        int baseDmg = Mathf.FloorToInt(5 +  source.dexterity * dexterityMultiply);


        if (source.animator)
        {
            source.onAttackEfx();
        }

        if (Target.shield == 0) baseDmg = Mathf.FloorToInt(baseDmg * noShieldBonus);
        
        int dmg = Mathf.FloorToInt(Random.Range(source.attackPower - (baseDmg * dmgRange), source.attackPower + (baseDmg * dmgRange)));
        bool isCritical = this.isCriticalHit(source.critChance);

        if (isCritical) dmg = Mathf.FloorToInt(dmg * criticalMultiply);
        

        Target.TakeDamage(dmg, isCritical, false, true);


        source.APchange(-APCost);

        //Debug.Log("Nowe AP:" + source.currentAP);
    }
}
