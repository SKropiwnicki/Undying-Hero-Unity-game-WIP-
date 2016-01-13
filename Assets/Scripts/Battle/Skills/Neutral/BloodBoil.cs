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

        source.APchange(-APCost);

    }
}
