using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour {
    ///this script allows the player to fire their cannon in the direction they are facing and deal damage to all enemies in a sphere around the point they fired at
    ///this script also tells the reticle to light up whenever the player is facing an enemy.

    public float damage = 10f; //public to be tweaked in engine;
    public float timeBetweenReloads = 100f; //public to be tweaked in engine;
    public Camera fpsCam;
    public Animator turretAnimator;
    public GameObject reloadTag;
    public Slider enemyHealthSlider;
    public ParticleSystem MuzzleFlare;
    public GameObject ImpactDetonation;
    [SerializeField]
    private float reloadtimer = 0f;
    [SerializeField]
    private GameObject target = null;
    [SerializeField]
    private GameObject lastTarget;
    private AudioSource turretShot;
    private Vector3 debughitspace;
    private int layermask = 1 << 8;

    private void Awake()
    {
        turretShot = GetComponent<AudioSource>();
        reloadTag.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
         target = null;
        if (!PauseMenuScript.Paused)
        { CheckIfFacingEnemy(); }

        if (Input.GetButtonDown("Fire1") && !PauseMenuScript.Paused)
        {
            if (reloadtimer <= 1)
            {
                turretAnimator.SetBool("Shooting", true);
                Shoot();
            }
        }

        if (reloadtimer == 1 && reloadTag.activeInHierarchy)
        {
            reloadTag.SetActive(false);
            turretAnimator.SetBool("Shooting", false);
        }
        else if (reloadtimer > 1 && !PauseMenuScript.Paused)
        { reloadtimer = reloadtimer - 1; }
	}
    //the following script makes the radius of the gun's explosion visible in the scene view. 
    //uncomment this code if you wish to see it.
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
     //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
     Gizmos.DrawWireSphere(debughitspace, 3f);
    }
    */
    void Shoot()
    {
        turretShot.Play();
        MuzzleFlare.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            debughitspace = hit.point;
            Quaternion blank = new Quaternion(0, 0, 0, 0);
            Instantiate(ImpactDetonation, hit.point, blank);
            Collider[] targets = Physics.OverlapSphere(hit.point, 3f, layermask);
            
            foreach (Collider enemyCollidersCollected in targets)
            {
                target = enemyCollidersCollected.transform.gameObject;
                if (target.GetComponent<EnemyBehaviour>() != null)
                {
                    EnemyRobotHit(target.GetComponent<EnemyBehaviour>(), hit.point);
                }
                else if (target.GetComponent<UFOBehaviour>() != null)
                {
                    EnemyUFOHit(target.GetComponent<UFOBehaviour>());
                }


               
                
            }
        }
        if (reloadtimer < 2 && !reloadTag.activeInHierarchy)
        {
            reloadtimer = timeBetweenReloads;
            reloadTag.SetActive(true);
        }
        
    }

    void EnemyRobotHit(EnemyBehaviour targetScript, Vector3 ExplosionLocation)
    {
        targetScript.TakeExplosiveDamage(damage, ExplosionLocation);
    }

    void EnemyUFOHit(UFOBehaviour targetScript)
    {
        targetScript.TakeDamage(damage);
    }


    void CheckIfFacingEnemy()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            //the following script makes the raycasts that are being fired from the gun visible in the scene editor,
            //uncomment if you need to see it.
            //Debug.DrawLine(fpsCam.transform.position, hit.point, Color.red, 1);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                target = hit.transform.gameObject;
            }



            
            if (target != null)
            {
                if (target.GetComponent<EnemyBehaviour>() != null)
                {
                    collectRobotHealthForDisplay(target.GetComponent<EnemyBehaviour>());
                }
                else if (target.GetComponent<UFOBehaviour>() != null)
                {
                    collectUFOHealthForDisplay(target.GetComponent<UFOBehaviour>());
                }
                 //else if (target.GetCompenent<WhateverInameotherenemyscriptsto>() != null)

            }
            else
            {
                enemyHealthSlider.value = 0;
            }

        }
        else { enemyHealthSlider.value = 0; }
    }

    void collectRobotHealthForDisplay(EnemyBehaviour enemyScript)
    {
        enemyHealthSlider.value = (enemyScript.ownHealth * 100) / 10;
    }
    void collectUFOHealthForDisplay(UFOBehaviour ufoScript)
    {
        enemyHealthSlider.value = (ufoScript.health * 100) / 40;
    }


}
