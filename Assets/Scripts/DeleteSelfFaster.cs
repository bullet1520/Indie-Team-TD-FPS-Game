using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSelfFaster : MonoBehaviour {
    ///this is a simple script that tells an object to delete itself after a period of a second or so
    ///this is intended for instructions as the level begins.
    [SerializeField]
    private int timer;
    // Use this for initialization
    void Start()
    {
        timer = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer == 0)
        {
            Destroy(gameObject);
        }
        else if (!PauseMenuScript.Paused)
        {
            timer = timer - 1;
        }
    }
}
