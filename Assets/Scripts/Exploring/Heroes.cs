using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Heroes : MonoBehaviour
{
    public int level;
    public int experience;

    public Text levelText;
    public Text experienceText;

    void Awake()
    {
        levelText.text = "Level: " + level;
        experienceText.text = "Experience: " + experience;
    }
}
