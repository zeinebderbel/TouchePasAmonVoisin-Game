using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAmoving : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject zone;
    public GameObject window;
    private float timer;
    public float speed;
    public bool isAtWindow;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        isAtWindow = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        //not moving
        if (!(agent.hasPath))
        {
            GetComponent<Animator>().SetBool("isMoving", false);
        }

        if (!(agent.hasPath) && !(isAtWindow))
        {
            if (Random.value < 0.001f)
            {
                isMovingRand();
            }
        }
    }

	void isMovingRand() {
        float randX = Random.Range(zone.transform.position.x - 2, zone.transform.position.x + 2);
        float randZ = Random.Range(zone.transform.position.z - 1, zone.transform.position.z + 1);
        agent.SetDestination(new Vector3(randX, zone.transform.position.y, randZ));
        GetComponent<Animator>().Play("Walk");
        GetComponent<Animator>().SetBool("isMoving", true);
    }
    

    public void toWindow()
    {
        isAtWindow = true;
        agent.SetDestination(window.transform.position);
        GetComponent<Animator>().Play("Walk");
        GetComponent<Animator>().SetBool("isMoving", true);
    }
}
