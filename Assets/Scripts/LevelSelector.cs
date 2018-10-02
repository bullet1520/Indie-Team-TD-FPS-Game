using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    GameObject Lev1;
    GameObject Lev2;
    GameObject Lev3;
    GameObject Back;
    GameObject DescriptionObject;

    Text description;


	// Use this for initialization
	void Start ()
    {

        Lev1 = GameObject.Find("Lv1BG");
        Lev2 = GameObject.Find("SpiralBG");
        Lev3 = GameObject.Find("ForkBG");
        Back = GameObject.Find("MainBG");
        DescriptionObject = GameObject.Find("LevelPreviewText");


        description = DescriptionObject.GetComponent<Text>();

        description.text = "Select a level to play!";

	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void buttonhover(Button button)
    {
        if (button.name == "L1Btn")
        {
            Lev1.transform.SetAsLastSibling();
            description.text = "The test level. There's barely anything here.";
            
        }

        else if (button.name == "L2Btn")
        {
            Lev2.transform.SetAsLastSibling();
            description.text = "A winding path that slowly pushes enemies toward the center.";

        }

        else if (button.name == "L3Btn")
        {
            Lev3.transform.SetAsLastSibling();
            description.text = "A forked path that forces the player to handle enemies from 2 directions.";
        }

        else if (button.name == "Back_Btn")
        {
            Back.transform.SetAsLastSibling();
            description.text = "Return to the main menu.";

        }
    }

    public void buttonclick(Button button)
    {
        if (button.name == "L1Btn")
        {
            SceneManager.LoadScene("testzone");
            Debug.Log("Player selected the Test Zone");
        }

        else if (button.name == "L2Btn")
        {
            //SceneManager.LoadScene("Spiral");
            Debug.Log("Player selected the Spiral");
        }

        else if (button.name == "L3Btn")
        {
            SceneManager.LoadScene("Fork");
            Debug.Log("Player selected the Fork");
        }

    }
}
