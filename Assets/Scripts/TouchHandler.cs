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

    //window opening
    SideEnum previousCameraPosition;
    Animator animatorWindow;

    private IAmoving selectedCharacter;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
	{
        isZoomed = mainCamera.GetComponent<CameraScript>().isZoomed;
    }

	void Update()
    {
        isZoomed = mainCamera.GetComponent<CameraScript>().isZoomed;
        
	    //Select
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.transform.name); //name of actor hit

                if (hit.transform.gameObject.tag.Equals("Window") && !isZoomed)
                {
                    animatorWindow = hit.transform.parent.gameObject.GetComponentInParent<Animator>();
                    if (mainCamera.GetComponent<CameraScript>().sideToNavigateTo != SideEnum.Window)
                        previousCameraPosition = mainCamera.GetComponent<CameraScript>().sideToNavigateTo;
                    mainCamera.GetComponent<CameraScript>().SetNavigationData(hit.transform.gameObject.GetComponent<Renderer>().bounds.center, shouldZoom: true);
                    animatorWindow.enabled = true;
                    animatorWindow.SetFloat("Direction", 1);
                    animatorWindow.PlayInFixedTime("ParentAnim", -1, 0);
                }

                else if (hit.transform.gameObject.TryGetComponent<IAmoving>(out IAmoving mov))
                {
                    //Debug.Log("Touched " + hit.transform.gameObject.transform.name);
                    selectedCharacter = mov;
                    selectedCharacter.toWindow();
                }
            }
        }
        //swipe direction
        else if (Input.touchCount > 0)
        {
            if (isZoomed && Input.touchCount >= 2) //pinch for unzoom
            {
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
                        dialogueCanva.enabled = false;
                        if (selectedCharacter != null) selectedCharacter.isAtWindow = false;

                        if (previousCameraPosition != default)
                            mainCamera.GetComponent<CameraScript>().SetNavigationData(previousCameraPosition, shouldZoom: false);
                        animatorWindow.SetFloat("Direction", -1);
                        animatorWindow.PlayInFixedTime("ParentAnim", -1, 1);
                    }
                }
                
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
                        SideEnum lastPosition = x > 0 ? SideEnum.Left : SideEnum.Right;
                        mainCamera.GetComponent<CameraScript>().SetNavigationData(lastPosition, shouldZoom: false);
                    }
                    //else{direction = y > 0 ? "Up" : "Down";}
                }
            }
        }
    }
}
