using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Heroes : MonoBehaviour
{
    public static int level;
    public static int experience;

    public Text levelText;
    public Text experienceText;

    public string onLvlUpText;

    private void okFunction() { }

    void Awake()
    {
        if(BattleToExplore.wasGenerated)
        {
            level = BattleToExplore.level;
            experience = BattleToExplore.experience;
        }
        else
        {
            level = 1;
            experience = 0;
        }
    }

    void Start()
    {
        if (experience >= level * (4 + (level * 10)))
        {
            level++;
            ExploreToBattle.hero1.setButtonsActive(true);
            ExploreToBattle.hero2.setButtonsActive(true);
            ExploreToBattle.hero1.levelUpPointsLeft += ExploreToBattle.hero1.onLevelUpPoints;
            ExploreToBattle.hero2.levelUpPointsLeft += ExploreToBattle.hero2.onLevelUpPoints;
            OkPanel.instance().make(InspectorStringAssistant.instance.make(onLvlUpText), new UnityAction(okFunction));
        }

        levelText.text = "Level: " + level;
        experienceText.text = "Experience: " + experience;
    }
}
