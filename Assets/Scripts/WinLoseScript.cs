using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseScript : MonoBehaviour
{
    public GameObject LoseScreen;
    public GameObject WinScreen;

    public GameObject Objective;

    public GameObject Spawner;
    public GameObject Enemy;

   

   public int Life;


    // Use this for initialization
    void Start ()
    {
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        Life = gameObject.GetComponent<Objective>().objectiveHealth;
        if (Life <= 0)
        {
            Lose();
        }

        //if ()
        //{

        //}




	}

    void Lose()
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
