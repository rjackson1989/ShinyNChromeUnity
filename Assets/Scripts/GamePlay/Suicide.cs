using UnityEngine;
using System.Collections;

public class Suicide : MonoBehaviour {

	void Update()
	{
		
	}
	void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
	}
}
