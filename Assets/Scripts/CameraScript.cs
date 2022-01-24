using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public float Speed = 2.0f;
    private SideEnum sideToNavigateTo;

    private Vector3 windowPosition;
    public Vector3 RightBuildingPosition;
    public Vector3 LeftBuildingPosition;
    private Vector3 PositionToNagivateTo;

    public int zoom = 20;
    public int normal = 41;
    public float smooth = 5;
    public bool isZoomed;

    //mobile
    //touch
    public Text phaseDisplayText;
    private Touch theTouch;
    private float timeTouchEnded;
    private float displayTime = 0.5f;
    //direction swiipe
    public Text directionText;
    private Vector2 touchStartPosition, touchEndPosition;
    //multitouch
    public Text multiTouchInfoDisplay;
    private int maxTapCount = 0;
    private string multiTouchInfo;

    private void Start()
    {
        isZoomed = false;
        sideToNavigateTo = SideEnum.Right;

    }

    public void SetNavigationData(Vector3 newPosition, bool shouldZoom)
    {
        windowPosition = new Vector3(transform.position.x, newPosition.y, newPosition.z);
        isZoomed = shouldZoom;
        sideToNavigateTo = SideEnum.Window;
    }
    public void SetNavigationData(SideEnum side, bool shouldZoom)
    {
        isZoomed = shouldZoom;
        sideToNavigateTo = side;
    }

    private void Update()
    {
        /* touch / long touch
        if(Input.touchCount > 0 )
        {
           theTouch = Input.GetTouch(0);
           if(theTouch.phase == TouchPhase.Ended)
           {
               phaseDisplayText.text = theTouch.phase.ToString();
               timeTouchEnded = Time.time;
           }
           else if(Time.time - timeTouchEnded > displayTime)
           {
               phaseDisplayText.text = theTouch.phase.ToString();
               timeTouchEnded = Time.time;
           }            
       }
       else if (Time.time - timeTouchEnded > displayTime)
       {
           phaseDisplayText.text = "";
       }
        */

        //swipe direction
        if (Input.touchCount > 0)
        {
            if (IsZoomed && Input.touchCount >= 2) //pinch for unzoom
            {
            }

            else //swipe right/left
            {
                theTouch = Input.GetTouch(0);

                if (theTouch.phase == TouchPhase.Began)
                {
                    touchStartPosition = theTouch.position;
                }

                else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
                {
                    touchEndPosition = theTouch.position;

                    float x = touchEndPosition.x - touchStartPosition.x;
                    float y = touchEndPosition.y - touchStartPosition.y;

                    if ((Mathf.Abs(x) > Mathf.Abs(y)) && !IsZoomed)
                    {
                        sideToNavigateTo = x > 0 ? SideEnum.Left : SideEnum.Right;
                    }
                    //else{direction = y > 0 ? "Up" : "Down";}
                }
            }
        }



        multiTouchInfo = string.Format("Max tap count: {0}\n", maxTapCount);

        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                theTouch = Input.GetTouch(i);

                multiTouchInfo +=
                 string.Format("Touch {0} - Position {1} - Tap Count: {2} - Finger ID: {3}\nRadius: {4} ({5}%)\n",
                 i, theTouch.position, theTouch.tapCount, theTouch.fingerId, theTouch.radius,
                 ((theTouch.radius / (theTouch.radius + theTouch.radiusVariance)) * 100f).ToString("F1"));

                if (theTouch.tapCount > maxTapCount)
                {
                    maxTapCount = theTouch.tapCount;
                }
            }
        }

        //multiTouchInfoDisplay.text = multiTouchInfo;


        /////////////////////////////////////

        //check which side we have to navigate to in order to get the right position
        switch (sideToNavigateTo)
        {
            case SideEnum.Right:
                PositionToNagivateTo = RightBuildingPosition;
                break;
            case SideEnum.Left:
                PositionToNagivateTo = LeftBuildingPosition;
                break;
            case SideEnum.Window:
                PositionToNagivateTo = windowPosition;
                break;
            default:
                break;
        }
        //Check the FOV
        if (isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, smooth * Time.deltaTime);
        }
        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, smooth * Time.deltaTime);
        }
        transform.position = Vector3.Lerp(transform.position, PositionToNagivateTo, Speed * Time.deltaTime);
    }
    public void OnLeftClick()
    {
        sideToNavigateTo = SideEnum.Left;
    }
    public void OnRightClick()
    {
        sideToNavigateTo = SideEnum.Right;
    }
}
