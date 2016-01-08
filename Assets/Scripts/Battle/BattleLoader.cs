using UnityEngine;
using System.Collections;

public class BattleLoader : MonoBehaviour
{
    public static bool loaded;

    public GameObject ActorsParent;
    public GameObject Hero1Prefab;
    public GameObject Hero2Prefab;

    public int enemiesLevel;

    public int minEnemies;
    public int maxEnemies;

    public AudioClip music;

    void Awake()
    {
        //dane z ExploreToBattle tutaj bedzie mozna przekazac
    }

    void Start()
    {
        StartCoroutine("start");
    }

    private void spawnHeroes()
    {
        Hero1Prefab = Instantiate(Hero1Prefab, new Vector3(2.5f, 1.25f, 0), Quaternion.identity) as GameObject;
        Hero1Prefab.name = "Hero1";
        Hero1Prefab.transform.SetParent(ActorsParent.transform, false);

        /*wykomentowac gdy Hero2 w koncu bedzie
        Hero2Prefab = Instantiate(Hero2Prefab, new Vector3(5f, 1.25f, 0), Quaternion.identity) as GameObject;
        Hero2Prefab.name = "Hero2";
        Hero2Prefab.transform.SetParent(ActorsParent.transform, false);*/
    }

    private void spawnEnemies()
    {
        string path = "Enemies/" + enemiesLevel;
        Object[] enemies = Resources.LoadAll(path, typeof(GameObject));

        int count = Random.Range(minEnemies, maxEnemies + 1);
        for (int i = 0; i < count; i++)
        {
            int numb = Random.Range(0, enemies.Length);
            GameObject enemy = enemies[numb] as GameObject;

            enemy = Instantiate(enemy, new Vector3(((i + 1) * -2.7f + 0.75f), 0, 0), Quaternion.identity) as GameObject;
            enemy.transform.SetParent(ActorsParent.transform, false);
        }
    }

    private IEnumerator start()
    {
        spawnEnemies();
        spawnHeroes();


        yield return new WaitForEndOfFrame();

        ButtonManager.instance.init();

        yield return new WaitForEndOfFrame();

        AllSkills.instance.init();

        yield return new WaitForEndOfFrame();

        Actors.instance.init();

        yield return new WaitForEndOfFrame();

        HealthBars.instance.spawnSliders();

        yield return new WaitForEndOfFrame();

        foreach (Actor actor in Actors.instance.get())
        {
            actor.skillLoader();
        }

        yield return new WaitForEndOfFrame();

        StartCoroutine(TurnManagement.instance.initTurnManagement());

        yield return new WaitForEndOfFrame();

        EndBattlePanel.instance.init();        

        yield return new WaitForEndOfFrame();

        BattleToExplore.instance.init();

        yield return new WaitForEndOfFrame();

        loaded = true;

        yield return new WaitForEndOfFrame();

        SoundManager.instance.playMusic(music);
    }
}
