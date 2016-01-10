using UnityEngine;
using System.Collections;

public class Connector : MonoBehaviour
{
    public static Connector instance;

    public GameObject gameObjectWithHero1Stats;
    public GameObject gameObjectWithHero2Stats;

    public static HeroStats hero1;
    public static HeroStats hero2;

    public static DungeonStats dungeon;

    public static bool wasGeneratedMapToExplore;
    public static bool wasGeneratedExploreToMap;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.transform);

        if (hero1 == null)
        {
            hero1 = gameObjectWithHero1Stats.GetComponent<HeroStats>();
        }
        if (hero2 == null)
        {
            hero2 = gameObjectWithHero2Stats.GetComponent<HeroStats>();
        }
    }

    public void beforeExploreFromMap()
    {
        dungeon = OnDungClick.dungeon;
        wasGeneratedMapToExplore = true;
        BattleToExplore.wasGenerated = false;
    }

    public void beforeMapFromExplore()
    {
        wasGeneratedExploreToMap = true;
        wasGeneratedMapToExplore = false;
    }
}
