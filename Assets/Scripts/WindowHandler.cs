using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHandler : MonoBehaviour
{
    Color startcolor;
    public Material mat;
    GameObject parent;
    private void Start()
    {
        parent = gameObject.transform.parent.gameObject;
    }
    // Start is called before the first frame update
    void OnMouseEnter()
    {
        startcolor = parent.GetComponent<Renderer>().material.color;
        parent.GetComponent<Renderer>().material.color = mat.color;
    }
    void OnMouseExit()
    {
        parent.GetComponent<Renderer>().material.color = startcolor;
    }
}
