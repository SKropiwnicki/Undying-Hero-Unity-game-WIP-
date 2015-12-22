using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextDamage : MonoBehaviour
{
    private Text damageText;

    public float posYChange = 100;
    public float flashSpeed = 1.5f;

    private Vector3 targetPosition;

    void Awake()
    {
        damageText = GetComponent<Text>();
    }

    void Start()
    {
        targetPosition = new Vector3(damageText.transform.localPosition.x, damageText.transform.localPosition.y + posYChange, damageText.transform.localPosition.z);
    }

    void Update()
    {
        damageText.transform.localPosition = Vector3.Lerp(damageText.transform.localPosition, targetPosition, flashSpeed * Time.deltaTime);

        if (damageText.transform.localPosition.y >= targetPosition.y - 10f) Destroy(gameObject);
    }
}
