using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Whirlwind : Skill {

    public float dmgRange = 0.15f;
    public Whirlwind()
    {
        name = "Whirlwind";
        APCost = 8;
        type = "DamageAoE";
    }
    public override void action(Actor source)
    {
        int baseDmg = Mathf.FloorToInt(source.attackPower / 4 + source.dexterity / 2);


        if (source.animator)
        {
            source.onAttackEfx();
        }

        List<Actor> actors = new List<Actor>(TurnManagement.instance.actors);
        foreach (Actor target in actors)
        {
            if (target.isControllable != source.isControllable)
            { 
                int dmg = Mathf.FloorToInt(Random.Range(source.attackPower - (baseDmg * dmgRange), source.attackPower + (baseDmg * dmgRange)));
                bool isCritical = this.isCriticalHit(source.critChance);

                if (isCritical) dmg = Mathf.FloorToInt(dmg * 1.5f);

                Debug.Log(source.name + " zadaje " + dmg + " dla " + target.name + " CRIT: " + isCritical);

                target.TakeDamage(dmg, isCritical);
            }
        }

        source.currentAP -= APCost;
        
        Debug.Log("Nowe AP:" + source.currentAP);
    }
}
