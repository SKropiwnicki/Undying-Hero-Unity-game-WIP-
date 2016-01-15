using UnityEngine;
using UnityEngine.UI;

public class DisplayNames : MonoBehaviour
{
    public static DisplayNames instance = null;

    public Text textPrefab;
    public GameObject textParent;
    public float yOffset;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void spawnNames()
    {
        Actor act = null;
        foreach (Actor actor in Actors.instance.get())
        {
            if (act == null)
            {
                act = actor;
            }
            spawnDisplayNameText(actor, act.transform.position.y);
        }
    }

    private void spawnDisplayNameText(Actor actor, float y)
    {
        Text displayNameText = Instantiate(textPrefab) as Text;
        displayNameText.text = actor.displayName;
        if (actor.level > 0)
        {
            displayNameText.text = displayNameText.text + " lvl: " + actor.level;
        }
        displayNameText.transform.SetParent(textParent.transform, false);

        Vector3 worldPos = new Vector3(actor.transform.position.x, y + yOffset, actor.transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        displayNameText.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

        actor.setDisplayNameText(displayNameText);
    }
}
