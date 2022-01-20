using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchHandler : MonoBehaviour
{
    private Camera mainCamera;
    public Canvas dialogueCanva;
    private bool isZoomed;

    //direction swipe & pinch
    private Vector2 touchStartPosition, touchEndPosition;
    private Vector2 touchStartPosition2, touchEndPosition2;
    public Text multiTouchInfoDisplay;

    private SideEnum lastPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
	{
        isZoomed = mainCamera.GetComponent<CameraScript>().isZoomed;
        lastPosition = SideEnum.Right;
    }

	// Update is called once per frame
	void Update()
    {
        isZoomed = mainCamera.GetComponent<CameraScript>().isZoomed;
        //swipe direction
        if (Input.touchCount > 0)
        {
            if (isZoomed && Input.touchCount >= 2) //pinch for unzoom
            {
                string multiTouchInfo = ""; //debug

                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                if (touch1.phase == TouchPhase.Began)
                {
                    touchStartPosition = touch1.position.normalized;
                }
                if (touch2.phase == TouchPhase.Began)
                {
                    touchStartPosition2 = touch2.position.normalized;
                }

                else if (touch1.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Ended)
                {
                    touchEndPosition = touch1.position.normalized - touchStartPosition;
                    touchEndPosition2 = touch2.position.normalized - touchStartPosition2;


                    if (touchEndPosition != Vector2.zero || touchEndPosition2 != Vector2.zero)
                    {
                        mainCamera.GetComponent<CameraScript>().SetNavigationData(lastPosition, shouldZoom: false);
                        dialogueCanva.enabled = false;
                    }
                }

                //debug
                
                multiTouchInfo = "touch 1 "+touchEndPosition+" & touch 2 "+touchEndPosition2;
                multiTouchInfoDisplay.text = multiTouchInfo;
                
            }

            else if (!isZoomed) //swipe right/left
            {
                Touch theTouch = Input.GetTouch(0);

                if (theTouch.phase == TouchPhase.Began)
                {
                    touchStartPosition = theTouch.position;
                }

                else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Ended)
                {
                    touchEndPosition = theTouch.position;

                    float x = touchEndPosition.x - touchStartPosition.x;
                    float y = touchEndPosition.y - touchStartPosition.y;

                    if ((Mathf.Abs(x) > Mathf.Abs(y)))
                    {
                        lastPosition = x > 0 ? SideEnum.Left : SideEnum.Right;
                        mainCamera.GetComponent<CameraScript>().SetNavigationData(lastPosition, shouldZoom: false);
                    }
                    //else{direction = y > 0 ? "Up" : "Down";}
                }
            }
        }
    }
}
