using UnityEngine;
using System.Collections;

public class HeroStats : MonoBehaviour
{
    public static int level;
    public static int experience;

    public int maxHealth;
    [HideInInspector]
    public int health;

    public int initiative;
    public int def;

    public int strength;
    public int dexterity;
    public int intelligence;

    void Awake()
    {
        if(!Connector.wasGeneratedExploreToMap)
        {
            level = 1;
            experience = 0;
        }
    }
}
