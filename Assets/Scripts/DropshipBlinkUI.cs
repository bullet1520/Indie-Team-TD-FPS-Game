using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropshipBlinkUI : MonoBehaviour {
    private int counter;
    public int TimeElapsed = 0;

    
    [SerializeField]
    private GameObject ThisUI;
    private bool isOn = false;

    // Use this for initialization
    void Start()
    {
        ThisUI.GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            counter++;
            TimeElapsed++;
        }
        
       
        if (counter == 10)
        {
            Blink();
        }

        if (counter == 15)
        {
            AntiBlink();
        }

        if (TimeElapsed == 200)
        {
            isOn = false;
            TimeElapsed = 0;
            AntiBlink();
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
    public void StartOver()
    {
        isOn = true;
    }
}
