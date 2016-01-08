using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Hero : MonoBehaviour
{
    public int maxHealth;
    public int health; //aktualne hp
    public int initiative;

    public int def;

    public int strength;
    public int dexterity;
    public int intelligence;
    
    public Text healthText;
    public Text strengthText;
    public Text dexterityText;
    public Text intelligenceText;

    public Button healthUp;
    public Button strengthUp;
    public Button dexterityUp;
    public Button intelligenceUp;

    public int onLevelUpPoints;
    public int levelUpPointsLeft;

    void Awake()
    {
        if(BattleToExplore.wasGenerated && name == "Hero1")
        {
            health = BattleToExplore.hero1.health;
            if(health < 0)
            {
                health = 100; //todo: 0
            }
        }
        //Hero2, a potem podzial hp jesli ktos ma 0 i koniec gry jesli obaj maja 0
    }

    void Start()
    {
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
        healthText.text = "Health: " + health + "/" + maxHealth;
        strengthText.text = "Strength: " + strength;
        dexterityText.text = "Dexterity: " + dexterity;
        intelligenceText.text = "Intelligence: " + intelligence;
    }

    public void heal(int value)
    {
        health += value;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        healthText.text = "Health: " + health + "/" + maxHealth;
    }

    public void addStat(string str)
    {
        if(str == "Hp")
        {
            maxHealth+=10;
            health+=10;
            healthText.text = "Health: " + health + "/" + maxHealth;
        }
        if (str == "Str")
        {
            strength++;
            strengthText.text = "Strength: " + strength;
        }
        if (str == "Dex")
        {
            dexterity++;
            dexterityText.text = "Dexterity: " + dexterity;
        }
        if (str == "Int")
        {
            intelligence++;
            intelligenceText.text = "Intelligence: " + intelligence;
        }
        levelUpPointsLeft--;
        if(levelUpPointsLeft == 0)
        {
            setButtonsActive(false);
        }
    }
}
