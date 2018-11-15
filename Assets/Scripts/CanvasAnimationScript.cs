using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAnimationScript : MonoBehaviour {

    [SerializeField]
    private Animator canvasAnimator;
    [SerializeField]
    private GameObject plusFiveText;

    private int Timer = 0;
	
	
	// Update is called once per frame
	void Update () {

        if (Timer > 0)
        {
            Timer = Timer - 1;
        }
        
        if (Timer <= 0)
        {
            plusFiveText.SetActive(false);
            canvasAnimator.SetBool("hasLanded", false);
        }
	}

    public void ResetTimer()
    {
        Timer = 50;
        plusFiveText.SetActive(true);
        canvasAnimator.SetBool("hasLanded", true);
    }

}
