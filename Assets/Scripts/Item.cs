using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Id;
    public string Name;
    GameManager gm;
    private void Start()
    {
        gm = (GameManager)GameObject.FindObjectOfType(typeof(GameManager));
    }
    private void OnMouseDown()
    {
        gm.CollectItem(this);
    }

}
