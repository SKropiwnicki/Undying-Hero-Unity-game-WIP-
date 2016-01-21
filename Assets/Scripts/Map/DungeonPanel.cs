using UnityEngine;
using System.Collections;

public class DungeonPanel : MonoBehaviour
{
    public GameObject parent;

    public int height;
    public int width;
    public int x = 50;
    public int y = 50;

    public AudioClip music;

    void Start()
    {
        string path = "Dungeons";
        Object[] dungeons = Resources.LoadAll(path, typeof(GameObject));

        int cx = 0, cy = 0;
        for(int j = x+width; j < 1920; j+=(width + x))
        {
            cx++;
        }
        for (int j = y+height; j < 1080; j+=(height + y))
        {
            cy++;
        }

        int perW = dungeons.Length / cy;
        for (int i = 0; i < cx; i++)
        {
            for(int j = 0; j < cy; j++)
            {
                if(dungeons.Length <= i * cx + j)
                {
                    if (Connector.hs.level == 1 && Connector.hs.experience == 0)
                    {
                        OnDungClick.onPointerClick();
                    }
                    else
                    {
                        SoundManager.instance.playMusic(music);
                    }
                    return;
                }
                GameObject go = Instantiate(dungeons[i * cx + j], new Vector3(-(perW)*(width/2+x/2) + (i* (width + x)), -y-(j * (height + y)), 0), Quaternion.identity) as GameObject;
                go.transform.SetParent(parent.transform, false);
                if(i == 0 && j == 0)
                    OnDungClick.dungeon = go.GetComponent<DungeonStats>();
            }
        }
    }
}
