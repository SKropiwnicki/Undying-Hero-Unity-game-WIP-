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
