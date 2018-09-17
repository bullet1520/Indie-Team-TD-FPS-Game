using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteself : MonoBehaviour {

    public int timer;
	// Use this for initialization
	void Start () {
        timer = 400;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer == 0)
        {
            this.gameObject.SetActive(false);
        }
        else { timer = timer - 1; }
	}
}
