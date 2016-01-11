using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class TextSpawner : MonoBehaviour
{
    public static TextSpawner instance = null;

    private Queue<UnityAction> queue;

    public float timeBeforeNextSpawn = 0.5f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        queue = new Queue<UnityAction>();
        StartCoroutine("op");
    }

    public Text text;
    public GameObject textHolder;
    public float yOffset;

    private IEnumerator op()
    {
        while (!TurnManagement.instance.isBattleFinished)
        {
            if (queue.Count == 0)
            {
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                queue.Dequeue().Invoke();
                yield return new WaitForSeconds(timeBeforeNextSpawn);
            }
        }
    }

    public void spawn(Transform transform, string str)
    {
        UnityAction ua = () => { spawnx(transform, str); };
        queue.Enqueue(ua);
    }

    public void spawn(Transform transform, string str, Color color, int fontSize)
    {
        UnityAction ua = () => { spawnx(transform, str, color, fontSize); };
        queue.Enqueue(ua);
    }

    private void spawnx(Transform transform, string str)
    {
        if (!transform) return;
        text.text = str;

        GameObject damageTextObject = Instantiate(text.gameObject) as GameObject;
        damageTextObject.transform.SetParent(textHolder.transform, false);

        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        damageTextObject.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }

    private void spawnx(Transform transform, string str, Color color, int fontSize)
    {
        if (!transform) return;
        text.text = str;

        GameObject damageTextObject = Instantiate(text.gameObject) as GameObject;
        Text tx = damageTextObject.GetComponent<Text>();
        tx.fontSize = fontSize;
        tx.color = color;
        damageTextObject.transform.SetParent(textHolder.transform, false);

        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        damageTextObject.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }
}