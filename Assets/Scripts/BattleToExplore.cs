using UnityEngine;
using System.Collections;

public class BattleToExplore : MonoBehaviour
{
    //public static bool wasGenerated;

    void Awake()
    {
        DontDestroyOnLoad(this.transform);
    }
}
