using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    private Camera mainCamera;
    RaycastHit hit;
    Vector3 previousCameraPosition;
    Animator animator;

    float animSpeed = 1;
    float animDirection = 1;

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
                animator = hit.transform.parent.gameObject.GetComponentInParent<Animator>();
                previousCameraPosition = mainCamera.transform.position;
                mainCamera.GetComponent<CameraScript>().SetNavigationData(hit.transform.gameObject.GetComponent<Renderer>().bounds.center, shouldZoom: true);
                animator.enabled = true;
                animator.SetFloat("Direction", 1);
                animator.PlayInFixedTime("ParentAnim", -1, 0);
            }
        }        
        else if (Input.GetMouseButtonDown(2) && mainCamera.GetComponent<CameraScript>().IsZoomed)
        {
            if (previousCameraPosition != null)
                mainCamera.GetComponent<CameraScript>().SetNavigationData(previousCameraPosition, shouldZoom: false);
            animator.SetFloat("Direction",-1);
            animator.PlayInFixedTime("ParentAnim", -1, 1);
        }
    }
}
