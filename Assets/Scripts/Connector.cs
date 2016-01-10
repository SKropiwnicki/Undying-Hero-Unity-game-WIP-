using UnityEngine;
using System.Collections;

public class Connector : MonoBehaviour
{
    public static Connector instance;

    public GameObject gameObjectWithHero1Stats;
    public GameObject gameObjectWithHero2Stats;

    public static HeroStats hero1;
    public static HeroStats hero2;

    public static DungeonStats dungeon;

    public static bool wasGeneratedMapToExplore;
    public static bool wasGeneratedExploreToMap;
    public static bool wasGeneratedBattleToExplore;

    ///////////////////

    public static Board.Tile[,] board;
    public static int boardPosX, boardPosY;

    public static bool wasGeneratedExploreToBattle;

    ///////////////////

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.transform);

        if (hero1 == null)
        {
            hero1 = gameObjectWithHero1Stats.GetComponent<HeroStats>();
        }
        if (hero2 == null)
        {
            hero2 = gameObjectWithHero2Stats.GetComponent<HeroStats>();
        }
    }

    public void beforeExploreFromMap()
    {
        dungeon = OnDungClick.dungeon;
        wasGeneratedMapToExplore = true;
        wasGeneratedBattleToExplore = false;
        wasGeneratedExploreToBattle = false;
        wasGeneratedExploreToMap = false;
    }

    public void beforeMapFromExplore()
    {
        wasGeneratedExploreToMap = true;
        wasGeneratedMapToExplore = false;
        wasGeneratedBattleToExplore = false;
        wasGeneratedExploreToBattle = false;
    }

    public void beforeBattleFromExplore()
    {
        board = Board.instance.board;
        boardPosX = Board.instance.currentPos.x;
        boardPosY = Board.instance.currentPos.y;
        Board.instance.board[boardPosX, boardPosY].type = Board.Tile.Type.EMPTY;
        HeroStats.level = Heroes.level;
        HeroStats.experience = Heroes.experience;

        wasGeneratedExploreToBattle = true;
        wasGeneratedExploreToMap = false;
        wasGeneratedMapToExplore = false;
        wasGeneratedBattleToExplore = false;
    }

    public void beforeExploreFromBattle()
    {
        wasGeneratedBattleToExplore = true;
        wasGeneratedExploreToBattle = false;
        wasGeneratedExploreToMap = false;
        wasGeneratedMapToExplore = false;
    }
}
