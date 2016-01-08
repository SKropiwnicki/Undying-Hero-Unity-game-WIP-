using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    #region Variables
    public int maxHealth;
    public int health; //aktualne hp
    public int initiative;
    public int experience;
    public int attackPower;
    public int critChance;

    public int def;

    //CoreStats
    public int strength;
    public int dexterity;
    public int intelligence;


    //AP
    public int startingAP;
    public int currentAP;
    public int maxAP;
    public int perTurnAp;    

    public bool isControllable;
    
    public IAI ai;

    public string[] skillsNames;
    public List<Skill> skills;

    public GameObject skillPrefab;

    public GameObject portraitPrefab;

    private Slider healthBar;

    public float dmgAnimSpeed = 0.9f;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    public Animator animator;

    public AudioClip attackSound;
    public AudioClip onDeathSound;
    #endregion

    void Awake()
    {
        health = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        skills = new List<Skill>();
        skills.Add(new AutoAttack());
    }

    public void SetHpBar(Slider healthBar)
    {
        this.healthBar = healthBar;
    }

    public void loadHeroStats()
    {
        Hero hero = null;

        if(name == "Hero1")
        {
            hero = ExploreToBattle.hero1;
        }
        else if(name == "Hero2")
        {
            hero = ExploreToBattle.hero2;
        }
        else
        {
            calculateStats();
            currentAP = startingAP;
            return;
        }

        maxHealth = hero.maxHealth;
        health = hero.health;
        initiative = hero.initiative;
        strength = hero.strength;
        dexterity = hero.dexterity;
        intelligence = hero.intelligence;
        def = hero.def;

        //OBSŁUGA AP
        calculateStats();
        currentAP = startingAP - perTurnAp;
    }

    public void skillLoader()
    {
        foreach (string skillName in skillsNames)
        { 
            foreach (Skill skill in AllSkills.instance.allSkillList)
            {
                if (skillName == skill.name)
                {
                    skills.Add(skill);
                }
            }
        }
        Debug.Log("Nazywam sie " + name +" a moje skille to: ");
        foreach (Skill skill in skills)
        {
            Debug.Log("Skill: " + skill.name);
        }
    }


    public void onAttackEfx()
    {
        SoundManager.instance.playSingle(attackSound);
        animator.SetTrigger("Attack");
    }

    public void TakeDamage(int damageValue, bool isCriticalHit)
    {
        StopAllCoroutines(); //UWAGA! to moze wpływać na inne coroutiny!
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

        int actualDamage = (damageValue - def);
        if (actualDamage <= 0) actualDamage = 1;
        health -= actualDamage;

        string text = "";
        if (isCriticalHit) text += "CRIT! ";
        text +="- " + actualDamage;

        TextSpawner.instance.Spawn(this.transform, text);
        //StartCoroutine(damageAnimation());
   
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
        SoundManager.instance.playOnDeath(onDeathSound);
        ExploreToBattle.experience += experience;

        Actors.instance.remove(this);

        destroyActorThings();

        TurnManagement.instance.updatePortraitsPosition();
        TurnManagement.instance.updatePortraitBorderPosition();

        Destroy(this.gameObject);
    }

    public void destroyActorThings()
    {
        Destroy(healthBar.transform.gameObject); //transform.gameObject.SetActive(false); -> jesli bedziemy miec wskrzeszanie mozna uzywac zamiennie
        Destroy(portraitPrefab.transform.gameObject);
    }

    IEnumerator damageAnimation()
    {
        Color defaultColor = spriteRenderer.color;
        spriteRenderer.color = new Color(1f, 0f, 0f, 1f);
        while (spriteRenderer.color.r > 0.1f)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, defaultColor, dmgAnimSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new  WaitForSeconds(1.5f);
    }

    public void calculateStats()
    {
        //strength
        attackPower = 1 + strength;
        if (attackPower < 0) attackPower = 0;
        maxAP = 5 + Mathf.FloorToInt(strength/2);
        //dexterity
        initiative = Mathf.FloorToInt(dexterity / 3);
        startingAP = 1 + Mathf.FloorToInt(dexterity / 5);
        critChance = 0 + Mathf.FloorToInt(dexterity / 4);
        if (critChance > 50) critChance = 50;

        //intelligence
        perTurnAp = 1 + (intelligence / 10);

        controlMaxAP();
    }

    public void controlMaxAP ()
    {
        if (currentAP > maxAP) currentAP = maxAP;
    }
    
    public virtual void AI()
    {
        ai.specialAI();
    }
}
