using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class GameManager : MonoBehaviour
{
    static Item currentItem;
    static Quest currentQuest;
    private Camera mainCamera;
    public Canvas winScreen;

    void Start()
    {
        currentQuest = GameObject.FindObjectOfType<Quest>();
        var components = Array.ConvertAll(GameObject.FindObjectsOfType(typeof(Item), true), i => i as Component).ToList();
        currentQuest.ItemsGo = components.Select(i => i.gameObject).ToList();
        currentQuest.Collector = new Collector();
        mainCamera = Camera.main;
        winScreen.enabled = false;
    }

    public void CollectItem(Item item)
    {
        currentItem = item;
        currentQuest.CollectItem(item);
        ActivateNextItem();
    }
     void ActivateNextItem()
    {
        if (currentItem.Id < currentQuest.ItemsGo.Count - 1)
        {
            currentQuest.ItemsGo.Where(i => i.GetComponent<Item>().Id == currentItem.Id + 1).Select(i => i).FirstOrDefault().gameObject.SetActive(true);
        }
        else if (currentItem == currentQuest.Target)
        {
            Debug.Log("You win this quest!! Congrats");
            mainCamera.GetComponent<CameraScript>().SetNavigationData(currentQuest.questVictim.gameObject.transform.position, shouldZoom: true);
            currentQuest.questVictim.GetComponent<Animator>().SetFloat("Direction", 1);
            currentQuest.questVictim.GetComponent<Animator>().PlayInFixedTime("ParentAnim", -1, 0);
            //currentQuest.Panel.SetActive(true);
            //Pause();
        }
    }
    public void affWinScreen(int dialNum)
    {
        if (currentQuest.Target.Id == dialNum)
        {
            Debug.Log("aff screen");
            winScreen.enabled = true;

        }
    }
    static void Pause()
    {
        Time.timeScale = 0;
    }

}
