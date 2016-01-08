using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextSpawner : MonoBehaviour
{
    public static TextSpawner instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public Text text;
    public GameObject textHolder;
    public float yOffset;

    public void Spawn(Transform transform, string str)
    {
        text.text = str;

        GameObject damageTextObject = Instantiate(text.gameObject) as GameObject;
        damageTextObject.transform.SetParent(textHolder.transform, false);

        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        damageTextObject.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }

    public void spawnMessage(Transform transform, string str, Color color, int fontSize)
    {
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