using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleLoadingScreen : MonoBehaviour
{
    private Slider levelProgressBar;

    void Awake()
    {
        levelProgressBar = GetComponent<Slider>();
    }

    void Start()
    {
        StartCoroutine("newGame");
    }

    private IEnumerator newGame()
    {
        yield return new WaitForSeconds(1);
        AsyncOperation async = SceneManager.LoadSceneAsync("FightPrototype");

        while (!async.isDone)
        {
            Debug.Log(async.progress);
            levelProgressBar.value = async.progress;

            yield return null;
        }
    }
}
