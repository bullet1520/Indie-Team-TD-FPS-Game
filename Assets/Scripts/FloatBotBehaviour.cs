﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatBotBehaviour : MonoBehaviour {
    ///this script controls the enemy AI, telling it where to go, when to attack and when to die.
    public GameObject Target; //this is assigned by the spawner
    public EnemySpawner enemySpawner; //this is assigned by the spawner
    public float ownHealth = 30f; //this is accessed by GunScript
    public float healthInChange = 100f; //this is accessed by Gunscript
    [SerializeField]
    private Material floatbotBaseMaterial;
    [SerializeField]
    private Material floatbot2Material;
    [SerializeField]
    private Material floatbot3Material;
    [SerializeField]
    private Material floatbot4Material;
    [SerializeField]
    private Objective targetScript;
    [SerializeField]
    private GameObject RobotRenderer;
    [SerializeField]
    private ParticleSystem robotDeathExplosion;
    [SerializeField]
    private Animator EnemyAnimator;
    [SerializeField]
    private float damageDealt = 5;
    private Rigidbody myRigidbody;
    private float ownHealthChangePercentage;
    private Transform target;
    private UnityEngine.AI.NavMeshAgent nav;
    private int hasHit;
    [SerializeField]
    private bool isdead = false;
    private int deathtimer = 200;
    private Vector3 cannonImpactPoint;
    private bool isAttacking = false;

    void Awake()
    {
        targetScript = Target.GetComponent<Objective>();
        target = Target.transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    public void TakeExplosiveDamage(float amount, Vector3 explosionPoint) //this needs to take a position
    {
        ownHealth -= amount;
        if (ownHealth <= 0f)
        {
            isdead = true;
        }
        cannonImpactPoint = explosionPoint;
        CycleMaterialUponHit();
    }

    void Die()
    {
        enemySpawner.totalDeadEnemies = enemySpawner.totalDeadEnemies + 1;
        Destroy(gameObject);
    }


    void Update()
    {

        ownHealthChangePercentage = (ownHealth * 100) / 30;
        if (healthInChange > ownHealthChangePercentage)
        {
            healthInChange = healthInChange - 1;
        }
        if (isdead) //what is this mess doing
        { //if the robot has been killed
            nav.enabled = false; //stop moving
            EnemyAnimator.SetBool("isDead", true);
            myRigidbody.constraints = RigidbodyConstraints.None;
            if (deathtimer == 200)
            {
                robotDeathExplosion.Play(); //play an explosion
                if (cannonImpactPoint != null)
                    myRigidbody.AddExplosionForce(400, cannonImpactPoint, 3.0f, 4.0f);
                //RobotRenderer.SetActive(false); //dissappear
                //GetComponent<BoxCollider>().enabled = false; //get rid of your collider so you dont interact with physics or raycasts
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
                    isAttacking = true;
                    nav.enabled = false; // stop moving
                    EnemyAnimator.SetBool("isAttacking", true); //play your attacking animation
                    if (hasHit == 0 && !PauseMenuScript.Paused && !isdead) //check a timer to keep it from hitting every frame, rather hit every couple seconds
                    {
                        HitTarget();
                        hasHit = 100;
                    }
                    else if (!PauseMenuScript.Paused && !isdead)
                    { hasHit = hasHit - 1; }
                }
                else if (Vector3.Distance(transform.position, target.position) >= 3f && !isAttacking) // if not close enough to target
                {
                    nav.enabled = true; // keep moving at the target
                    nav.SetDestination(target.position);
                    EnemyAnimator.SetBool("isAttacking", false); //stop playing an attack animation
                }
                else if (Vector3.Distance(transform.position, target.position) >= 3f && isAttacking)
                {
                    nav.enabled = false;
                    EnemyAnimator.SetBool("isAttacking", true);
                    if (hasHit == 0 && !PauseMenuScript.Paused && !isdead)
                    {
                        HitTarget();
                        hasHit = 100;
                    }
                    else if (!PauseMenuScript.Paused && !isdead)
                    { hasHit = hasHit - 1; }
                }

                if (hasHit != 0) //the timer still goes down if the target has moved
                { hasHit = hasHit - 1; }

            
        }
    }
    void HitTarget()
    {
        targetScript.TakeDamage(damageDealt);
    }

    void CycleMaterialUponHit()
    {
        if (ownHealth == 20f)
        {
            RobotRenderer.GetComponent<Renderer>().material = floatbot2Material;
        }
        else if (ownHealth == 10f)
        {
            RobotRenderer.GetComponent<Renderer>().material = floatbot3Material;
        }
        else if (ownHealth == 0)
        {
            RobotRenderer.GetComponent<Renderer>().material = floatbot4Material;
        }
    }

}
