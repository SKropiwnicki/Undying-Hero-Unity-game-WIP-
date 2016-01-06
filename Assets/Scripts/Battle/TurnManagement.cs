using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManagement : MonoBehaviour
{
    public static TurnManagement instance;

    private Actor currentActor;
    public List<Actor> actors;

    public GameObject pointerPrefab;
    public GameObject pointerParent;
    public float pointerYOffset = 2.5f;

    public GameObject portraitBorderPrefab;
    public GameObject portraitsParent;
    public float portraitXOffset; //szerokosc portretu/ramki

    public bool isBattleFinished;

    public float beforeEnemyActionTime;
    public float afterEnemyActionTurn;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public bool winCheck()
    {
        int enemiesCount = 0;
        int controllableCount = 0;

        foreach(Actor actor in actors)
        {
            if(actor.isControllable)
            {
                controllableCount++;
            }
            else
            {
                enemiesCount++;
            }
        }

        if(controllableCount == 0)
        {
            EndBattlePanel.instance.onLose();
            return true;
        }
        else if(enemiesCount == 0)
        {
            EndBattlePanel.instance.onWin();
            return true;
        }
        return false;
    }

    public IEnumerator initTurnManagement()
    {
        initRound();
        StartCoroutine(spawnPointer()); //wczytywanie prefabow trwa te pare milisekund - bolesnie sie o tym przekonalem
        spawnPortraits();
        spawnPortraitBorder();
        updatePortraitBorderPosition();

        ButtonManager.instance.spawnButtons(currentActor);

        while(!BattleLoader.loaded) //nie jest potrzebne w tym wypadku "bo dziala", ale jest mocno zalecane
        {
            yield return new WaitForEndOfFrame();
        }
        TurnManagement.instance.onTurnAction();
    }

    public void onTurnAction()
    {
        ButtonManager.instance.spawnButtons(currentActor);
        if (currentActor.isControllable)
        {
            
        }
        else
        {
            StartCoroutine("w8CurrentActorAi");
        }
    }

    private IEnumerator w8CurrentActorAi()
    {
        Debug.Log("Wywoluje AI dla " + currentActor.name);
        yield return new WaitForSeconds(beforeEnemyActionTime);
        currentActor.AI();
    }

    private void initRound()
    {
        actors = new List<Actor>(Actors.instance.get());
        
        if (actors.Count != 0)
        {
        actors.Sort(delegate (Actor x, Actor y)
        {
            return y.initiative.CompareTo(x.initiative);
        });
        }

        currentActor = actors[0];
    }

    public void nextTurnCor()
    {
        StartCoroutine("nextTurn");
    }


    private IEnumerator nextTurn()
    {
        yield return new WaitForSeconds(afterEnemyActionTurn);

        bool finished = winCheck();

        if (finished)
        {
              yield return new WaitForEndOfFrame();
        }
      

        int currentIndex = actors.IndexOf(currentActor);
        int nextIndex = currentIndex + 1;

        if (nextIndex >= actors.Count)
        {
            Debug.Log("Pora na kolejna rundke ;)");

            initRound();
            updatePortraitsPosition();
        }
        else
        {
            currentActor = actors[nextIndex];
        }
        setPointerPosition();
        updatePortraitBorderPosition();
        Debug.Log("Tura " + currentActor.name + " inicjatywa: " + currentActor.initiative);

        currentActor.calculateStats();
      

        if (!finished) onTurnAction();
    }

    public bool isThisCurrentActor(Actor actor)
    {
        if (currentActor == actor) { return true; }
        else return false;
    }

    public Actor getCurrentActor()
    {
        return currentActor;
    }







    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>

    private IEnumerator spawnPointer()
    {
        pointerPrefab = Instantiate(pointerPrefab) as GameObject;
        pointerPrefab.transform.SetParent(pointerParent.transform, false);
        yield return new WaitForEndOfFrame();
        setPointerPosition();
        //Debug.Log("Tura " + currentActor.name + " inicjatywa: " + currentActor.initiative);
    }

    private void setPointerPosition()
    {
        float pointerXOffset = currentActor.transform.position.x;

        Vector3 worldPos = new Vector3(pointerXOffset, pointerYOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        pointerPrefab.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
    }

    private void spawnPortraits()
    {
        int i = 0;
        foreach (Actor actor in actors)
        {
            actor.portraitPrefab = Instantiate(actor.portraitPrefab, new Vector3(i * portraitXOffset, 0, 0), Quaternion.identity) as GameObject;
            actor.portraitPrefab.transform.SetParent(portraitsParent.transform, false);

            i++;
        }
    }

    private void spawnPortraitBorder()
    {
        portraitBorderPrefab = Instantiate(portraitBorderPrefab) as GameObject;
        portraitBorderPrefab.transform.SetParent(portraitsParent.transform, false);
    }

    public void updatePortraitBorderPosition()
    {
        int currentIndex = actors.IndexOf(currentActor);
        portraitBorderPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentIndex * portraitXOffset, 0f);
    }

    public void updatePortraitsPosition()
    {
        int i = 0;
        foreach (Actor actor in actors)
        {
            actor.portraitPrefab.GetComponent<RectTransform>().anchoredPosition = new Vector2(i * portraitXOffset, 0f);

            i++;
        }
    }
}
