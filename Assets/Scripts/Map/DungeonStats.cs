using UnityEngine;
using System.Collections.Generic;

public class DungeonStats : MonoBehaviour
{
    public int width, height;
    public int enemiesLevel;
    public string startText, endText;
    public int type;

    [SerializeField]
    [TextArea(8, 8)]
    public List<string> startingCutscene;

    [SerializeField]
    [TextArea(8, 8)]
    public List<string> endingCutscene;
}
