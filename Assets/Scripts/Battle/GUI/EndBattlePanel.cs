using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndBattlePanel : MonoBehaviour
{
    public GameObject panel;

    public Text win;
    public Text lose;

    private void onBattleEnd()
    {
        TurnManagement.instance.isBattleFinished = true;
    }

    public void onWin()
    {
        //dopisac ilosc expow
        win.enabled = true;
        onBattleEnd();
    }

    public void onLose()
    {
        //konsekwencje jakie?
        lose.enabled = true;
        onBattleEnd();
    }
}
