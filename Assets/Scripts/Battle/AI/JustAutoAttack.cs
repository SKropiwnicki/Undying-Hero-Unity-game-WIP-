using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JustAutoAttack : MonoBehaviour, IAI
{
    Actor Me;
    void Awake()
    {
        Me = GetComponent<Actor>();

        Me.ai = this;
    }

    public void specialAI()
    {
        List<Actor> actors = new List<Actor>(TurnManagement.instance.actors);
        foreach (Actor target in actors )
        {
            if (target.name == "Hero1" || target.name == "Hero2")
            {
                //TurnManageMent.instance.nextTurn();
                Skill autoattack = Me.skills.Find(x => x.name.Contains("AutoAttack"));
                if (autoattack != null) autoattack.useSkill(Me, target);  // To moze sprawic problemy jesli autoattack nie jest na 0 pozycji. ALE MUSI BYC.
            }
        }
    }
}
