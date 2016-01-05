using UnityEngine;
using System.Collections;

public class ExploreToBattle : MonoBehaviour
{
    public GameObject gameObjectWithHero1Script;
    public GameObject gameObjectWithHero2Script;

    public static Hero hero1;
    public static Hero hero2;

    public static bool wasGenerated;

    void Awake()
    {
        DontDestroyOnLoad(this.transform);

        if(hero1 == null)
        {
            hero1 = gameObjectWithHero1Script.GetComponent<Hero>();
        }
        if (hero2 == null)
        {
            hero2 = gameObjectWithHero2Script.GetComponent<Hero>();
        }
    }
}
