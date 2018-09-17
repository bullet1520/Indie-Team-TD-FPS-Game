using UnityEngine;

public class GunScript : MonoBehaviour {

    public float damage = 10f;
   
    public Camera fpsCam;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
		
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

    }
}
