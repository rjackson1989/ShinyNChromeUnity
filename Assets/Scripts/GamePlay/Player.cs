using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour {

    enum states
    {
        alive,
        dead,
        paused
    }
    //variable values
    public int speed = 30, health;
    public float rotationSpeed = 100.0F;
    public int maxSpeed = 60, maxHealth = 300, shotPower = 50;
    public int altAmmo = 10;
    public float respawnTime = 5.0f;
    public int currentLives = 5;
	public String[] weapons;

    //generic variables
	private Vector3 forward;
	private float translation;
    public int currentWeapon;
    private states currentState;

    //object components
    private Rigidbody rb;
    public int playerNumber = 1;
    public Transform deathPF, hitPF;
    public minigun gun;
    public Text UIText;
    public Text weaponText;
    public Text livesText;
    public GameObject body;
    public GameObject optionPanel;

    // Audio components
    public AudioClip impact;
    public AudioClip destroy;


    private AudioSource audioImpact;
    private AudioSource audioDestroy;

    // Use this for initialization
    void Start () {
        currentState = states.alive;
        health = maxHealth;
        updateUIText();
		rb = GetComponent<Rigidbody>();
        optionPanel.SetActive(false);
        audioImpact = GetComponent<AudioSource>();
        audioDestroy = GetComponent<AudioSource>();

	}
    
    // Update is called once per frame

    void Update()
    {

        if (!optionPanel.activeSelf) //not paused
        {
            if (body.activeSelf) //I haven't been killed
            {
                updateUIText();
                if (health > 0)
                {

                    calculateMovement();

                    if (Input.GetButtonDown("Select" + playerNumber))
                    {
                        currentWeapon++;
                        if (currentWeapon > 1)
                        {
                            currentWeapon = 0;
                        }
                    }
                    if (Input.GetButtonDown("Menu"))
                    {
                        optionPanel.SetActive(true);
                    }


                }

                if (health <= 0)
                {
                    UIText.text = "Player " + playerNumber + " Health: 0";
                    audioImpact.Stop();
                    audioDestroy.Play();
                    Destroy(Instantiate(deathPF.gameObject, transform.position, Quaternion.identity), 0.5f);
                    body.SetActive(false);
                    currentLives--;
                    health = maxHealth;
                }
            }
            else
            {
                rb.useGravity = false;
                rb.velocity = new Vector3(0, 0, 0);

                if (currentLives > 0) //no respawning if you run out of lives
                {
                    if (respawnTime >= 0)
                    {
                        respawnTime -= Time.deltaTime;
                        UIText.text = "Respawn in " + Mathf.Round(respawnTime) + " sec";
                    }
                    else
                    {
                        respawnTime = 5.0f;
                        rb.useGravity = true;
                        body.SetActive(true);
                    }
                }
                else
                {
                    updateUIText();
                    optionPanel.SetActive(true);
                }

            }

        }
        else if(currentLives > 0)
        {
            rb.velocity = new Vector3(0, 0, 0);
            if (Input.GetButtonDown("Cancel"))
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
                gun.fireBullet(currentWeapon, playerNumber);
            }
            
        }

    }

    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag.Equals("SpeedUp"))
        {
            gun.addBullet(1, 5);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("AmmoUp"))
        {
            gun.addBullet(0, 10);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("bullet" + playerNumber))
        { 
            //ignore
        }
        else if (collision.gameObject.tag.Contains("bullet"))
        {
            health -= shotPower;
            ContactPoint hit = collision.contacts[0];
            audioImpact.Play();
            Destroy(collision.gameObject);
            Destroy(Instantiate(hitPF.gameObject, hit.point, Quaternion.identity), 0.3f);

        }
    }
    private void updateUIText()
    {
        UIText.text = "Player " + playerNumber + " Health: " + health;
        weaponText.text = weapons[currentWeapon] + ": " + gun.getAmount(currentWeapon);
        livesText.text = "Lives: " + currentLives;
    }
    private void calculateMovement()
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
}
