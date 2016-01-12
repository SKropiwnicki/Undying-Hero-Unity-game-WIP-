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
            Debug.Log("LEVEL: " + level);
            experience = Connector.hs.experience;
        }
        else if(Connector.wasGeneratedBattleToExplore)
        {
            level = Connector.hs.level;
            Debug.Log("LEVEL: " + level);
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
        while(experience >= level * (40 + (level * 10)))
        {
            Debug.Log(level + " " + experience);
            level++;
            Connector.hs.level = level;
            hero1.setButtonsActive(true);
            hero2.setButtonsActive(true);
            hero1.levelUpPointsLeft += hero1.onLevelUpPoints;
            hero2.levelUpPointsLeft += hero2.onLevelUpPoints;
            OkPanel.instance().make(InspectorStringAssistant.instance.make(onLvlUpText), new UnityAction(okFunction));
        }

        levelText.text = "Level: " + level;
        experienceText.text = "Experience: " + experience;
    }
}
