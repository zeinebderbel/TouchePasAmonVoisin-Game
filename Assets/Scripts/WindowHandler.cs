using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowHandler : MonoBehaviour
{
    Color startcolor;
    public Material mat;

    // Start is called before the first frame update
    void OnMouseEnter()
    {
        startcolor = gameObject.GetComponent<Renderer>().material.color;
        Color color = new Color(236,88,91,255);
        gameObject.GetComponent<Renderer>().material.color = mat.color;
    }
    void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.color = startcolor;
    }
}
