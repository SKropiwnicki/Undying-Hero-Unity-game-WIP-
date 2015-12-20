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

    void Start()
    {
        spawnSliders();
    }

    private void spawnSliders()
    {
        foreach (Transform child in transform)
        {
            Actor actor = child.GetComponent<Actor>();
            if (actor != null)
            {
                spawnHitPointsSlider(actor);
            }
            else
            {
                Debug.Log(child.name + " :to nie jest aktor, czy na pewno umiesciles tu poprawny object?");
            }
        }
    }

    private void spawnHitPointsSlider(Actor actor)
    {
        GameObject healthObject = Instantiate(healthBarPrefab) as GameObject;
        healthObject.transform.SetParent(healthBarParent.transform, false);

        Slider healthBar = healthObject.GetComponentInChildren<Slider>();
        actor.setHpBar(healthBar);

        int maxHealth = actor.maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
      
        Vector3 worldPos = new Vector3(actor.transform.position.x, actor.transform.position.y + healthBarYOffset, actor.transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthObject.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }
}
