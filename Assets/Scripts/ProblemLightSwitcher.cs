using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemLightSwitcher : MonoBehaviour {


    [SerializeField]
    private GameObject[] LightsToChange;
    [SerializeField]
    private Material glowingYellow;
    [SerializeField]
    private Material EnemyRed;
    [SerializeField]
    private float timeToSwitchBack = -1;

   
	
	// Update is called once per frame
	void Update () {
        if (timeToSwitchBack > 0)
        {
            timeToSwitchBack = timeToSwitchBack - 1;
        }
        else if (timeToSwitchBack == 0)
        {
            SwitchAllToYellow();
            timeToSwitchBack = -1;
        }
	}

    public void SwitchAllToRed()
    {
        if (timeToSwitchBack < 0)
        {
            foreach (GameObject light in LightsToChange)
            {
                light.GetComponent<Renderer>().material = EnemyRed;
            }
        }
        else { }
        timeToSwitchBack = 50f;
    }
    void SwitchAllToYellow()
    {
        foreach (GameObject light in LightsToChange)
        {
            light.GetComponent<Renderer>().material = glowingYellow;
        }
    }
}
