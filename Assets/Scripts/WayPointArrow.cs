using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WayPointArrow : MonoBehaviour {


    //finally this bit works correctly, follows objective and does not move to strange parts of the screen.
    
    public CameraMotion Root;
    
    public GameObject TargetObject;
    [SerializeField]
    private Image ownImage;
    private Transform targetTransform;
    private Vector3 screenPos; //this is the worldspace position we are tracking
    private Vector2 onScreenPos; //this is the position we are putting the arrow in
    private float max;
    public Transform canvasTransform;
    public Camera playerCamera;
    private RectTransform ownRT;
    [SerializeField]
    private bool isFlipped;
    [SerializeField]
    private string occupiedEdge;
    [SerializeField]
    private float toldxpos;
    [SerializeField]
    private float toldypos;
    [SerializeField]
    private float toldscreenposxprecal;
    [SerializeField]
    private float toldscreenposyprecal;
    [SerializeField]
    private float toldscreenposzprecal;
    [SerializeField]
    private bool TargetIsVisibleByPlayerCamera;
    [SerializeField]
    private int objectTracked = 0;
    [SerializeField]
    private int ObjectiveDamageTimer = 0;
    private bool scaleHasBeenFixed = false;

    private void Awake()
    {
        ownImage.enabled = false;
        targetTransform = TargetObject.GetComponent<Transform>();
        if (TargetObject.GetComponent<Objective>() != null)
        {
            objectTracked = 1;
        }
        else if (TargetObject.GetComponent<UFOBehaviour>() != null)
        {
            objectTracked = 2;
        }
        transform.parent = canvasTransform;
        
    }
    void Start()
    {
        playerCamera = Camera.main; //first enabled camera means that any secondary cameras used would need to be updated.
        ownRT = GetComponent<RectTransform>();
        ownImage = GetComponent<Image>();
    }

    void Update()
    {
        if (!scaleHasBeenFixed)
        {
            ownRT.localScale = new Vector3(1, 1, 1);
            scaleHasBeenFixed = true;
        }
       
        DieOnTargetDeath();
        if (objectTracked == 1)
        {
            if (ObjectiveDamageTimer <= 0)
            {
                ownImage.enabled = false;
                return;
            }
            else if (ObjectiveDamageTimer > 0)
            {
                ownImage.enabled = true;
                ObjectiveDamageTimer = ObjectiveDamageTimer - 1;
            }
        }

        CheckifTargetVisibleByCamera();
        if (TargetIsVisibleByPlayerCamera)
        {
            ownImage.enabled = false;
            return;
        }
        else
        {
            ownImage.enabled = true;
        }

        screenPos = playerCamera.WorldToViewportPoint(targetTransform.position); //get viewport positions
        //this is figuring where on the viewport the transform of the object would be, this value can exceed the bounds of the viewport
        assignWorldToViewPortResults();

       
        //screenPosZ is not used in the subsequent caluclation but it does tell us when it flips.
        onScreenPos = new Vector2(screenPos.x - 0.5f, screenPos.y - 0.5f) * 2; //2D version, new mapping
       
        max = Mathf.Max(Mathf.Abs(onScreenPos.x), Mathf.Abs(onScreenPos.y)); //get largest offset
        onScreenPos = (onScreenPos / (max * 2)) + new Vector2(0.5f, 0.5f); //undo mapping
        Debug.Log(onScreenPos);
        //onScreenPos always has a value of zero or one on the extreme where the arrow is resting, i need to find an appropriate offset to move it to so the 
        //image comes through facing the right way.
       
        transform.position = playerCamera.ViewportToScreenPoint(new Vector3(onScreenPos.x, onScreenPos.y, 0));

        flipScreenEdgeWhenFacingAway();


        toldxpos = ownRT.anchoredPosition.x;
        toldypos = ownRT.anchoredPosition.y;

        OffsetPositionAndRotationAccordingToOccupiedEdge();
    }

    public void addToDamageTimerIfOnObjective()
    {
        ObjectiveDamageTimer = 50;
    }

    void CheckifTargetVisibleByCamera()
    {
        if (objectTracked == 1)
        {
            Objective trackedScript = TargetObject.GetComponent<Objective>();
            if (trackedScript.isVisibleByPlayerCamera)
            {
                TargetIsVisibleByPlayerCamera = true;
            }
            else
            {
                TargetIsVisibleByPlayerCamera = false;
            }
        }
        else if (objectTracked == 2)
        {
            UFOBehaviour trackedScript = TargetObject.GetComponent<UFOBehaviour>();
            if (trackedScript.isVisibleByCamera)
            {
                TargetIsVisibleByPlayerCamera = true;
            }
            else
            {
                TargetIsVisibleByPlayerCamera = false;
            }
        }
    }

    void DieOnTargetDeath()
    {
        if (objectTracked == 2)
        {
            UFOBehaviour trackedScript = TargetObject.GetComponent<UFOBehaviour>();
            if (trackedScript.deathDelayTimer < 300)
            {
                Destroy(gameObject);
            }
            else
            { }
        }
    }

    void OffsetPositionAndRotationAccordingToOccupiedEdge()
    {
        // 1.0, x = right
        // x, 0.0 = bottom
        // 0.0, x = left
        // x, 1.0 = top
        // all of these are reversed when flipped.
        FindOccupiedEdge();
        if (occupiedEdge == "Right")
        {
            ownRT.anchoredPosition = new Vector2(ownRT.anchoredPosition.x - 50, ownRT.anchoredPosition.y);
            ownRT.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (occupiedEdge == "Left")
        {
            ownRT.anchoredPosition = new Vector2(ownRT.anchoredPosition.x + 50, ownRT.anchoredPosition.y);
            ownRT.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (occupiedEdge == "Top")
        {
            ownRT.anchoredPosition = new Vector2(ownRT.anchoredPosition.x, ownRT.anchoredPosition.y - 50);
            ownRT.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (occupiedEdge == "Bottom")
        {
            ownRT.anchoredPosition = new Vector2(ownRT.anchoredPosition.x, ownRT.anchoredPosition.y + 50);
            ownRT.eulerAngles = new Vector3(0, 0, 180);
        }

    }

    void FindOccupiedEdge()
    {
        if (!isFlipped && onScreenPos.x == 1.0)
        {
            occupiedEdge = "Right";
        }
        else if (!isFlipped && onScreenPos.x == 0.0)
        {
            occupiedEdge = "Left";
        }
        else if (!isFlipped && onScreenPos.y == 1.0)
        {
            occupiedEdge = "Top";
        }
        else if (!isFlipped && onScreenPos.y == 0.0)
        {
            occupiedEdge = "Bottom";
        }
        else if (isFlipped && onScreenPos.x == 1.0)
        {
            occupiedEdge = "Left";
        }
        else if (isFlipped && onScreenPos.x == 0.0)
        {
            occupiedEdge = "Right";
        }
        else if (isFlipped && onScreenPos.y == 1.0)
        {
            occupiedEdge = "Bottom";
        }
        else if (isFlipped && onScreenPos.y == 0.0)
        {
            occupiedEdge = "Top";
        }
    }

    void assignWorldToViewPortResults()
    {
        toldscreenposxprecal = screenPos.x;
        toldscreenposyprecal = screenPos.y;
        toldscreenposzprecal = screenPos.z;
    }

    void flipScreenEdgeWhenFacingAway()
    {
        if (toldscreenposzprecal < 0)
        {
            isFlipped = true;
            ownRT.anchoredPosition = new Vector2(ownRT.anchoredPosition.x * -1, ownRT.anchoredPosition.y * -1);
        }
        else { isFlipped = false; }
    }
}
