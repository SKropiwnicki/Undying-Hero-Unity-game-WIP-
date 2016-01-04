using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {

    public static ButtonManager instance;
    public GameObject AutoAttackButton;

    public float buttonOffsetX = 2.0f;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void spawnButtons(Actor actor)
    {
        int i = 0;
        foreach (Skill skill in actor.skills)
        {
            if (skill.name == "AutoAttack")
            {
                GameObject button = Instantiate(actor.portraitPrefab, new Vector3(i * buttonOffsetX, 10, 0), Quaternion.identity) as GameObject;
                button.transform.SetParent(this.gameObject.transform, false);
            }
                i++;
            
        }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
    Buttony maja prefaby  w którym jest przypisane wywołanie skryptu z targetting i tam też nazwa danego skilla.
    Buttony sie beda instantiate'owac przy zmianie tury, a metody do tego będą stąd wywołowany
    Dwa warunki: If actor jest controllable  i ze skille nie moga przekraczac miejsca

    */
}
