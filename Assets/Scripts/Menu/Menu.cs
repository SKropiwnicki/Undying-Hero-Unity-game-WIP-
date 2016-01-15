using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;


public class Menu : MonoBehaviour
{
    public GameObject panel;
    public static List<SaveLabel> sl;

    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    public void playFunc()
    {
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
        Application.Quit();
    }
}
