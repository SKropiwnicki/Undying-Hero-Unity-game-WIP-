using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DefendTillYouCanHitHard : MonoBehaviour, IAI
{
    Actor Me; // Aktualny Actor dla którego AI jest wywołane.
    void Awake()
    {
        Me = GetComponent<Actor>();

        Me.ai = this;
    }

    public void specialAI()
    {
        List<Actor> actors = new List<Actor>(TurnManagement.instance.actors);

        Skill defend = new Defend();
        Skill bestAttack = new AutoAttack();
        //Skill autoattack = Me.skills.Find(x => x.name.Contains("AutoAttack"));  //Niepotrzebne w sumie.
        int highestAttackAP = -100;
        foreach (Skill skill in Me.skills)
        {
            if (skill.type == "DefensiveNoTarget")
            {
                defend = skill;
            }
            else if (skill.type == "DamageTarget")
            {
                if (highestAttackAP < skill.APCost && skill.APCost < Me.maxAP )
                {
                    bestAttack = skill;
                    highestAttackAP = skill.APCost;
                }
            }
        }


        //Debug.Log("AI " + Me.name + " Moj defend skill to " + defend.name + " a moj best attack to " + bestAttack.name);


        if (Me.currentAP < highestAttackAP)
        {
            defend.useSkill(Me);
        }

        else foreach (Actor target in actors )
        {
            if (target.name == "Hero1" || target.name == "Hero2")
            {
                    bestAttack.useSkill(Me, target); 
            }
        }
    }
}
