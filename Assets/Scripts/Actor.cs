using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Actor : MonoBehaviour
{
    public int maxHealth;
    public int health; //aktualne hp
    public int initiative;

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
            string text = "- " + damageValue;
            TextSpawner.instance.Spawn(this.transform, text);
        }
        else
        {
            //dead state ;]
            Debug.Log(name + " is dead");
            Actors.get().Remove(this);
            Destroy(healthBar.transform.gameObject); //transform.gameObject.SetActive(false); -> jesli bedziemy miec wskrzeszanie mozna uzywac zamiennie
            Destroy(this.gameObject);
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
