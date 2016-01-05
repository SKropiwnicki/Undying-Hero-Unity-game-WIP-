using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndBattlePanel : MonoBehaviour
{
    public static EndBattlePanel instance;

    public GameObject panel;
    public GameObject parent;

    public Text win;
    public Text lose;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
    }

    //kolejnosc dowolna
    public void init() { }

    private void onBattleEnd()
    {
        panel = Instantiate(panel) as GameObject;
        panel.transform.SetParent(parent.transform, false);
        TurnManagement.instance.isBattleFinished = true;
    }

    public void onWin()
    {
        //dopisac ilosc expow
        win.gameObject.SetActive(true);
        onBattleEnd();
    }

    public void onLose()
    {
        //konsekwencje jakie?
        lose.gameObject.SetActive(true);
        onBattleEnd();
    }
}
