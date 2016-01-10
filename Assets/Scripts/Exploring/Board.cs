﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    public static Board instance;

    public Tile[,] board;
    public int width, height;
    public Point currentPos;

    public Image centerTile;
    public Image upTile;
    public Image downTile;
    public Image rightTile;
    public Image leftTile;
    public Image rightUpTile;
    public Image rightDownTile;
    public Image leftUpTile;
    public Image leftDownTile;

    public Color visitedColor = Color.red;

    public Sprite currentTile;
    public Sprite endTile;
    public Sprite healTile;
    public Sprite emptyTile;
    public Sprite blockTile;
    public Sprite battleTile;

    public AudioClip onMoveSound;

    public string endText;

    public string battleText;

    public string healText;
    public int healMin;
    public int HealMax;

    public struct Point
    {
        public int x;
        public int y;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        if(Connector.wasGeneratedMapToExplore)
        {
            width = Connector.dungeon.width;
            height = Connector.dungeon.height;
            endText = Connector.dungeon.endText;
            generateBoard();
        }
        else if (Connector.wasGeneratedBattleToExplore)
        {
            board = Connector.board;
            width = Connector.dungeon.width;
            height = Connector.dungeon.height;
            endText = Connector.dungeon.endText;
            currentPos.x = Connector.boardPosX;
            currentPos.y = Connector.boardPosY;
        }
        else
        {
            generateBoard();
        }
    }

    void Start()
    {
        updateTiles();
    }

    void Update()
    {
        if(TileOnClick.wasClicked && !OkPanel.instance().okButton.isActiveAndEnabled)
        {
            TileOnClick.wasClicked = false;

            if(TileOnClick.tile.direction == "UP")
            {
                move(0, 1);
            }
            else if(TileOnClick.tile.direction == "DOWN")
            {
                move(0, -1);
            }
            else if (TileOnClick.tile.direction == "RIGHT")
            {
                move(1, 0);
            }
            else if (TileOnClick.tile.direction == "LEFT")
            {
                move(-1, 0);
            }
            Board.instance.updateTiles();
        }
    }

    public void updateTiles()
    {
        centerTile.sprite = currentTile;
        centerTile.color = visitedColor;
        setTileSprite(currentPos.x, currentPos.y + 1, upTile);
        setTileSprite(currentPos.x, currentPos.y - 1, downTile);
        setTileSprite(currentPos.x + 1, currentPos.y, rightTile);
        setTileSprite(currentPos.x - 1, currentPos.y, leftTile);

        setTileSprite(currentPos.x + 1, currentPos.y + 1, rightUpTile);
        setTileSprite(currentPos.x + 1, currentPos.y - 1, rightDownTile);
        setTileSprite(currentPos.x - 1, currentPos.y + 1, leftUpTile);
        setTileSprite(currentPos.x - 1, currentPos.y - 1, leftDownTile);
    }

    private void setTileSprite(int x, int y, Image image)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
        {
            image.enabled = false;
            return;
        }

        image.enabled = true;

        if(board[x, y].wasVisited)
        {
            image.color = visitedColor;
        }
        else
        {
            image.color = Color.white;
        }

        switch (board[x, y].type)
        {
            case Tile.Type.START:
                image.sprite = emptyTile;
                break;

            case Tile.Type.END:
                image.sprite = endTile;
                break;

            case Tile.Type.BATTLE:
                image.sprite = battleTile;
                break;

            case Tile.Type.HEAL:
                image.sprite = healTile;
                break;

            case Tile.Type.BLOCK:
                image.sprite = blockTile;
                break;

            default:
                image.sprite = emptyTile;
                break;
        }
    }

    public class Tile
    {
        public Type type;
        public bool wasVisited;

        public enum Type
        {
            START,
            END,
            EMPTY,
            BLOCK,
            BATTLE,
            HEAL
        }

        public Tile()
        {
            type = Type.EMPTY;
        }
    }

    public void move(int difX, int difY)
    {
        int newX = currentPos.x + difX;
        int newY = currentPos.y + difY;
        if (isMovePossible(newX, newY))
        {
            SoundManager.instance.playOnGui(onMoveSound);
            board[newX, newY].wasVisited = true;
            Debug.Log("nowa pozycja: " + newX + ", " + newY + " : " + board[newX, newY].type);
            currentPos.x = newX;
            currentPos.y = newY;
        }

        switch (board[currentPos.x, currentPos.y].type)
        {
            case Tile.Type.BATTLE:
                onBattle();
                break;

            case Tile.Type.HEAL:
                onHeal();
                break;

            case Tile.Type.END:
                onEnd();
                break;

            default:
                break;
        }
    }

    #region onEnd
    private void onEnd()
    {
        OkPanel.instance().make(InspectorStringAssistant.instance.make(endText), new UnityAction(okEnd));
    }

    private void okEnd()
    {
        Connector.instance.beforeMapFromExplore();
        SceneManager.LoadScene("Map");
    }

    #endregion

    #region onHeal
    private void onHeal()
    {
        PlayerController.canMove = false;
        int heal = Random.Range(healMin, HealMax + 1);
        OkPanel.instance().make(InspectorStringAssistant.instance.make(healText) + "\nHeal: " + heal, new UnityAction(okHeal));
        Heroes.hero1.heal(heal);
        Heroes.hero2.heal(heal);
        board[currentPos.x, currentPos.y].type = Tile.Type.EMPTY;
    }

    private void okHeal()
    {
    }

    #endregion

    #region onBattle
    private void onBattle()
    {
        PlayerController.canMove = false;
        SoundManager.instance.musicSource.Stop();
        OkPanel.instance().make(InspectorStringAssistant.instance.make(battleText), new UnityAction(okBattle));
    }

    private void okBattle()
    {
        Connector.instance.beforeBattleFromExplore();
        SceneManager.LoadScene("FightPrototype");
    }
    #endregion

    private bool isMovePossible(int x, int y)
    {
        if(x < 0 || x >= width || y < 0 || y >= height)
        {
            Debug.Log("za krawedz wyszedl zes ;x");
            return false;
        }

        Tile tile = board[x, y];
        if(tile.type == Tile.Type.BLOCK)
        {
            Debug.Log("no na blocka to nie wejdziesz ziom");
            return false;
        }
        return true;
    }

    private void generateBoard()
    {
        //todo: do zmiany all
        board = new Tile[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                board[i, j] = new Tile();
            }
        }

        int count = width * height;
        int st = Random.Range(0, width);
        board[st, 0].type = Tile.Type.START;
        currentPos.x = st;
        currentPos.y = 0;
        board[currentPos.x, currentPos.y].wasVisited = true;
        Debug.Log("start: " + currentPos.x + " " + currentPos.y);

        int en = Random.Range(0, width);
        board[en, width - 1].type = Tile.Type.END;

        for (int i = 0; i <= 0.5 * count; i++)
        {
            int x = Random.Range(0, width);
            int y = Random.Range(0, height);

            if (board[x, y].type == Tile.Type.EMPTY)
            {
                board[x, y].type = Tile.Type.BATTLE;
            }
        }

        for (int i = height - 1; i >= 0; i--)
        {
            for (int j = 0; j < width; j++)
            {
                if (board[j, i].type == Tile.Type.EMPTY)
                {
                    if (Random.Range(1, 3) == 1)
                    {
                        board[j, i].type = Tile.Type.HEAL;
                    }
                }
            }
        }

        for (int i = height - 1; i >= 0; i--)
        {
            string str = "";
            for (int j = 0; j < width; j++)
            {
                str += board[j, i].type.ToString() + " ";
            }
            Debug.Log(str);
        }
    }
}
