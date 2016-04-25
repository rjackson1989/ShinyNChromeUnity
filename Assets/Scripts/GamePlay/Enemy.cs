using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    //private enum states { };
    private NavMeshAgent agent;
    public GameObject target;
    private GameObject[] playerList;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        playerList = GameObject.FindGameObjectsWithTag("Player");
        float firstDist = Vector3.Distance(transform.position, playerList[0].transform.position);
        float secDist = Vector3.Distance(transform.position, playerList[1].transform.position);

        if (secDist > firstDist)
        {
            target = playerList[0];
            
        }
        else
        {
            target = playerList[1];
        }
    }
	
	// Update is called once per frame
	void Update () {

        agent.SetDestination(target.transform.position);
    }
}
