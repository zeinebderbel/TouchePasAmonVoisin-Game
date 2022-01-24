using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    static Item currentItem;
    static Quest currentQuest;

    // Start is called before the first frame update
    void Start()
    {
        currentQuest = GameObject.FindObjectOfType<Quest>();
        var components = Array.ConvertAll(GameObject.FindObjectsOfType(typeof(Item), true), i => i as Component).ToList();
        currentQuest.ItemsGo = components.Select(i => i.gameObject).ToList();
        currentQuest.Collector = new Collector();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void CollectItem(Item item)
    {
        currentItem = item;
        currentQuest.CollectItem(item);
        ActivateNextItem();
    }
    static void ActivateNextItem()
    {
        if (currentItem.Id < currentQuest.ItemsGo.Count - 1)
        {
            currentQuest.ItemsGo.Where(i => i.GetComponent<Item>().Id == currentItem.Id + 1).Select(i => i).FirstOrDefault().gameObject.SetActive(true);
            
        }
        else
            if (currentItem == currentQuest.Target)
            Debug.Log("You win this quest!! Congrats");
    }
}
