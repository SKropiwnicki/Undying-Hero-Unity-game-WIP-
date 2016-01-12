using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class HeroesStats
{
    public int level;
    public int experience;

    public HeroesStats()
    {
        level = 1;
        experience = 0;
    }
}

[Serializable]
public class HeroStats
{
    public int maxHealth;
    [HideInInspector]
    public int health;

    public int initiative;
    public int def;

    public int strength;
    public int dexterity;
    public int intelligence;

    public HeroStats()
    {
        maxHealth = 100;
        health = 100;

        initiative = 2;
        def = 3;

        strength = 10;
        dexterity = 10;
        intelligence = 10;
    }
}
