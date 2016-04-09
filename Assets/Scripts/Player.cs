using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;
    public int playerNumber = 1;
    public Transform explosionPF;
    public Hashtable stats;
    // Use this for initialization
    void Start () {
        stats = new Hashtable();
        initStats();
	}

    private void initStats()
    {
        
    }

    // Update is called once per frame

    void Update()
    {
        float translation = Input.GetAxis("Vertical"+playerNumber) * speed;
        float rotation = Input.GetAxis("Horizontal" + playerNumber) * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        Quaternion q = transform.rotation;
        Vector3 forward = q * Vector3.forward;
        transform.Translate(-forward * translation);
        transform.Rotate(0, 0, rotation);
        
    }
}
