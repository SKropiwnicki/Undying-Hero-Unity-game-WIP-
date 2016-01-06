using UnityEngine;
using System.Collections;

public class ExploreToBattle : MonoBehaviour
{
    public static ExploreToBattle instance;

    public GameObject gameObjectWithHero1Script;
    public GameObject gameObjectWithHero2Script;

    public static int level, experience;
    public static Hero hero1;
    public static Hero hero2;

    public static Board.Tile[,] board;
    public static int posX, posY;

    public static bool wasGenerated;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.transform);
    }

    public void beforeBattle()
    {
        if (hero1 == null)
        {
            hero1 = gameObjectWithHero1Script.GetComponent<Hero>();
        }
        if (hero2 == null)
        {
            hero2 = gameObjectWithHero2Script.GetComponent<Hero>();
        }
        board = Board.instance.board;
        posX = Board.instance.currentTile.x;
        posY = Board.instance.currentTile.y;
        Board.instance.board[posX, posY].type = Board.Tile.Type.EMPTY;
        level = Heroes.level;
        experience = Heroes.experience;
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
