using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManagement : MonoBehaviour
{
    public static TurnManagement instance;

    private Actor currentActor;
    private List<Actor> actors;

    public GameObject pointerPrefab;
    public GameObject pointerParent;
    public float pointerYOffset = 2.5f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void initTurnManagement()
    {
        StartCoroutine(spawnPointer()); //wczytywanie prefabow trwa te pare milisekund - bolesnie sie o tym przekonalem
        initRound();
    }

    public void initRound()
    {
        actors = new List<Actor>(Actors.get());

        actors.Sort(delegate (Actor x, Actor y)
        {
            return y.initiative.CompareTo(x.initiative);
        });

        currentActor = actors[0];
    }

    public void testNextTurn() //potrzebne dla buttonka - nie uruchamia statycznych funkcji //todo: del
    {
        nextTurn();
    }

    public void nextTurn()
    {
        int currentIndex = actors.IndexOf(currentActor);

        if (currentIndex + 1 >= actors.Count)
        {
            Debug.Log("Pora na kolejna rundke ;)");

            initRound();
        }
        else
        {
            currentActor = actors[currentIndex + 1];
        }
        setPointerPosition();
        Debug.Log("Tura " + currentActor.name + " inicjatywa: " + currentActor.initiative);
    }

    private IEnumerator spawnPointer()
    {
        pointerPrefab = Instantiate(pointerPrefab) as GameObject;
        pointerPrefab.transform.SetParent(pointerParent.transform, false);
        yield return new WaitForEndOfFrame();
        setPointerPosition();
        Debug.Log("Tura " + currentActor.name + " inicjatywa: " + currentActor.initiative);
    }

    private void setPointerPosition()
    {
        float pointerXOffset = currentActor.transform.position.x;

        Vector3 worldPos = new Vector3(pointerXOffset, pointerYOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        pointerPrefab.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }
}
