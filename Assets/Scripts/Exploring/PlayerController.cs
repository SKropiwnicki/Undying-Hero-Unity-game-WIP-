using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Board.instance.move(0, 1);
            Board.instance.updateTiles();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Board.instance.move(0, -1);
            Board.instance.updateTiles();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Board.instance.move(1, 0);
            Board.instance.updateTiles();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Board.instance.move(-1, 0);
            Board.instance.updateTiles();
        }
    }
}
