using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScript : MonoBehaviour
{
    ///this is a simple script that tells the player whether they have won or lost when the condition occurs
    ///when the objective object this is on loses all its health it activates Lose()
    ///when the EnemySpawner script detects there are no more enemies alive that script calls this one to activate Win()
    [SerializeField]
    private GameObject LoseScreen;
    [SerializeField]
    private GameObject WinScreen;
    [SerializeField]
    private GameObject Objective;


    // Use this for initialization
    void Start ()
    {
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void Lose()
    {
        PauseMenuScript.Paused = true;
        LoseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Win()
    {
        PauseMenuScript.Paused = true;
        WinScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
