using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour {

    public PlayerMovement playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    EnemyTest enemyHealth;
    float basicHealth = 100f;
    float healthMultiplier;
    public int mutexRound = 10;

    public Text text;
    public Text waveIndicator;

    public enum SpawnState { SPAWNING, KILLING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public Transform[] enemies;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public int round = 0;
    // Actual round number
    public int healthRound;

    public float timeBetweenWave = 10f;
    public float countDown;
    private float searchCountdown = 1f;
    private float lastHealth;

    public SpawnState state = SpawnState.COUNTING;

    // Use this for initialization
    void Start () {
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
        countDown = timeBetweenWave;
        lastHealth = 100;
        healthRound = 1;
        text.text = "Round: " + healthRound;
        waveIndicator.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if( state == SpawnState.KILLING)
        {
            // Check if enemies are alive
            if (!EnemyIsAlive())
            {
                // Start round counter
                WaveComplete();
            }
            else
            {
                return;
            }
        }

        if(countDown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                // Start SPawning 
                waveIndicator.enabled = false;
                StartCoroutine(SpawnWave(waves[round]));
            }
        }
        else
        {
            countDown -= Time.deltaTime;
        }

        
    }

    // Updates enemy health if the wave if above the mutex set
    public void WaveComplete()
    {
        Debug.Log("Wave Completed");
        playerHealth.AddHealth(50);
        if (healthRound > mutexRound)
        {
            lastHealth = lastHealth * 1.1f;
        }
        healthRound++;
        text.text = "Round: " + healthRound;

        waveIndicator.enabled = true;
        state = SpawnState.COUNTING;
        countDown = timeBetweenWave;

        if(round + 1 > waves.Length - 1)
        {
            round = 0;
            Debug.Log("Looping waves");
        }
        else if(round == 0)
        {
            return;
        }
        else
        {
            round++;
        }

        if(healthRound > 10)
        {
            round = 0;
        }
        
    }

    // Checks if all enemies are dead
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    // Actually spawns waves of enemies 
    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        // Spawn

        if ((healthRound % 5 == 0 || healthRound % 5 == 5) && healthRound > 15)
        {
            Debug.Log("Spawnig 3rd enemy");
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            var currentEnemy = Instantiate(_wave.enemies[2], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            EnemyHealth set = currentEnemy.GetComponent <EnemyHealth>();
            set.SetHealth(CalculateHealth());
        }

        for (int x = 0; x < _wave.count; x++)
        {
            Spawn(_wave);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.KILLING;

        yield break;
    }

    void Spawn(Wave _wave)
    {
        Debug.Log("Spawning enemy");
        //if(playerHealth.playerHealth <= 0f)
        //{
        //    return;
        //}

        if (round == 0)
        {

            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            var currentEnemy = Instantiate(_wave.enemies[0], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            EnemyHealth set = currentEnemy.GetComponent<EnemyHealth>();
            set.SetHealth(CalculateHealth());

            spawnPointIndex = Random.Range(0, spawnPoints.Length);
            currentEnemy = Instantiate(_wave.enemies[1], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            set = currentEnemy.GetComponent<EnemyHealth>();
            set.SetHealth(CalculateHealth());


        }
        else
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            // currentEnemy = Instantiate(_wave.enemies[0], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            var currentEnemy = Instantiate(_wave.enemies[0], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            EnemyHealth set = currentEnemy.GetComponent<EnemyHealth>();
            set.SetHealth(CalculateHealth());
        }
    }


    // Calculates enemy health based on the current wave
    float CalculateHealth()
    {
        if(healthRound < mutexRound)
        {
            Debug.Log("Setting health to default");
            return 100;
        }
        else
        {
            Debug.Log("Setting health to round based");

            float currentRoundHealth = lastHealth * 1.1f;
            return (currentRoundHealth);
        }

        
    }
}
