using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour {

    public static bool Paused = false;

    public GameObject pauseMenuUI;
    public GameObject PlayerCamera;


    void Start()
    {
        
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (Paused)
            {
                ResumeGame();
                Debug.Log("Player resumed gameplay.");
            }

            else
            {
                PauseGame();
                Debug.Log("Player paused the game.");
            }

        }
	}

    void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
        CameraMotion FPC = PlayerCamera.GetComponent<CameraMotion>();
        FPC.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        CameraMotion FPC = PlayerCamera.GetComponent<CameraMotion>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        FPC.enabled = false;
    }


}
