using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    public Vector3 BalconyToGoTo;
    public void OnMouseDown()
    {
        gameObject.transform.position = BalconyToGoTo;

    }
}
