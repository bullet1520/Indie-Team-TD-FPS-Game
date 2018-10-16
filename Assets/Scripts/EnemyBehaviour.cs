using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    ///this script controls the enemy AI, telling it where to go, when to attack and when to die.

    
    public GameObject Target;
    public Objective targetScript;
    public GameObject RobotRenderer;
    public ParticleSystem robotDeathExplosion;
    public EnemySpawner enemySpawner;
    public Animator EnemyAnimator;

    [SerializeField]
    private float ownHealth = 10f;
    private Transform target;
    private UnityEngine.AI.NavMeshAgent nav;
    private int hasHit;
    [SerializeField]
    private bool isdead = false;
    private int deathtimer = 100;


    void Awake()
    {
        targetScript = Target.GetComponent<Objective>();
        target = Target.transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void TakeDamage(float amount)
    {
        ownHealth -= amount;
        if (ownHealth <= 0f)
        {
            isdead = true;
        }
    }

    void Die()
    {
        enemySpawner.totalDeadEnemies = enemySpawner.totalDeadEnemies + 1;
        Destroy(gameObject);
    }


    void Update()
    {
        
        
        if (isdead) //what is this mess doing
        { //if the robot has been killed
            nav.enabled = false; //stop moving
            if (deathtimer == 100)
            {
                robotDeathExplosion.Play(); //play an explosion
                RobotRenderer.SetActive(false); //dissappear
                GetComponent<BoxCollider>().enabled = false; //get rid of your collider so you dont interact with physics or raycasts
                deathtimer = deathtimer - 1; //start a timer
            }
            else if (deathtimer == 0)
            { Die(); } //when you are done exploding delete yourself
            else { deathtimer = deathtimer - 1; } //wait till the explosion is about done playing
        }
        else if (!isdead)
        {
            if (Vector3.Distance(transform.position, target.position) < 3f) //if close enough to target
            {
                nav.enabled = false; // stop moving
                EnemyAnimator.SetBool("isattacking", true); //play your attacking animation
                if (hasHit == 0 && !PauseMenuScript.Paused && !isdead) //check a timer to keep it from hitting every frame, rather hit every couple seconds
                {
                    HitTarget();
                    hasHit = 100;
                }
                else if (!PauseMenuScript.Paused && !isdead)
                { hasHit = hasHit - 1; }
            }
            else // if not close enough to target
            {
                nav.enabled = true; // keep moving at the target
                nav.SetDestination(target.position);
                EnemyAnimator.SetBool("isattacking", false); //stop playing an attack animation
            }

            if (hasHit != 0) //the timer still goes down if the target has moved
            { hasHit = hasHit - 1; }
            transform.eulerAngles = new Vector3(0.0f, 90f, 0.0f); //this is only in place because the model from maya keeps coming out facing the wrong way
        }

       
    }

    void HitTarget()
    {
        targetScript.TakeDamage();
    }
  

    

}
