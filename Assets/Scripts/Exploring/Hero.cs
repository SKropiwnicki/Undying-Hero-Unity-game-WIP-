using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Hero : MonoBehaviour
{
    public HeroStats hero;

    public Text healthText;
    public Text strengthText;
    public Text dexterityText;
    public Text intelligenceText;

    public Button healthUp;
    public Button strengthUp;
    public Button dexterityUp;
    public Button intelligenceUp;

    //public int onLevelUpPoints;
    //public int levelUpPointsLeft;

    void Awake()
    {
        if (Connector.wasGeneratedMapToExplore)
        {
            if (name == "Hero1")
            {
                hero = Connector.hero1;
            }
            else if (name == "Hero2")
            {
                hero = Connector.hero2;
            }
        }
        else if (Connector.wasGeneratedBattleToExplore)
        {
            if (name == "Hero1")
            {
                hero = Connector.hero1;
                if (Connector.hero1.health < 0)
                {
                    Connector.hero1.health = 100; //todo: 0
                }
            }
            else if (name == "Hero2")
            {
                hero = Connector.hero2;
                return;
                //Hero2, a potem podzial hp jesli ktos ma 0 i koniec gry jesli obaj maja 0
            }
        }
    }

    void Start()
    {
        if(Connector.wasGeneratedMapToExplore)
        {
            hero.health = hero.maxHealth;
        }
        healthUp.onClick.AddListener( () => addStat("Hp"));
        strengthUp.onClick.AddListener( () => addStat("Str"));
        dexterityUp.onClick.AddListener( () => addStat("Dex"));
        intelligenceUp.onClick.AddListener( () => addStat("Int"));
        setPanel();
    }

    public void setButtonsActive(bool b)
    {
        healthUp.gameObject.SetActive(b);
        strengthUp.gameObject.SetActive(b);
        dexterityUp.gameObject.SetActive(b);
        intelligenceUp.gameObject.SetActive(b);
    }

    private void setPanel()
    {
        healthText.text = "Health: " + hero.health + "/" + hero.maxHealth;
        strengthText.text = "Strength: " + hero.strength;
        dexterityText.text = "Dexterity: " + hero.dexterity;
        intelligenceText.text = "Intelligence: " + hero.intelligence;
    }

    public void heal(int value)
    {
        hero.health += value;
        if(hero.health > hero.maxHealth)
        {
            hero.health = hero.maxHealth;
        }
        healthText.text = "Health: " + hero.health + "/" + hero.maxHealth;
    }

    public void addStat(string str)
    {
        if(str == "Hp")
        {
            hero.maxHealth +=10;
            hero.health +=10;
            healthText.text = "Health: " + hero.health + "/" + hero.maxHealth;
        }
        if (str == "Str")
        {
            hero.strength++;
            strengthText.text = "Strength: " + hero.strength;
        }
        if (str == "Dex")
        {
            hero.dexterity++;
            dexterityText.text = "Dexterity: " + hero.dexterity;
        }
        if (str == "Int")
        {
            hero.intelligence++;
            intelligenceText.text = "Intelligence: " + hero.intelligence;
        }

        if (name == "Hero1")
        {
            Connector.hero1.levelUpPointsLeft--;
            if (Connector.hero1.levelUpPointsLeft == 0)
            {
                setButtonsActive(false);
            }
        }
        else if (name == "Hero2")
        {
            Connector.hero2.levelUpPointsLeft--;
            if (Connector.hero2.levelUpPointsLeft == 0)
            {
                setButtonsActive(false);
            }
        }
    }
}
