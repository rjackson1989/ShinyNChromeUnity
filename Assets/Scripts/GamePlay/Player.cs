using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour {

  
    //variable values
    public int speed = 30, health;
    public float rotationSpeed = 100.0F;
    public int maxSpeed = 60, maxHealth = 300, shotPower = 30;
    public int altAmmo = 10;
    public float respawnTime = 5.0f;
    public int currentLives = 5;
	public String[] weapons;

    //generic variables
	private Vector3 forward;
	private float translation;
    public int currentWeapon;

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
    public bool isHit = false;
    // Audio components
    private AudioSource audioImpact;
    private AudioSource audioDestroy;
    private AudioSource audioRev;
    private AudioSource audioAmmo;
    private AudioSource audioHealth;


    // Use this for initialization
    void Start () {
        health = maxHealth;
        updateUIText();
		rb = GetComponent<Rigidbody>();
        optionPanel.SetActive(false);
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioImpact = audioSources[0];
        audioDestroy = audioSources[1];
        audioRev = audioSources[2];
        audioAmmo = audioSources[3];
        audioHealth = audioSources[4];
        audioRev.Play();
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
            audioAmmo.Play();
            gun.addBullet(1, 5);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("AmmoUp"))
        {
            audioAmmo.Play();
            gun.addBullet(0, 10);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("HealthUp"))
        {
            audioHealth.Play();
            
            health += 30;
            if (health > maxHealth)
            {
                health = 300;
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("bullet" + playerNumber))
        {

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
