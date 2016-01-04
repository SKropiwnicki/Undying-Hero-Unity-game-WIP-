using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleLoadingScreen : MonoBehaviour
{
    public static bool loaded;

    public GameObject ActorsParent;
    public GameObject Hero1Prefab;
    public GameObject Hero2Prefab;

    public int enemiesLevel = 1;

    public int minEnemies;
    public int maxEnemies;

    void Start()
    {
        StartCoroutine("start");
    }

    private IEnumerator start()
    {
        Object[] enemies = Resources.LoadAll("Enemies/1", typeof(GameObject));

        int count = Random.Range(minEnemies, maxEnemies + 1);
        for(int i = 0; i < count; i++)
        {
            int numb = Random.Range(0, enemies.Length);
            GameObject enemy = enemies[numb] as GameObject;

            enemy = Instantiate(enemy, new Vector3(((i + 1) * -3f + 0.75f), 0.05f, 0), Quaternion.identity) as GameObject;
            enemy.transform.SetParent(ActorsParent.transform, false);
        }

        Hero1Prefab = Instantiate(Hero1Prefab, new Vector3(2.5f, 1.25f, 0), Quaternion.identity) as GameObject;
        Hero1Prefab.name = "Hero1";
        Hero1Prefab.transform.SetParent(ActorsParent.transform, false);

        yield return new WaitForEndOfFrame();

        Actors.instance.init();

        yield return new WaitForEndOfFrame();

        TurnManagement.instance.initTurnManagement();

        yield return new WaitForEndOfFrame();

        HealthBars.instance.spawnSliders();

        yield return new WaitForEndOfFrame();

        loaded = true;
    }
}
