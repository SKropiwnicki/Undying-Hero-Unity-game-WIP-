using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndBattlePanel : MonoBehaviour
{
    public static EndBattlePanel instance;

    public GameObject panel;
    private Canvas parent;

    public Text win;
    public Text lose;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
    }

    //kolejnosc dowolna
    public void init()
    {
        parent = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    private void onBattleEnd()
    {
        SoundManager.instance.musicSource.Stop();
        GameObject go = Instantiate(panel) as GameObject;
        go.transform.SetParent(parent.transform, false);
        TurnManagement.instance.isBattleFinished = true;
        
        Connector.instance.beforeExploreFromBattle();
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

    public void destroy()
    {
        gameObject.SetActive(false);
        Destroy(instance.gameObject);
    }
}
