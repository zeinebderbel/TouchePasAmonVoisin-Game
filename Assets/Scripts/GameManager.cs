using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Item currentItem;
    Quest currentQuest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CollectItem(Item item)
    {
        this.currentItem = item;
        currentQuest.CollectItem(item);
    }
}
