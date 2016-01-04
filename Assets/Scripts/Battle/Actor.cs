using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    public int maxHealth;
    public int health; //aktualne hp
    public int initiative;
    public int attackPower;

    public bool isControllable;

    [HideInInspector]
    public List<Skill> skills;

    public GameObject skillPrefab;

    public GameObject portraitPrefab;

    private Slider healthBar;

    public float dmgAnimSpeed = 0.9f;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public Animator animator;

    public void SetHpBar(Slider healthBar)
    {
        this.healthBar = healthBar;
    }

    public void Awake()
    {
        health = maxHealth; //TODO: Do ogarniecia, ze w przyszlosci postac moze zaczynac z mniej niz maxhp.
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        
    }
    public void Start()
    {
        skills = new List<Skill>();
        AutoAttack atakauto = new AutoAttack();
        skills.Add(atakauto);
    }

    public void onAttackAnimation()
    {
        animator.SetTrigger("Attack");
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
            onDeath();
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

    private void onDeath()
    {
        Debug.Log(name + " is dead");
        Actors.get().Remove(this);
        Destroy(healthBar.transform.gameObject); //transform.gameObject.SetActive(false); -> jesli bedziemy miec wskrzeszanie mozna uzywac zamiennie
        Destroy(this.gameObject);
    }

    IEnumerator damageAnimation()
    {
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
        while (spriteRenderer.color.r > 0.1f)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, dmgAnimSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new  WaitForSeconds(1.5f);
    }
}
