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

    public GameObject InstrText;

    private int Life;


    // Use this for initialization
    void Start ()
    {
        Life = gameObject.GetComponent<Objective>().objectiveHealth;
	}
	
	// Update is called once per frame
	void Update ()
    {
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
        LoseScreen.SetActive(true);
        Time.timeScale = 0f;
        InstrText.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    void Win()
    {
        WinScreen.SetActive(true);
        Time.timeScale = 0f;
        InstrText.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
