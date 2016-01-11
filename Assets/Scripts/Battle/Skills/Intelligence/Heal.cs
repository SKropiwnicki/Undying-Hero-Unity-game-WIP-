using UnityEngine;
using System.Collections;

public class Heal : Skill {
    private float healRange = 0.15f;
    private float intelligenceMultiply = 1.0f;
    private float criticalMultiply = 1.8f;
    public Heal()
    {
        name = "Heal";
        APCost = 4;
        type = "HealTarget";
    }
    public override void action(Actor source, Actor target)
    {
        int baseHeal = Mathf.FloorToInt(source.intelligence * intelligenceMultiply );
        int heal = Mathf.FloorToInt(Random.Range(baseHeal - (baseHeal * healRange), baseHeal + (baseHeal * healRange)));

        int critChance = source.intelligence;
        if (critChance > 30) critChance = 30;
        bool isCritical = this.isCriticalHit(critChance);

        if (isCritical) heal = Mathf.FloorToInt(heal * criticalMultiply);


        Debug.Log(source.name + " Leczy " + heal + " dla " + target.name + " CRIT: " + isCritical);


        if (source.animator)
        {
            source.onAttackEfx();
        }

        //Pozniej to mozna inaczej rozwiazac
        target.health += heal;
        if (target.health > target.maxHealth) target.health = target.maxHealth;
        string txt = "+" + heal;
        target.healthBar.value = target.health;

        TextSpawner.instance.spawn(target.transform, txt, Color.green, 42);

//target.TakeDamage(dmg, isCritical);
        source.currentAP -= APCost;
        Debug.Log("Nowe AP:" + source.currentAP);
    }
}
