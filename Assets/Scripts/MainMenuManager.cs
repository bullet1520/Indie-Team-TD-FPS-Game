using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    ///is this script depricated? what is this doing other than quitting?
    //Button LevelSelect = ;

    public void Play()
    {
        Debug.Log("Player moved to level select screen.");
    }

    public void Options()
    {
        Debug.Log("Player moved to options menu.");
    }

    public void Credits()
    {
        Debug.Log("Player went to check out the credits.");
    }


    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player quit the game.");
    }

}
