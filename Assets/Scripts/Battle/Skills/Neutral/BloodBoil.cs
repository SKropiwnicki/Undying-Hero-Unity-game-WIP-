using UnityEngine;
using System.Collections;

public class BloodBoil : Skill {

    private float healthPercent = 0.2f;
    private float buffMultiply = 0.5f;
    
    public BloodBoil()
    {
        name = "BloodBoil";
        APCost = 3;
        type = "BuffTarget";
    }
    public override void action(Actor source, Actor target)
    {
        int dmgDealt = Mathf.FloorToInt(target.health * healthPercent);

        target.TakeDamage(dmgDealt, false);

        //tymczasowy kolorek
        target.spriteRenderer.color = new Color(1.0f, 0f, 0f, 1.0f);


        //Tu byńdzie buff
        target.strength += Mathf.FloorToInt(target.strength * buffMultiply);
        target.def = 0;

        /*

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
        Debug.Log("Nowe AP:" + source.currentAP);*/
    }
}
