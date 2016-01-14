using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Button continueButton;
    public Button quitButton;

    public void continueFunc()
    {
        SceneManager.LoadScene("START");
    }

    public void quitFunc()
    {
        Application.Quit();
    }
}
