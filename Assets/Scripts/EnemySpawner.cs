using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public Transform[] spawnPoints;
    public WinLoseScript winLoseScript;
    public GameObject objectivepoint;
    public Text enemyCounterText;
    public int totalEnemiestoStart;
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
        enemyCounterText.text = "Enemies Remaining: " + totalEnemiesToBeKilled;
        if (currentspawntime <= 0 && totalEnemies >= 1)
        {
            Spawn();
            currentspawntime = chosenspawnrate;
        }
        else if (currentspawntime > 0)
        {
            currentspawntime = currentspawntime - 1;
        }

        if (foesSinceLastWave == 5)
        {
            chosenspawnrate = chosenspawnrate - 25;
            foesSinceLastWave = 0;
        }

        if (totalEnemies == 0 && totalDeadEnemies == totalEnemiestoStart)
        {
            winLoseScript.Win();
        }

	}


    void Spawn()
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
