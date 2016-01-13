using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class TextSpawner : MonoBehaviour
{
    public static TextSpawner instance = null;

    private List<tQueue> list;

    public class tQueue
    {
        public Transform transform;
        public Queue<UnityAction> queue;

        public tQueue(Transform transform, UnityAction ua)
        {
            this.transform = transform;
            queue = new Queue<UnityAction>();
            queue.Enqueue(ua);
        }
    }

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
        list = new List<tQueue>();
    }

    public Text text;
    public GameObject textHolder;
    public float yOffset;

    private IEnumerator op(Queue<UnityAction> queue)
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
        yield return new WaitForSeconds(0.1f);
    }

    private void checkAndDo(Transform transform, UnityAction ua)
    {
        foreach (tQueue tq in list)
        {
            if (transform == tq.transform)
            {
                tq.queue.Enqueue(ua);
                return;
            }
        }
        tQueue tqx = new tQueue(transform, ua);
        list.Add(tqx);
        StartCoroutine(op(tqx.queue));
    }

    public void spawn(Transform transform, string str)
    {
        UnityAction ua = () => { spawnx(transform, str, Color.red, 40); };
        checkAndDo(transform, ua);
    }

    public void spawn(Transform transform, string str, Color color)
    {
        UnityAction ua = () => { spawnx(transform, str, color, 40); };
        checkAndDo(transform, ua);
    }

    public void spawn(Transform transform, string str, Color color, int fontSize)
    {
        UnityAction ua = () => { spawnx(transform, str, color, fontSize); };
        checkAndDo(transform, ua);
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