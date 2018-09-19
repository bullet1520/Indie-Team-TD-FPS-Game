using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeGame : MonoBehaviour {

    public GameObject pauseMenuUI;

	public void Continue()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenuScript.Paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
