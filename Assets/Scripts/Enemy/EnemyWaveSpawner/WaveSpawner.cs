using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public GameObject Canvas;
    public Text waveText;
    public Text factText;

    public Wave[] waves;
    public static int nextWave = 1;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    private int[] factWaves = { 2, 5, 8, 10 };
    private string[] facts =
    {
        "fact 1",
        "fact 2",
        "fact 3",
        "fact 4",
    };

    void Start()
    {

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn Point referenced");
        }
        
        waveCountdown = timeBetweenWaves;
    }

    public void Update()
    {
        waveText.text = "Wave: " + nextWave;
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                //Begin a new round
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;

            //Debug.Log(waveCountdown);
        }
    }

    private IEnumerator Freeze(int wave)
    {
        int randomfactint = Random.Range(0, facts.Length);
        while (facts[randomfactint] != "")
        {
            randomfactint = Random.Range(0, facts.Length);
            yield return new WaitForEndOfFrame() ;
            if (facts[randomfactint] != "")
                break;
        }

        string fact = string.Copy(facts[randomfactint]); //facts[randomfactint];
        facts[randomfactint] = "";

        Debug.Log(fact);

        Time.timeScale = 0f;
        Canvas.gameObject.SetActive(true);
        factText.text = "" + fact;
        yield return new WaitForSecondsRealtime(5.0f);
        Canvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void WaveCompleted()
     {
        Debug.Log("Wave Completed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;


        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves COmPLETE!");
        }
        else
        {
            nextWave++;

            if (factWaves.Contains(nextWave))
            {
                StartCoroutine(Freeze(nextWave));
            }
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemey(_wave.enemy);
            yield return new WaitForSeconds(3f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemey (Transform _enemy)
    {
        Debug.Log("Spawning Enemy:" + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}
