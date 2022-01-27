using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float Speed = 2.0f;
    public SideEnum sideToNavigateTo;

    private Vector3 windowPosition;
    public Vector3 RightBuildingPosition;
    public Vector3 LeftBuildingPosition;
    private Vector3 PositionToNagivateTo;

    //zoom
    public int zoom = 20;
    public int normal = 41;
    public float smooth = 5;
    public bool isZoomed;

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
        IsZoomed = shouldZoom;
        sideToNavigateTo = side;
    }

    private void Update()
    {
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
