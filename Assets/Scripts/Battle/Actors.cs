using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Actors : MonoBehaviour
{
    private static List<Actor> actors;

	void Awake ()
    {
        actors = new List<Actor>();

        foreach (Transform child in transform)
        {
            Actor actor = child.GetComponent<Actor>();
            if (actor != null)
            {
                actors.Add(actor);
            }
            else
            {
                Debug.Log(child.name + " :to nie jest aktor, czy na pewno umiesciles tu poprawny object?");
            }
        }

        TurnManagement.instance.initTurnManagement();
    }

    public static List<Actor> get()
    {
        if(actors == null)
        {
            Debug.Log("Inicjalizacja aktorow jeszcze nie nastapila! Kolejnosc wykonywania skryptow jest bardzo wazna!");
        }
        return actors;
    }
}
