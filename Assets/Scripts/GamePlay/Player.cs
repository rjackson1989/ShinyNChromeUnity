using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour {

    public int speed = 30, health;
    public float rotationSpeed = 100.0F;
    public int maxSpeed = 60, maxHealth = 300, shotPower = 50;
    public int altAmmo = 10;
	private Rigidbody rb;
	private Vector3 forward;
	private float translation;
    public float respawnTime = 5.0f;
    public Text UIText;
    public GameObject body;
    public GameObject optionPanel;

    public int playerNumber = 1;
    public Transform deathPF, hitPF;
    public minigun gun;
    // Use this for initialization
    void Start () {
        health = maxHealth;
        UIText.text = "Player " + playerNumber + " Health: " + health;
		rb = GetComponent<Rigidbody>();
        optionPanel.SetActive(false);
	}

    // Update is called once per frame

    void Update()
    {
        if (!optionPanel.activeSelf)
        {
            if (body != null)
            {
                UIText.text = "Player " + playerNumber + " Health: " + health;
                if (health > 0)
                {

                    translation = Input.GetAxis("Vertical" + playerNumber) * speed;
                    float rotation = Input.GetAxis("Horizontal" + playerNumber) * rotationSpeed;
                    translation *= Time.deltaTime;
                    rotation *= Time.deltaTime;
                    Quaternion q = transform.rotation;
                    forward = q * Vector3.forward;
                    rb.velocity += forward * translation;

                    //transform.Translate(-forward * translation);
                    transform.Rotate(0, rotation, 0);



                }

                if (health <= 0)
                {
                    UIText.text = "Player " + playerNumber + " Health: 0";
                    Destroy(Instantiate(deathPF.gameObject, transform.position, Quaternion.identity), 0.5f);
                    Destroy(body);
                    health = maxHealth;
                }
            }
            else
            {
                rb.useGravity = false;
                optionPanel.SetActive(true);
            }

        }
        else
        {
            if (Input.GetButtonDown("Fire" + playerNumber))
            {
                optionPanel.SetActive(false);
            }


        }

    }
	void FixedUpdate()
	{
        if (!optionPanel.activeSelf)
        {
            if (Input.GetButtonDown("Fire" + playerNumber))
            {
                gun.fireBullet();
            }
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
