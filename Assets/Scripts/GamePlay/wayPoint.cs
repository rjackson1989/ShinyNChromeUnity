using UnityEngine;
using System.Collections;

public class wayPoint : MonoBehaviour {
	public bool isTriggered;
	// Use this for initialization
	void Start () {
		isTriggered = false;
	}
	
	/**
	 * When the NPC steps on this point, they get a message that says "you stepped here"
	 */
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag.Equals ("Enemy")) {
			isTriggered = true;
		}
	}
	//When isTriggered is true, the enemy moves on to the next waypoint so turn this one 'off'
	void OnTriggerExit(Collider other){
		isTriggered = false;
	}
}
