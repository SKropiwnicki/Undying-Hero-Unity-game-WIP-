using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrcMageAI : MonoBehaviour, IAI
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

        //string decision = "AutoAttack";
        bool isActionDone = false;
        //Bardzo sztywne szkile
        Skill heal = null;
        Skill bestAttack = null;
        Skill APUp = null;
        Skill autoattack = null;
        Skill buff = null;
        int highestAttackAP = -100;
        foreach (Skill skill in Me.skills)
        {
            if (skill.type == "HealTarget")
            {
                heal = skill;
            }
            else if (skill.type == "DamageTarget")
            {
                if (highestAttackAP < skill.APCost && skill.APCost < Me.maxAP)
                {
                    bestAttack = skill;
                    highestAttackAP = skill.APCost;
                }
            }
            else if (skill.type == "DefensiveNoTarget")
            {
                APUp = skill;
            }
            else if (skill.type == "BuffTarget")
            {
                buff = skill;
            }
        }

        if (Me.currentAP < 5)
        {
           if(APUp != null)
                if (APUp.hasEnoughAP(Me.currentAP))
                {
                    Debug.Log("Używam " + APUp.name);
                    isActionDone = true;
                    APUp.useSkill(Me);
                }

        }
       
       if (!isActionDone)
            foreach (Actor target in actors)
            {
                if (target.isControllable == Me.isControllable)
                {
                    if (target.health < target.maxHealth * 0.45f)
                    {
                        if (heal != null)
                        {
                            if (heal.hasEnoughAP(Me.currentAP) && !isActionDone)
                            {
                                Debug.Log("Używam heala");
                                isActionDone = true;
                                heal.useSkill(Me, target);
                                break;
                            }
                        }
                    }
                    else if (target.name.Contains("OrcRare"))
                    {

                        if (buff != null)
                        {
                            if (buff.hasEnoughAP(Me.currentAP) && !isActionDone)
                            {
                                Debug.Log("Używam Buffa");
                                isActionDone = true;
                                buff.useSkill(Me, target);
                                break;
                            }
                        }
                    }
                }
            }

       if (!isActionDone)
            foreach (Actor target in actors)
            {
                
                if (target.isControllable == Me.isControllable)
                {
                    if (target.health < target.maxHealth * 0.6f)
                    {
                        
                        if (heal != null)
                        {
                            if (heal.hasEnoughAP(Me.currentAP) && !isActionDone)
                            {
                                Debug.Log("Używam heala");
                                isActionDone = true;
                                heal.useSkill(Me, target);
                                break;
                            }
                        }
                    }
                    
                }
                if (target.name == "Hero1" || target.name == "Hero2")
                {
                    if (bestAttack != null)
                    {
                        if (bestAttack.hasEnoughAP(Me.currentAP) && !isActionDone)
                        {
                            Debug.Log("Używam Fireballa");
                            isActionDone = true;
                            bestAttack.useSkill(Me, target);
                            break;
                        }
                    } 
                    if (autoattack != null && !isActionDone)
                    {
                        Debug.Log("Autoatakuje");
                        isActionDone = true;
                        autoattack.useSkill(Me, target);
                        break;
                    }
                }
            }
    }
}
