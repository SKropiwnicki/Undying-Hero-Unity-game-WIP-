using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;


public class Menu : MonoBehaviour
{
    public static Menu instance = null;

    public GameObject panel;
    public static List<SaveLabel> sl;

    public AudioClip music;
    public AudioClip click;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SoundManager.instance.playMusic(music);
    }

    public void playFunc()
    {
        SoundManager.instance.playOnGui(click);
        sl = new List<SaveLabel>();
        for(int i = 0; i < 3; i++)
        {
            sl.Add(new SaveLabel());
        }

        string path = Application.persistentDataPath;
        BinaryFormatter bf = new BinaryFormatter();

        for (int i = 1; i <= 3; i++)
        {
            if (!Directory.Exists(path + "/Player" + i) || !File.Exists(path + "/Player" + i + "/label.dat"))
            {
                Directory.CreateDirectory(path + "/Player" + i);
                FileStream file = File.Create(path + "/Player" + i + "/label.dat");
                bf.Serialize(file, sl[i-1]);
                file.Close();
            }
            else
            {
                FileStream file = File.Open(Application.persistentDataPath + "/Player" + i + "/label.dat", FileMode.Open);
                SaveLabel slx = (SaveLabel)bf.Deserialize(file);
                file.Close();
                sl[i - 1] = slx;
            }
        }

        panel.SetActive(true);
    }

    public void quitFunc()
    {
        SoundManager.instance.playOnGui(click);
        Application.Quit();
    }
}
