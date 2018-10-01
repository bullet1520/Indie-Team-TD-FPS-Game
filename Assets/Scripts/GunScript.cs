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

    private AudioSource turretShot;

    private void Awake()
    {
        turretShot = GetComponent<AudioSource>();
        reloadtag.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
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

    void Shoot()
    {
        turretShot.Play();
        MuzzleFlare.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            EnemyBehaviour target = hit.transform.GetComponent<EnemyBehaviour>();

            if (target != null)
            {
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
            EnemyBehaviour target = hit.transform.GetComponent<EnemyBehaviour>();

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
