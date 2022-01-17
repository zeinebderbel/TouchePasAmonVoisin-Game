using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCursor : MonoBehaviour
{
    private Camera mainCamera;
    RaycastHit hit;
    Vector3 previousCameraPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private bool IsCursorOverWindowUI()
    {
        if (GraphicRaycast() == null)
            return false;
        return GraphicRaycast().Value.transform.gameObject.tag.Equals("Window");
    }

    private RaycastHit? GraphicRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            return hit;
        }
        return null;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsCursorOverWindowUI())
            {
                previousCameraPosition = mainCamera.transform.position;
                mainCamera.GetComponent<CameraScript>().SetNavigationData(hit.transform.gameObject.GetComponent<Renderer>().bounds.center, shouldZoom: true);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (previousCameraPosition != null)
                mainCamera.GetComponent<CameraScript>().SetNavigationData(previousCameraPosition, shouldZoom: false);
        }
    }
}
