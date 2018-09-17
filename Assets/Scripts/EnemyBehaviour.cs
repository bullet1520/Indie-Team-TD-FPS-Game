using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    
    //9/10/2018: this version of enemy is only capable of moving towards a target and dying when hit by the player's raycasts
    public GameObject Target;
    

    int hasHit;
    Transform target;
    public float ownHealth = 30f;
    UnityEngine.AI.NavMeshAgent nav;


    void Awake()
    {
        
        
        target = Target.transform;
        
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void TakeDamage(float amount)
    {
        ownHealth -= amount;
        if (ownHealth <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


    void Update()
    {
         //this whole mess is doing the following: 
         //if the enemy unit is within a small distance of its target it stops moving and attempts to deal damage to the target
         //this is not functional currently but will be in later versions, it is a relic of code i ripped from a different project of mine
        if (Vector3.Distance(transform.position, target.position) < 3f) //if close enough to target
        {
            nav.enabled = false; // stop moving
            if (hasHit == 0) //check a timer to keep it from hitting every frame, rather hit every couple seconds
            {
                hasHit = 100;
            }
            else
            { hasHit = hasHit - 1; } 
        }
        else // if not close enough to target
        {
            nav.enabled = true; // keep moving at the target
            nav.SetDestination(target.position);
        }

        if (hasHit != 0) //the timer still goes down if the target has moved
        { hasHit = hasHit - 1; }
       
    }

  

    

}
