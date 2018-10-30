using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingImage : MonoBehaviour
{

    int counter;
    int GameTime;

    [SerializeField]
    GameObject ThisUI;   


	// Use this for initialization
	void Start ()
    {
        ThisUI.GetComponent<Image>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        counter++;
        GameTime++;
        if (counter == 10)
        {
            Blink();
        }

        if (counter == 15)
        {
            AntiBlink();
        }

        if (GameTime == 200)
        {
            ThisUI.SetActive(false);
        }
	}

    void Blink()
    {
        ThisUI.GetComponent<Image>().enabled = true;
    }

    void AntiBlink()
    {
        ThisUI.GetComponent<Image>().enabled = false;
        counter = 0;
    }

}
