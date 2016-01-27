using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Connector : MonoBehaviour
{
    public static Connector instance;

    public static HeroesStats hs;
    public static HeroStats hero1;
    public static HeroStats hero2;

    public static DungeonStats dungeon;

    public static bool wasGeneratedMapToExplore;
    public static bool wasGeneratedExploreToMap;
    public static bool wasGeneratedBattleToExplore;
    public static bool wasGeneratedExploreToBattle;

    public static Board.Tile[,] board;
    public static int boardPosX, boardPosY;

    public static int profileNumber;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this.transform);

        if(!wasGeneratedExploreToMap)
        {
            profileNumber = PlayerPrefs.GetInt("profileNumber");
            load();
        }
    }

    private void save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Player" + profileNumber;
        Debug.Log(path);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        FileStream file = File.Create(path + "/hero1.dat");
        bf.Serialize(file, hero1);
        file.Close();
        
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        file = File.Create(path + "/hero2.dat");
        bf.Serialize(file, hero2);
        file.Close();

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        file = File.Create(path + "/heroes.dat");
        bf.Serialize(file, hs);
        file.Close();

        SaveLabel label = new SaveLabel();
        label.level = hs.level;
        file = File.Create(path + "/label.dat");
        bf.Serialize(file, label);
        file.Close();

        //Debug.Log("AUTOSAVE done");
    }

    private void load()
    {
        string profile = "/Player" + profileNumber;
        Debug.Log(Application.persistentDataPath + profile + "/hero1.dat");
        if((File.Exists(Application.persistentDataPath + profile + "/hero1.dat")) && (File.Exists(Application.persistentDataPath + profile + "/hero2.dat")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + profile + "/hero1.dat", FileMode.Open);
            HeroStats h1 = (HeroStats)bf.Deserialize(file);
            file.Close();

            file = File.Open(Application.persistentDataPath + profile + "/hero2.dat", FileMode.Open);
            HeroStats h2 = (HeroStats)bf.Deserialize(file);
            file.Close();

            file = File.Open(Application.persistentDataPath + profile + "/heroes.dat", FileMode.Open);
            HeroesStats h = (HeroesStats)bf.Deserialize(file);
            file.Close();

            hero1 = h1;
            hero2 = h2;
            hs = h;

            //Debug.Log("LOAD done");
        }
        else
        {
            hero1 = new HeroStats();
            hero2 = new HeroStats();
            hs = new HeroesStats();

            //Debug.Log("Plikow ni ma, tworzenie nowych bohaterow...");
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
        save();
    }

    public void beforeBattleFromExplore()
    {
        board = Board.instance.board;
        boardPosX = Board.instance.currentPos.x;
        boardPosY = Board.instance.currentPos.y;
        Board.instance.board[boardPosX, boardPosY].type = Board.Tile.Type.EMPTY;
        hs.level = Heroes.level;
        hs.experience = Heroes.experience;

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
