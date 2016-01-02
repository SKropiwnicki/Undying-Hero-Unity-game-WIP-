using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Actor : MonoBehaviour
{
    public int maxHealth;
    public int health; //aktualne hp
    public int initiative;

    public GameObject portraitPrefab;

    private Slider healthBar;

    public void setHpBar(Slider healthBar)
    {
        this.healthBar = healthBar;
    }

    public void Awake()
    {
        health = maxHealth; //TODO: Do ogarniecia, ze w przyszlosci postac moze zaczynac z mniej niz maxhp.
    }

    public void TakeDamage(int damageValue)
    {

        health -= damageValue;
        string text = "- " + damageValue;
        TextSpawner.instance.Spawn(this.transform, text);

        if (health <= 0)
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
