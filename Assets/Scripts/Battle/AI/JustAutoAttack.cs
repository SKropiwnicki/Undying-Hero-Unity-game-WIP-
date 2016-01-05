using UnityEngine;
using System.Collections;

public class JustAutoAttack :  MonoBehaviour, IAI
{
    Actor dis;
    void Awake()
    {
        dis = GetComponent<Actor>();
        if (dis) dis.hasSpecialAI = true;
        dis.ai = this;
        
    }

    public  void specialAI()
    {
        
        foreach (Actor target in TurnManagement.instance.actors)
        {
            if (target.name == "Hero1" || target.name == "Hero2")
            {
                dis.skills[0].useSkill(dis, target);  // To moze sprawic problemy jesli autoattack nie jest na 0 pozycji. ALE MUSI BYC.
            }
        }
    }
}
