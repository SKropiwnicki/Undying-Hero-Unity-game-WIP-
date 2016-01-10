using UnityEngine;
using System.Collections;

public class DungeonPanel : MonoBehaviour
{
    public GameObject parent;

    public int height;

    void Start()
    {
        string path = "Dungeons";
        Object[] dungeons = Resources.LoadAll(path, typeof(GameObject));

        int i = 0;
        foreach(GameObject dung in dungeons)
        {
            GameObject go = Instantiate(dung, new Vector3(0, 350 - (i * height), 0), Quaternion.identity) as GameObject;
            go.transform.SetParent(parent.transform, false);
            i++;
        }
    }
}
