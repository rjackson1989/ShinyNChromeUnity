using UnityEngine;
using System.Collections;

public class Suicide : MonoBehaviour {

	void Update()
	{
		//Destroy(gameObject, 0.5f);
	}
	void OnCollisionEnter(Collision collision)
	{
		Destroy(gameObject);
	}
}
