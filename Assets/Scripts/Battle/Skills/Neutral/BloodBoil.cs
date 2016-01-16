using UnityEngine;
using System.Collections;

public class BloodBoil : Skill {

    private float healthPercent = 0.2f;
    private float buffMultiply = 0.3f;
    
    public BloodBoil()
    {
        name = "BloodBoil";
        APCost = 4;
        type = "BuffTarget";
        displayName = "Blood Boil";
    }
    public override void action(Actor source, Actor target)
    {
        int dmgDealt = Mathf.FloorToInt(target.health * healthPercent);

        target.TakeDamage(dmgDealt, false);

        //tymczasowy kolorek
        target.spriteRenderer.color = new Color(1.0f, 0f, 0f, 1.0f);


        //Tu byńdzie buff
        int buffValue = Mathf.FloorToInt(target.strength * buffMultiply) - target.strength;
        target.addBuff(buffValue, 3, ref target.strength, "strength");
        int defDebuff = 0 - target.def;
        target.addBuff(defDebuff, 3, ref target.def, "def");

        source.APchange(-APCost);

    }
}
