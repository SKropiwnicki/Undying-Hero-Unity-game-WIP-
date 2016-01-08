using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBars : MonoBehaviour
{
    public static HealthBars instance = null;

    public GameObject healthBarPrefab;
    public GameObject healthBarParent;
    public float healthBarYOffset;

    public GameObject shieldBarPrefab;

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
        actor.setHpBar(healthBar);

        int maxHealth = actor.maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = actor.health;
        
        Vector3 worldPos = new Vector3(actor.transform.position.x, f - (ff / 2f) + healthBarYOffset, actor.transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthObject.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);


        GameObject shieldObject = Instantiate(shieldBarPrefab) as GameObject;
        shieldObject.transform.SetParent(healthBarParent.transform, false);
        Slider shieldBar = shieldObject.GetComponentInChildren<Slider>();
        actor.setShieldBar(shieldBar);
        shieldBar.maxValue = maxHealth;
        shieldBar.value = maxHealth;
        Rect hpRect = healthBar.GetComponent<RectTransform>().rect;
        shieldBar.transform.position = new Vector2(healthBar.transform.position.x, healthBar.transform.position.y);
        shieldBar.transform.localPosition = new Vector2(shieldBar.transform.localPosition.x - (hpRect.width / 2), shieldBar.transform.localPosition.y + hpRect.height / 2);
        shieldBar.transform.localScale = new Vector3(0, 1, 0);
    }
}
