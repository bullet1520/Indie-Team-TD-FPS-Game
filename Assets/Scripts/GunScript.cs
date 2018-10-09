using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour {

    public float damage = 10f;
    public float reloadtimer = 0f;
    public Camera fpsCam;
    public Animator turretAnimator;
    public GameObject reloadtag;
    public Slider enemyHealthSlider;
    public ParticleSystem MuzzleFlare;
    EnemyBehaviour target = null;
    private AudioSource turretShot;
    private Vector3 debughitspace;
    private int layermask = 1 << 8;

    private void Awake()
    {
        turretShot = GetComponent<AudioSource>();
        reloadtag.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
         target = null;
        if (!PauseMenuScript.Paused)
        { CheckEnemy(); }

        if (Input.GetButtonDown("Fire1") && !PauseMenuScript.Paused)
        {
            if (reloadtimer <= 1)
            {
                turretAnimator.SetBool("Shooting", true);
                Shoot();
            }
        }

        if (reloadtimer == 1 && reloadtag.activeInHierarchy)
        {
            reloadtag.SetActive(false);
            turretAnimator.SetBool("Shooting", false);
        }
        else if (reloadtimer > 1 && !PauseMenuScript.Paused)
        { reloadtimer = reloadtimer - 1; }
	}
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
            Collider[] targets = Physics.OverlapSphere(hit.point, 3f, layermask);
            
            foreach (Collider E in targets)
            {
                target = E.transform.GetComponent<EnemyBehaviour>();
                target.TakeDamage(damage);
            }
        }
        if (reloadtimer < 2 && !reloadtag.activeInHierarchy)
        {
            reloadtimer = 100f;
            reloadtag.SetActive(true);
        }
        
    }

    void CheckEnemy()
    {
        
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
           // Debug.DrawLine(fpsCam.transform.position, hit.point, Color.red, 5);
             target = hit.transform.GetComponent<EnemyBehaviour>();

            if (target != null)
            {
                enemyHealthSlider.value = 1;
            }
            else
            {
                enemyHealthSlider.value = 0;
            }
        }
    }
}
