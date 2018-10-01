using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {


    public bool Controller = false;

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    //private bool cursorlock = true;

	// Use this for initialization
	void Start () {
       
        lockcursor();
	}
	
	// Update is called once per frame
	void Update () {

       

        if (!PauseMenuScript.Paused && !Controller)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");
            
            if (pitch > 50)
            {
                pitch = 50f;
            }
            else if (pitch < -50)
            {
                pitch = -50f;
            }

            transform.eulerAngles = new Vector3(0.0f, yaw, pitch);
        }

        if (!PauseMenuScript.Paused && Controller)
        {
            yaw += speedH * Input.GetAxis("Horizontal");
            pitch -= speedV * Input.GetAxis("Vertical");

            if (pitch > 50)
            {
                pitch = 50f;
            }
            else if (pitch < -50)
            {
                pitch = -50f;
            }

            transform.eulerAngles = new Vector3(0.0f, yaw, pitch);
        }



        //if (Input.GetKeyDown(KeyCode.Escape))
        //{

        //    unlockcursor();

        //}


    }


    void lockcursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //void unlockcursor()
    //{
    //    Cursor.visible = true;
    //    Cursor.lockState = CursorLockMode.None;
    //}

} //commented out the above if statement. Functionality of the function within moved to  pause script.
