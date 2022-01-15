using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3[] Positions;
    public float Speed = 2.0f;

    private int mCurrentIndex = 0;
    private void Start()
    {
        
    }
    private void Update()
    {
        Vector3 currentPos = (Vector3)Positions.GetValue(mCurrentIndex);

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (mCurrentIndex < Positions.Length - 1)
                mCurrentIndex++;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (mCurrentIndex > 0)
                mCurrentIndex--;
        }
        transform.position = Vector3.Lerp(transform.position, currentPos, Speed * Time.deltaTime);
    }
}
