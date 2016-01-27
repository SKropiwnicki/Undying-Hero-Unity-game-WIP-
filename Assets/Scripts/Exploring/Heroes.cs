using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Heroes : MonoBehaviour
{
    public static int level;
    public static int experience;

    public static Hero hero1;
    public static Hero hero2;

    public Text levelText;
    public Text experienceText;

    public string onLvlUpText;

    private void okFunction() { }

    void Awake()
    {
        hero1 = GameObject.Find("Hero1").GetComponent<Hero>();
        hero2 = GameObject.Find("Hero2").GetComponent<Hero>();

        if (Connector.wasGeneratedMapToExplore)
        {
            level = Connector.hs.level;
            //Debug.Log("LEVEL: " + level);
            experience = Connector.hs.experience;
        }
        else if(Connector.wasGeneratedBattleToExplore)
        {
            level = Connector.hs.level;
            //Debug.Log("LEVEL: " + level);
            experience = Connector.hs.experience;
        }
        else
        {
            level = 1;
            experience = 0;
        }
    }

    void Start()
    {
        while(experience >= 100 + (level - 1) * (level * 100))
        {
            level++;

            Connector.hero1.def++;
            Connector.hero2.def++;
            Connector.hero1.maxHealth += 5;
            Connector.hero2.maxHealth += 5;
            Connector.hero1.health += 5;
            Connector.hero2.health += 5;

            Connector.hs.level = level;
            Connector.hero1.levelUpPointsLeft += Connector.hs.onLevelUpPoints;
            Connector.hero2.levelUpPointsLeft += Connector.hs.onLevelUpPoints;
            OkPanel.instance().make(InspectorStringAssistant.instance.make(onLvlUpText), new UnityAction(okFunction));
        }
        if(Connector.hero1.levelUpPointsLeft > 0)
        {
            hero1.pointsLeftText.text = Connector.hero1.levelUpPointsLeft + " points left";
            hero1.setButtonsActive(true);
        }
        if (Connector.hero2.levelUpPointsLeft > 0)
        {
            hero2.pointsLeftText.text = Connector.hero2.levelUpPointsLeft + " points left";
            hero2.setButtonsActive(true);
        }

        levelText.text = "Level: " + level;
        experienceText.text = "Experience: " + experience;
    }
}
