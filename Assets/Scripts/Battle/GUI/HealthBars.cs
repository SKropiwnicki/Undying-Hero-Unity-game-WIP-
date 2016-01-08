using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBars : MonoBehaviour
{
    public static HealthBars instance = null;

    public GameObject healthBarPrefab;
    public GameObject healthBarParent;
    public float healthBarYOffset;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void spawnSliders()
    {
        Actor act = null;
        foreach (Actor actor in Actors.instance.get())
        {
            if(act == null)
            {
                act = actor;
            }
            spawnHitPointsSlider(actor, act.transform.position.y, act.spriteRenderer.bounds.size.y);
        }
    }

    private void spawnHitPointsSlider(Actor actor, float f, float ff)
    {
        GameObject healthObject = Instantiate(healthBarPrefab) as GameObject;
        healthObject.transform.SetParent(healthBarParent.transform, false);

        Slider healthBar = healthObject.GetComponentInChildren<Slider>();
        actor.SetHpBar(healthBar);

        int maxHealth = actor.maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = actor.health;
        
        Vector3 worldPos = new Vector3(actor.transform.position.x, f - (ff / 2f) + healthBarYOffset, actor.transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthObject.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }
}
