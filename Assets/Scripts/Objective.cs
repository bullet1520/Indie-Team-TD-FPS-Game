using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour {
    //this is a simple script that tracks how much health the objective has, updates a slider to display that to the player
    //and holds a function for the enemy to call that deals damage to it.

    public int enemydamage = 3;
    public Slider HealthSlider;
    [SerializeField]
    private int objectiveHealth = 100;
    private WinLoseScript winLoseScript;
    private void Start()
    {
        winLoseScript = GetComponent<WinLoseScript>();
    }
    private void Update()
    {
        HealthSlider.value = objectiveHealth;
        if (objectiveHealth <= 0)
        {
            winLoseScript.Lose();
        }
    }

    public void TakeDamage()
    { objectiveHealth = objectiveHealth - enemydamage; }
}
