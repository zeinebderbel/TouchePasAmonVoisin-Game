using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Collector 
{
    List<Item> collectedItems;
    void Initialize()
    {
        this.collectedItems = new List<Item>();
    }

    public Collector()
    {
        Initialize();
    }
    public bool Collect(Item item)
    {
        if ((collectedItems.Count != 0 && item.Id > collectedItems.LastOrDefault().Id) 
            || (collectedItems.Count == 0 && item.Id == 0))
        {
            collectedItems.Add(item);
            return true;
        }

        else
            return false;
    }
    
}
