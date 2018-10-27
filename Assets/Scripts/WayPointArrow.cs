using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointArrow : MonoBehaviour {


    //we want a series of objects probably making from a prefab to tell us that there is an object to pay attention to. 
    //each object that can have a waypoint needs to instantiate one of these arrows
    //each arrow needs to know where it should point at all times and whether or not it needs to be invisible/delete itself.

    //place script on arrow? to tell it where it should point.
    //place bound box check on each object that can have an arrow and assign it a public is in view variable to be called on by the arrow if it needs to.

    //what objects are going to have the arrows? dropships and objectives for now.
    [SerializeField]
    CameraMotion Root;
    [SerializeField]
    Transform targetTransform;
    Vector3 screenPos; //this is the worldspace position we are tracking
    Vector2 onScreenPos; //this is the position we are putting the arrow in
    float max;
    [SerializeField]
    Camera playerCamera;

    [SerializeField]
    private float toldxpos;
    [SerializeField]
    private float toldypos;



    void Start()
    {
        playerCamera = Camera.main;
        
    }

    void Update()
    {
       

        screenPos = playerCamera.WorldToViewportPoint(targetTransform.position); //get viewport positions

       

        onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2; //2D version, new mapping
        max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
        onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping
        Debug.Log(onScreenPos);

       
            transform.position = playerCamera.ViewportToScreenPoint(new Vector3(onScreenPos.x, onScreenPos.y, 0));
           
       
    }
    
}
