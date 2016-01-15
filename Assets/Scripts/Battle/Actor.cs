﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Actor : MonoBehaviour
{
    #region Variables
    public string displayName;
    public int maxHealth;
    public int health; //aktualne hp
    public int shield = 0;
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
    public bool shouldCalculateStats = true; //Niektorzy przeciwnicy sa tacy, ze nie chcemy wyliczac im statystyk wedlug zasad, gdyz oni omijaja zasady, np maja strasznie dużo maxAP i jeden epic skill
    
    public IAI ai;

    public string[] skillsNames;
    public List<Skill> skills;

    public GameObject skillPrefab;

    public GameObject portraitPrefab;

    //Zmiana na potrzebe skilli
    private Slider healthBar;
    private Slider shieldBar;
    public Text displayNameText;
    public int level = -1;

    public float dmgAnimSpeed = 0.9f;
    [HideInInspector]
    public SpriteRenderer spriteRenderer; //todo: del?

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

    public void setShieldBar(Slider slider)
    {
        this.shieldBar = slider;
    }

    public void setHpBar(Slider healthBar)
    {
        this.healthBar = healthBar;
    }

    public void setDisplayNameText(Text text)
    {
        displayNameText = text;
    }

    public void loadHeroStats()
    {
        HeroStats hero = null;

        if(name == "Hero1")
        {
            hero = Connector.hero1;
        }
        else if(name == "Hero2")
        {
            hero = Connector.hero2;
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
        //Debug.Log("Nazywam sie " + name +" a moje skille to: ");
        foreach (Skill skill in skills)
        {
            //Debug.Log("Skill: " + skill.name);
        }
    }


    public void onAttackEfx()
    {
        SoundManager.instance.playSingle(attackSound);
        animator.SetTrigger("Attack");
    }

    //musi byc publiczna zeby metody mogly używać
    public void changeShield(int value)
    {
        //Debug.Log("Przed hitem shielda jest: " + shield);
        shield += value;
        //Debug.Log("po hicie shielda jest : " + shield);
        if (shield <= 0)
        {
            int ShieldDamage = value - shield;  // ilosc obrazen + ujemna tarcza
            //Debug.Log(" policzone ile w shielda poszło " + ShieldDamage);
            string txt = "" + ShieldDamage + " shield";
            TextSpawner.instance.spawn(this.transform, txt, Color.blue, 40);

            int hpDamage = shield;
            if (hpDamage < 0) hpDamage *= -1;  //zamiana znaku na dodatni na potrzebny TakeDamage
            
            shield = 0;
            TakeDamage(hpDamage, false, true, true);
            
            
        }
        else
        {
            if (value > 0)
            {
                string txt = "+" + value + " shield";
                TextSpawner.instance.spawn(this.transform, txt, Color.blue, 40);
            }
            else
            {
                string txt = value + " shield";
                TextSpawner.instance.spawn(this.transform, txt, Color.blue, 40);
            }

        }


        shieldBar.maxValue = value;
        shieldBar.value = value;
        float ratio = shield / (maxHealth * 1.0f);
        shieldBar.transform.localScale = new Vector3(ratio, 1, 0);
    }


    public void Heal(int healValue)
    {
        health += healValue;
        int healed = healValue;
        if (health > maxHealth)
        {
            healed -= (health - maxHealth);
            health = maxHealth;
        }
        string txt = "+" + healed + " hp";

        TextSpawner.instance.spawn(this.transform, txt, Color.green, 40);

        healthBar.value = health;

        if (name == "Hero1") Connector.hero1.health = health;
        if (name == "Hero2") Connector.hero2.health = health;

    }

    public void APchange(int AP)
    {
        currentAP += AP;
        int apChange = AP;
        if (currentAP > maxAP)
        {
            apChange -= (currentAP - maxAP);
            currentAP = maxAP;
        }
        //Bugi łapiemy
        if (currentAP < 0) Debug.LogError("NIEPOPRAWNA ZMIANA AP na" + currentAP + " zmieniono ap o: "+ apChange);
        
        string txt = "";
        if (apChange > 0)
        {
            txt = "+" + apChange + " AP";
        }
        else
        {
            txt = apChange + " AP";
        }


        TextSpawner.instance.spawn(this.transform, txt, Color.yellow, 40);

    }

    public void TakeDamage(int damageValue)
    {
        TakeDamage(damageValue, false, false, false);
    }

    public void TakeDamage(int damageValue, bool isCriticalHit)
    {
        TakeDamage(damageValue, isCriticalHit, false, false);
    }

    public void TakeDamage(int damageValue, bool isCriticalHit, bool ignoreDef, bool ignoreShield)
    {
        //StopAllCoroutines(); //UWAGA! to moze wpływać na inne coroutiny!
        //spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        int actualDamage = damageValue;
        if (!ignoreDef)
        { 
            actualDamage = (damageValue - def);
            if (actualDamage <= 0) actualDamage = 1;
        }
        if (shield > 0 && !ignoreShield)
        {
            changeShield(-actualDamage);
        }
        else
        {
            health -= actualDamage;
            if (name == "Hero1") Connector.hero1.health = health;
            if (name == "Hero2") Connector.hero2.health = health;


            string text = "";
            if (isCriticalHit) text += "CRIT! ";
            text += "- " + actualDamage;
            TextSpawner.instance.spawn(this.transform, text);
            //StartCoroutine(damageAnimation());

            if (health <= 0)
            {
                StartCoroutine("onDeath");
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

    IEnumerator onDeath()
    {
        this.gameObject.SetActive(false);
        SoundManager.instance.playOnDeath(onDeathSound);
        Connector.hs.experience += experience;

        Actors.instance.remove(this);

        destroyActorThings();

        TurnManagement.instance.updatePortraitsPosition();
        TurnManagement.instance.updatePortraitBorderPosition();
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    public void destroyActorThings()
    {
        Destroy(healthBar.transform.gameObject); //transform.gameObject.SetActive(false); -> jesli bedziemy miec wskrzeszanie mozna uzywac zamiennie
        Destroy(shieldBar.transform.gameObject);
        Destroy(portraitPrefab.transform.gameObject);
        Destroy(displayNameText.transform.gameObject);
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
        if (shouldCalculateStats)  // Czy wyliczac staty (czasami nie chcemy)
        {
            //strength
            attackPower = 1 + Mathf.FloorToInt(strength*3/4 + dexterity /3);
            if (attackPower < 0) attackPower = 0;
            maxAP = 7 + Mathf.FloorToInt(strength / 2);
            //dexterity
            initiative = Mathf.FloorToInt(dexterity / 3);
            startingAP = 1 + Mathf.FloorToInt(dexterity / 5);
            critChance = 0 + Mathf.FloorToInt(dexterity / 4);
            if (critChance > 50) critChance = 50;

            //intelligence
            perTurnAp = 1 + (intelligence / 10);

            controlMaxAP();
        }
    }

    public void controlMaxAP ()
    {
        if (currentAP > maxAP) currentAP = maxAP;
    }
    
    public virtual void AI()
    {
        if (ai != null) ai.specialAI();
    }
}
