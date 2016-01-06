using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public int maxHealth;
    public int health; //aktualne hp
    public int initiative;

    public int strength;
    public int dexterity;
    public int intelligence;
    
    public Text healthText;
    public Text strengthText;
    public Text dexterityText;
    public Text intelligenceText;

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
        setPanel();
    }

    private void setPanel()
    {
        healthText.text = "Health: " + health + "/" + maxHealth;
        strengthText.text = "Strength: " + strength;
        dexterityText.text = "Dexterity: " + dexterity;
        intelligenceText.text = "Intelligence: " + intelligence;
    }
}
