using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;

public class SaveSlot : MonoBehaviour
{
    public Button playButton;
    public Button delButton;
    public Button newGameButton;

    public Text text;

    public int count;

    void Start()
    {
        setText();

        UnityAction ua = new UnityAction(newGameFunc);
        newGameButton.onClick.AddListener(ua);

        ua = new UnityAction(delFunc);
        delButton.onClick.AddListener(ua);

        ua = new UnityAction(playFunc);
        playButton.onClick.AddListener(ua);
    }

    private void setText()
    {
        int level = Menu.sl[count - 1].level;
        if (level <= 0)
        {
            text.text = "Player " + count;
            newGameButton.gameObject.SetActive(true);
        }
        else
        {
            text.text = "Player " + count + "\n\nLevel: " + level;
            playButton.gameObject.SetActive(true);
            delButton.gameObject.SetActive(true);
        }
    }

    public void newGameFunc()
    {
        playFunc();
    }

    public void delFunc()
    {
        FileUtil.DeleteFileOrDirectory(Application.persistentDataPath + "/Player" + count);
        playButton.gameObject.SetActive(false);
        delButton.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(true);
        text.text = "Player " + count;
        newGameButton.gameObject.SetActive(true);
    }

    public void playFunc()
    {
        PlayerPrefs.SetInt("profileNumber", count);
        SceneManager.LoadScene("START");
    }
}
