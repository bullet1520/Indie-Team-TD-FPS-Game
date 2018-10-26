using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOBehaviour : MonoBehaviour {

    public Transform objectivePoint;
    public float health = 40f;

    [SerializeField]
    private float speed = 15f;
    [SerializeField]
    private float delaytimer = 50f;
    private float deathDelayTimer = 300f;
    private Vector3 aerialObjective;
    [SerializeField]
    private bool atAerialObjective = false;
    [SerializeField]
    private ParticleSystem UFODeathParticles;
    [SerializeField]
    private GameObject myOwnCockpit;
    
	
	void Awake () {
        aerialObjective = new Vector3(objectivePoint.position.x, 20, objectivePoint.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        if (health == 0)
        {
            Die();
        }
        else
        {
            Move();
        }
       

	}

    private void Move()
    {
        
        if (!atAerialObjective && this.transform.position != aerialObjective)
        {
            if (delaytimer >= 1)
            { delaytimer = delaytimer - 1; }
            else
            {
                float step = speed * Time.deltaTime;

                transform.position = Vector3.MoveTowards(transform.position, aerialObjective, step);
            }
            
        }
        else if (!atAerialObjective && this.transform.position == aerialObjective)
        {
            atAerialObjective = true;
            speed = 7f;
            delaytimer = 200;
        }
        else if (atAerialObjective && delaytimer >= 1)
        { delaytimer = delaytimer - 1; }
        else if (atAerialObjective && delaytimer < 1)
        {
            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, objectivePoint.position, step);
        }
    }

    private void Die()
    {
        if (deathDelayTimer == 0)
        {
            Destroy(gameObject);
        }
        else if (deathDelayTimer == 300)
        {
            UFODeathParticles.Play();
            GetComponent<MeshRenderer>().enabled = false;
            myOwnCockpit.GetComponent<MeshRenderer>().enabled = false;
            deathDelayTimer = deathDelayTimer - 1;
        }
        else if (deathDelayTimer > 0)
        {
            deathDelayTimer = deathDelayTimer - 1;
        }
    }

    private void CheckIfLanded()
    {
        //tell the spawner to accelerate the rate at which enemies are spawned then die if you have
    }

    public void TakeDamage(float damage)
    {
        health = health - damage;
    }
}
