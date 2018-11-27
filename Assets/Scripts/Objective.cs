using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour {
    //this is a simple script that tracks how much health the objective has, updates a slider to display that to the player
    //and holds a function for the enemy to call that deals damage to it.
    [SerializeField]
    private AudioSource DamageClink;

    public bool isVisibleByPlayerCamera; //this is accessed by the WayPointArrow Script
    [SerializeField]
    private Camera playerCamera;
    private BoxCollider myOwnCollider;
    //[SerializeField]
    //private ProblemLightSwitcher LightSwitcher;
    [SerializeField]
    private bool canTakeDamage = true;
    [SerializeField]
    private Slider HealthSlider;
    [SerializeField]
    private float objectiveHealth = 100;
    private WinLoseScript winLoseScript;
    [SerializeField]
    private WayPointArrow attachedArrow;

    private void Start()
    {
        winLoseScript = GetComponent<WinLoseScript>();
        myOwnCollider = GetComponent<BoxCollider>();
    }
    private void Update()
    {
        CheckIfVisibleByPlayerCamera();
        HealthSlider.value = objectiveHealth;
        if (objectiveHealth <= 0)
        {
            winLoseScript.Lose();
        }
    }

    public void TakeDamage(float enemydamage)
    {
        DamageClink.Play();
        attachedArrow.addToDamageTimerIfOnObjective();
       // LightSwitcher.SwitchAllToRed();
        if (canTakeDamage)
        {
            objectiveHealth = objectiveHealth - enemydamage;
        }
        
    }

    public void CheckIfVisibleByPlayerCamera()
    { //the long line below should return whether the bounds of the object is in view of the playerCamera.
        if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(playerCamera), myOwnCollider.bounds))
        {
            isVisibleByPlayerCamera = true;
        }
        else
        { isVisibleByPlayerCamera = false; }
    }
}
