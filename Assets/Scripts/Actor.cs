using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Actor : MonoBehaviour
{
    public int maxHealth;
    public int health; //aktualne hp

    private Slider healthBar;

    public void setHpBar(Slider healthBar)
    {
        this.healthBar = healthBar;
    }

    public void Awake()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damageValue)
    {
        if (health > 0)
        {
            health -= damageValue;
        }
        else
        {
            //todo: smierc
            Debug.Log(name + " is dead");
        }

        if (healthBar != null)
        {
            healthBar.value = health;
        }
        else
        {
            Debug.Log("Actor.cs, slider problem");
        }
    }
}
