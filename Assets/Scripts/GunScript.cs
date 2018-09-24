using UnityEngine;

public class GunScript : MonoBehaviour {

    public float damage = 10f;
    public float reloadtimer = 0f;
    public Camera fpsCam;

    public GameObject reloadtag;

    private void Awake()
    {
        reloadtag.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1") && !PauseMenuScript.Paused)
        {
            if (reloadtimer <= 1)
            { Shoot(); }
        }

        if (reloadtimer == 1 && reloadtag.activeInHierarchy)
        {
            reloadtag.SetActive(false);
        }
        else if (reloadtimer > 1 && !PauseMenuScript.Paused)
        { reloadtimer = reloadtimer - 1; }
	}

    void Shoot()
    {
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
}
