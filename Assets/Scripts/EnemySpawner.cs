using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public Transform[] spawnPoints;

    public GameObject objectivepoint;

    public float chosenspawnrate = 500f;
    [SerializeField]
    private float currentspawntime = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (currentspawntime <= 0)
        {
            Spawn();
            currentspawntime = chosenspawnrate;
        }
        else
        {
            currentspawntime = currentspawntime - 1;
        }
	}


    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        EnemyBehaviour spawnedScript = enemy.GetComponent<EnemyBehaviour>();
        
            spawnedScript.Target = objectivepoint;
    }
}
