using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Vector3[] Positions;
    public float Speed = 2.0f;
    private int mCurrentIndex = 0;
    private int lastIndex = 1;
    private SideEnum sideToNavigateTo;

    private void Start()
    {
        lastIndex = Positions.Length - 1;
    }
    private void Update()
    {
        if (sideToNavigateTo == SideEnum.Right)
        {
            mCurrentIndex = lastIndex;
        }
        if (sideToNavigateTo == SideEnum.Left)
        {
            mCurrentIndex = 0;
        }
        Vector3 currentPos = Positions[mCurrentIndex];

        transform.position = Vector3.Lerp(transform.position, currentPos, Speed * Time.deltaTime);
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
