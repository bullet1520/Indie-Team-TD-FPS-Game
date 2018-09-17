using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective : MonoBehaviour {

    public int objectiveHealth = 100;
    public int enemydamage = 3;
    public Slider HealthSlider;


    private void Update()
    {
        HealthSlider.value = objectiveHealth;
    }

    public void TakeDamage()
    { objectiveHealth = objectiveHealth - enemydamage; }
}
