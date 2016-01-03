using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Actor : MonoBehaviour
{
    public int maxHealth;
    public int health; //aktualne hp
    public int initiative;
    public float dmgAnimSpeed = 0.9f;

    public GameObject portraitPrefab;

    private Slider healthBar;

    public void SetHpBar(Slider healthBar)
    {
        this.healthBar = healthBar;
    }

    public void Awake()
    {
        health = maxHealth; //TODO: Do ogarniecia, ze w przyszlosci postac moze zaczynac z mniej niz maxhp.
    }

    public void TakeDamage(int damageValue)
    {
        StopAllCoroutines(); //UWAGA! to moze wpływać na inne coroutiny!

        health -= damageValue;
        string text = "- " + damageValue;
        TextSpawner.instance.Spawn(this.transform, text);
        StartCoroutine(damageAnimation());
        
   
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

    IEnumerator damageAnimation()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(1f, 0f, 0f, 1f);
        while (renderer.color.r > 0.1f)
        {
            renderer.color = Color.Lerp(renderer.color, Color.white, dmgAnimSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new  WaitForSeconds(1.5f);
       
    }
}
