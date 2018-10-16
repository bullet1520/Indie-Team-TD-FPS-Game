using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    //this is one of the more complex and pivotal scripts
    //this script controls the random spawning of enemies between spawnpoints and tracks how many enemies are left to spawn/kill
    //it calls upon the WinLoseScript Win() function when there are no more enemies to kill
    

    public GameObject enemy;
    public Transform[] spawnPoints;
    public WinLoseScript winLoseScript;
    public GameObject objectivepoint;
    public Text enemyCounterText;

    public int totalEnemiestoStart; //all of these are only public because i found it useful to tweak them during runtime
    public int totalEnemies = 100;
    public int totalDeadEnemies = 0;
    public int foesSinceLastWave = 0;
    public int totalEnemiesToBeKilled;

    public float chosenspawnrate = 250f;
    [SerializeField]
    private float currentspawntime = 0f;
	// Use this for initialization
	void Start () {
        totalEnemiestoStart = totalEnemies;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        totalEnemiesToBeKilled = totalEnemiestoStart - totalDeadEnemies;
        enemyCounterText.text = "Enemies Remaining: " + totalEnemiesToBeKilled; //this displays to the player how many enemies there are left to kill
        if (currentspawntime <= 0 && totalEnemies >= 1)
        { //this says when to spawn an enemy
            SpawnEnemy();
            currentspawntime = chosenspawnrate;
        }
        else if (currentspawntime > 0)
        { //this makes the spawn timer tick down
            currentspawntime = currentspawntime - 1;
        }

        if (foesSinceLastWave == 5)
        { //this makes the spawn timer shorten every 5 enemies killed
            DecreaseSpawnRate();
        }

        if (totalEnemies == 0 && totalDeadEnemies == totalEnemiestoStart)
        { //this tells WinLoseScript to tell the player they won when all enemies are dead.
            winLoseScript.Win();
        }

	}

    void DecreaseSpawnRate()
    {
        chosenspawnrate = chosenspawnrate - 25;
        foesSinceLastWave = 0;
    }

    void SpawnEnemy()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        EnemyBehaviour spawnedScript = enemy.GetComponent<EnemyBehaviour>();
        
            spawnedScript.Target = objectivepoint;
        spawnedScript.enemySpawner = GetComponent<EnemySpawner>();
        totalEnemies = totalEnemies - 1;
        foesSinceLastWave = foesSinceLastWave + 1;
    }
}
