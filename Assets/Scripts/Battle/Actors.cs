using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actors : MonoBehaviour
{
    public static Actors instance;

    private List<Actor> actors;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void init()
    {
        actors = new List<Actor>();

        //Uwazac na tego foreacha w przyszłości, narazie działa i wygląda ok.
        foreach (Transform child in transform)
        {
            Actor actor = child.GetComponent<Actor>();
            if (actor != null)
            {
                if (Connector.wasGeneratedExploreToBattle)
                {
                    actor.loadHeroStats();
                  
                }

                actors.Add(actor);
            }
            else
            {
                //Debug.Log(child.name + " :to nie jest aktor, czy na pewno umiesciles tu poprawny object?");
            }
        }
    }

    public List<Actor> get()
    {
        if(actors == null)
        {
            //Debug.Log("Inicjalizacja aktorow jeszcze nie nastapila! Kolejnosc wykonywania skryptow jest bardzo wazna!");
        }
        return actors;
    }

    public void remove(Actor actor)
    {
        actors.Remove(actor);
        TurnManagement.instance.actors.Remove(actor);
    }
}
