using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RunGame : MonoBehaviour
{
	void Start ()
    {
        SceneManager.LoadScene("Map");
    }
}
