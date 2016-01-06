using UnityEngine;
using System.Collections;

public class Defend : Skill {

    public float dmgRange = 0.15f;
    public Defend()
    {
        name = "Defend";
        APCost = -4;
    }
    public override void action(Actor source)
    {
        source.def += 5;
        source.currentAP -= APCost;
        Debug.Log("Nowe AP:" + source.currentAP);
    }
}
