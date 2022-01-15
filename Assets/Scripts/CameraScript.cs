using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3[] Positions;
    public float Speed = 2.0f;
    private int mCurrentIndex = 0;
    private int lastIndex = 1;

    private void Start()
    {
        lastIndex = Positions.Length - 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mCurrentIndex = lastIndex;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            mCurrentIndex = 0;
        }
        Vector3 currentPos = Positions[mCurrentIndex];

        transform.position = Vector3.Lerp(transform.position, currentPos, Speed * Time.deltaTime);
    }
}
