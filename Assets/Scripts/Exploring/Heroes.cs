using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Heroes : MonoBehaviour
{
    public static int level;
    public static int experience;

    public Text levelText;
    public Text experienceText;

    void Awake()
    {
        if(BattleToExplore.wasGenerated)
        {
            level = BattleToExplore.level;
            experience = BattleToExplore.experience;
            //todo: przeliczenie xp -> lvl
        }
        else
        {
            level = 1;
            experience = 0;
        }

        levelText.text = "Level: " + level;
        experienceText.text = "Experience: " + experience;
    }
}
