using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject[] spots;
    public float sightRange = 30.0f;
    public float chaseTime = 25.0f;
    public bool playerDetected;
    public Transform player;

    public int index;
    private NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        index = 0;
        playerDetected = false;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(spots[index].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray sight = new Ray(transform.position, transform.forward);
        if (!playerDetected)
        {
            if (spots[index].GetComponent<wayPoint>().isTriggered == true)
            {
                index++;
                if (index >= spots.Length)
                {
                    index = 0;
                }
                agent.SetDestination(spots[index].transform.position);
                agent.updateRotation = true;
            }
            else
            {
                agent.SetDestination(spots[index].transform.position);
            }

            if (Physics.Raycast(sight, out hit, sightRange))
            {
                if (hit.collider.tag.Equals("Player"))
                {
                    playerDetected = true;
                }
            }
        }
        else {
            agent.SetDestination(player.position);
            chaseTime -= Time.deltaTime;
            if (Physics.Raycast(sight, out hit, sightRange))
            {
                if (hit.collider.tag.Equals("Player"))
                {
                    chaseTime = 25.0f;
                }
            }
            if (chaseTime <= 0)
            {
                playerDetected = false;
                agent.SetDestination(spots[index].transform.position);
                chaseTime = 25.0f;
            }
        }

    }
}
