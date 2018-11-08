using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VivaLaCameraRevolution : MonoBehaviour {

    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private int CameraSpeed = 10;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.RotateAround(Target.transform.position, Vector3.down, Time.deltaTime * CameraSpeed);
	}
}
