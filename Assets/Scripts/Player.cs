using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    public int speed = 30, health;
    public float rotationSpeed = 100.0F;
    public int maxSpeed = 60, maxHealth = 300;
    public int altAmmo = 10;


    public int playerNumber = 1;
    public Transform deathPF, hitPF;
    public minigun gun;
    // Use this for initialization
    void Start () {
        health = maxHealth;
	}

    // Update is called once per frame

    void Update()
    {
        if (health > 0)
        {
            float translation = Input.GetAxis("Vertical" + playerNumber) * speed;
            float rotation = Input.GetAxis("Horizontal" + playerNumber) * rotationSpeed;
            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;
            Quaternion q = transform.rotation;
            Vector3 forward = q * Vector3.forward;
            transform.Translate(-forward * translation);
            transform.Rotate(0, 0, rotation);

            if (Input.GetButtonDown("Fire" + playerNumber))
            {
                gun.fireBullet();
            }
            
        }
        if (health == 0)
        {
            Instantiate(deathPF, transform.position, Quaternion.identity);
            health -= 1;
        }
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag.Equals("SpeedUp"))
        {
            speed = maxSpeed;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("AmmoUp"))
        {
            altAmmo += 5;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("bullet"))
        {
            health -= 50;
            ContactPoint hit = collision.contacts[1];

            Destroy(collision.gameObject);
            Destroy(Instantiate(hitPF.gameObject, hit.point, Quaternion.identity),0.3f);
            
        }
    }
}
