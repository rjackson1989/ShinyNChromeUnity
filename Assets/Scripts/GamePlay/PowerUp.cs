using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public Transform box;
    public Transform boxPF;
    public float countdown = 3.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (box == null)
        {
            countdown -= Time.deltaTime;
            if(countdown <= 0f)
            {
                box = (Transform) Instantiate(boxPF, transform.position, transform.rotation);
                countdown = 3.0f;
            }
            
        }
	}
}
