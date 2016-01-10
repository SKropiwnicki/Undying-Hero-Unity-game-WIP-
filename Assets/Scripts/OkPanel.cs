using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class OkPanel : MonoBehaviour
{
    private static OkPanel okPanel;

    public Text txt;
    public Button okButton;
    public GameObject okPanelObject;

    void Awake()
    {
        okPanelObject.SetActive(false);
    }

    public static OkPanel instance()
    {
        if(!okPanel)
        {
            okPanel = FindObjectOfType(typeof(OkPanel)) as OkPanel;
        }
        return okPanel;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (okButton.isActiveAndEnabled)
            {
                okButton.onClick.Invoke();
                closePanel();
            }
        }
    }

    public void make(string txt, UnityAction okEvent)
    {
        okPanelObject.SetActive(true);

        okButton.onClick.RemoveAllListeners();
        okButton.onClick.AddListener(okEvent);
        okButton.onClick.AddListener(closePanel);

        this.txt.text = txt;

        okButton.gameObject.SetActive(true);
    }

    private void closePanel()
    {
        PlayerController.canMove = true;
        TileOnClick.wasClicked = false;
        okPanelObject.SetActive(false);
    }
}
