using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class HeroesStats
{
    public int level;
    public int experience;
    public int onLevelUpPoints;

    public HeroesStats()
    {
        level = 1;
        experience = 0;

        onLevelUpPoints = 3;
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

    public int levelUpPointsLeft;

    public HeroStats()
    {
        maxHealth = 100;
        health = 100;

        initiative = 2;
        def = 3;

        strength = 10;
        dexterity = 10;
        intelligence = 10;

        levelUpPointsLeft = 0;
    }
}
