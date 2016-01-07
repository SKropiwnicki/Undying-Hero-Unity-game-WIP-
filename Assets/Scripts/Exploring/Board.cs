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
    public int weight, height;
    public Point currentTile;

    public Image centerTile;
    public Image upTile;
    public Image downTile;
    public Image rightTile;
    public Image leftTile;

    public Sprite emptyTile;
    public Sprite blockTile;
    public Sprite battleTile;

    public AudioClip onMoveSound;

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
        
        if (!BattleToExplore.wasGenerated)
        {
            generateBoard();
        }
        else
        {
            board = BattleToExplore.board;
            currentTile.x = BattleToExplore.posX;
            currentTile.y = BattleToExplore.posY;
        }
        updateTiles();
    }

    public void updateTiles()
    {
        setTileSprite(currentTile.x, currentTile.y, centerTile);
        setTileSprite(currentTile.x, currentTile.y + 1, upTile);
        setTileSprite(currentTile.x, currentTile.y - 1, downTile);
        setTileSprite(currentTile.x + 1, currentTile.y, rightTile);
        setTileSprite(currentTile.x - 1, currentTile.y, leftTile);
    }

    private void setTileSprite(int x, int y, Image image)
    {
        if (x < 0 || x >= weight || y < 0 || y >= height)
        {
            image.enabled = false;
            return;
        }

        image.enabled = true;

        switch (board[x, y].type)
        {
            case Tile.Type.START: //todo: del
                image.sprite = blockTile;
                break;

            case Tile.Type.END: //todo: del
                image.sprite = blockTile;
                break;

            case Tile.Type.BATTLE:
                image.sprite = battleTile;
                break;

            //case Tile.Type.BLOCK:
            //    return blockTile;

            default:
                image.sprite = emptyTile;
                break;
        }
    }

    private void generateBoard()
    {
        //todo: do zmiany all
        board = new Tile[weight, height];
        for (int i = 0; i < weight; i++)
        {
            for (int j = 0; j < height; j++)
            {
                board[i, j] = new Tile();
            }
        }

        int count = weight * height;
        int st = Random.Range(0, weight);
        board[st, 0].type = Tile.Type.START;
        currentTile.x = st;
        currentTile.y = 0;
        Debug.Log("start: " + currentTile.x + " " + currentTile.y);

        int en = Random.Range(0, weight);
        board[en, weight - 1].type = Tile.Type.END;

        for (int i = 0; i <= 0.5 * count; i++)
        {
            int x = Random.Range(0, weight);
            int y = Random.Range(0, height);

            if (board[x, y].type == Tile.Type.EMPTY)
            {
                board[x, y].type = Tile.Type.BATTLE;
            }
        }

        for (int i = height - 1; i >= 0; i--)
        {
            string str = "";
            for (int j = 0; j < weight; j++)
            {
                str += board[j, i].type.ToString() + " ";
            }
            Debug.Log(str);
        }
    }

    public class Tile
    {
        public Type type;
        public enum Type
        {
            START,
            END,
            EMPTY,
            BLOCK,
            BATTLE //, EVENT?
        }

        public Tile()
        {
            type = Type.EMPTY;
        }
    }

    public void move(int difX, int difY)
    {
        int newX = currentTile.x + difX;
        int newY = currentTile.y + difY;
        if (isMovePossible(newX, newY))
        {
            SoundManager.instance.playOnGui(onMoveSound);
            Debug.Log("nowa pozycja: " + newX + ", " + newY + " : " + board[newX, newY].type);
            currentTile.x = newX;
            currentTile.y = newY;
        }

        switch (board[currentTile.x, currentTile.y].type)
        {
            case Tile.Type.BATTLE:
                onBattleTile();
                break;

            default:
                break;
        }
    }

    #region onBattle
    private void onBattleTile()
    {
        PlayerController.canMove = false;
        SoundManager.instance.musicSource.Stop();
        OkPanel.instance().make("O życie walcz śmiertelniku!", new UnityAction(okFunction));
    }

    private void okFunction()
    {
        ExploreToBattle.instance.beforeBattle();
        ExploreToBattle.wasGenerated = true;
        SceneManager.LoadScene("FightPrototype");
    }
    #endregion

    private bool isMovePossible(int x, int y)
    {
        if(x < 0 || x >= weight || y < 0 || y >= height)
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
}
