using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int Id;
    public string Name;
    [HideInInspector]
    public Collector Collector;
    public Item Target;
    [HideInInspector]
    public List<GameObject> ItemsGo;
    public GameObject questVictim;

    public GameObject Panel;

    public void CollectItem(Item item)
    {
        if (!Collector.Collect(item))
            Debug.LogError("Wrong order of item to collect!!");
        else
        {
            gameObject.GetComponent<DialogueManager>().triggerDialogue(item.Id);
            if (item.transform.gameObject.TryGetComponent<IAmoving>(out IAmoving mov))
            {
                mov.toWindow();
            }
        }
    }
}
