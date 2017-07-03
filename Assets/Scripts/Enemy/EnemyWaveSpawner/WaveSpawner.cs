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
    EnemyHealth bearHealth;

    public GameObject Canvas;
    public Text waveText;
    public Text factText;
    public Text continueText;

    public Wave[] waves;
    public static int nextWave = 1;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountdown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;
    private int[] factWaves = { 2, 4, 6, 8, 10, 12, 14};
    private string[] facts =
    {
        "Wanneer je naar de sterren kijkt, kijk je eigenlijk terug in de tijd. Omdat het licht van de sterren heel lang duurt voordat het de aarde heeft bereikt.",
        "Het is een mythe dat NASA miljoenen dollars heeft uitgegeven om een pen uit te vinden die in de ruimte werkt. De russen gebruikte gewoon een potlood.",
        "NASA heeft een planet gevonden genaamd “Waterwereld”. De planeet ligt 40 lichtjaren hier vandaan en bevat exotische materialen zoals heet ijs en super vloeibaar water.",
        "In 2006 heeft NASA toegegeven dat ze niet langer de originele videobanden van de maanlanding hadden omdat ze overgetaped zijn.",
        "Het duurt NASA 32 uur om met het ruimtestation Voyager I te communiceren met een bandbreedte van 115.2 KB per seconden.",
        "De ruimte begint al op 100 km hoogte als auto’s recht omhoog kunnen rijden zou je dus binnen een uur in de ruimte kunnen zijn.",
        "Het woord Astronaut komt van het griekse woord “Astron” wat ster betekent en “Nautes” wat zeiler betekent.",
        "Als je huilt in de ruimte blijven de tranen aan je gezicht plakken.",
    };

    void Start()
    {

        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn Point referenced");
        }
        
        waveCountdown = timeBetweenWaves;

        bearHealth = GetComponent<EnemyHealth>();
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
        continueText.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(2.0f);
        continueText.gameObject.SetActive(true);
        while (!Input.GetKey(KeyCode.Space))
        {
            yield return new WaitForEndOfFrame();
        }

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

        RaycastHit info;
        if (Physics.Raycast(_sp.position, Vector3.down, out info, float.PositiveInfinity))
        {
            Instantiate(_enemy, info.point, Quaternion.identity);
        }
    }
}
