using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour {

    public static bool Paused = false;

    public GameObject pauseMenuUI;
    [SerializeField]
    private GameObject PauseInstruction;

    void Start()
    {
        ResumeGame();
    }

	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (PauseInstruction.activeSelf)
            {
                PauseInstruction.SetActive(false);
            }
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
       
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
        
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
    }


}
