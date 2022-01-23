using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Id;
    public string Name;

    private void OnMouseDown()
    {
        GameManager.CollectItem(this);
    }

}
