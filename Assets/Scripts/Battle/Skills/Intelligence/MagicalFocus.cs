using UnityEngine;
using System.Collections;

public class MagicalFocus : Skill
{

    public MagicalFocus()
    {
        name = "MagicalFocus";
        type = "DefensiveNoTarget";
        APCost = 3;
    }
    public override void action(Actor source)
    {


        int minusAP = Mathf.FloorToInt( 3 - source.intelligence/8);
        if (minusAP < 0) minusAP = 0;
        int buffInt = Mathf.FloorToInt(source.intelligence / 4);

        Debug.Log("Magical focus:" + minusAP);
        int newAP = Mathf.FloorToInt(source.maxAP - minusAP);
       
        source.currentAP = newAP;
        //Tymczasowo tak buffuje
        source.intelligence += buffInt;
        
        

        Debug.Log("Magical focus new AP:" + source.currentAP);
    }
}
