using UnityEngine;
using System.Collections;

public class BattleToExplore : MonoBehaviour
{
    public static BattleToExplore instance;

    public static bool wasGenerated;

    public static Actor hero1;
    public static Actor hero2;

    public static int health;

    public static Board.Tile[,] board;
    public static int posX, posY;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.transform);
    }

    //moze byc ostatnie
    public void init()
    {
        if (hero1 == null)
        {
            hero1 = GameObject.Find("Hero1").GetComponent<Actor>();
        }
    }

    public void beforeExplore()
    {
        board = ExploreToBattle.board;
        posX = ExploreToBattle.posX;
        posY = ExploreToBattle.posY;
        wasGenerated = true;
    }

    public void destroy()
    {
        Destroy(instance.gameObject);
    }
}
