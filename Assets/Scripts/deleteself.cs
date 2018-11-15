using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteself : MonoBehaviour {

    ///this is a simple script that tells an object to delete itself after a period of a few seconds
    ///this is intended for instructions as the level begins.
    ///
    [SerializeField]
    private GameObject clickInstruction;
    [SerializeField]
    private GameObject pauseInstruction;

    [SerializeField]
    private int timer;
	// Use this for initialization
	void Start () {
        timer = 400;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer == 0)
        {
            clickInstruction.SetActive(true);
            pauseInstruction.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else { timer = timer - 1; }
	}
}
