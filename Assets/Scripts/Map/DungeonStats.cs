using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class DungeonStats : MonoBehaviour
{
    public int width, height;
    public int enemiesLevel;
    public string startText, endText;
    public int type;

    public Sprite startingImage;
    public Sprite endingImage;

    [SerializeField]
    [TextArea(8, 8)]
    public List<string> startingCutscene;

    [SerializeField]
    [TextArea(8, 8)]
    public List<string> endingCutscene;
}
