using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class spacem : MonoBehaviour
{

    float roll; // z- axis
    float pitch; // x-axis
    float yaw; // y-axis
    public float speed = 0f;
    public float turnspeed = 50f;
    public Joystick joyStick;
    //public Animator animator;
    public GameObject ring;
    private bool isShootButtonPressed  = false;
    public Text SpeedUi;
    public GameObject explosionEffect;
    private Rigidbody rigidbodyJet;
    private int seed = 0;
    private bool isRinginstatiated = false;
    public bool isTriggered = false;
    private GameObject rng;
    public Camera[] cameras;
    public Text playerHealthBarText;
    public float damage = 10f;
    public float range = 100f;
    public ParticleSystem gunFire;
    public GameObject impacctFire;

    public float FireRate = 100f;
    private health myHealth;
    private float nextTimeToFire = 0f;
    public GameObject enemy;
    private Vector3 enemyPosition = new Vector3();
    public int totalenemies = 10;

    private AudioSource audioSource;
    public AudioClip shootingClip;
    public AudioClip explosionClip;
    public Image crossHairShooting;
    public GameObject[] thrusters;

    public Text scoreText;
    private int  score = 0;
  

    private GameObject[] enemies;


    private TriggerCollider triggerCollider;

    // Start is called before the first frame update
    void Start()
    {
        
        UnityEngine.Random.InitState(seed);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        rigidbodyJet = GetComponent<Rigidbody>();
        triggerCollider = ring.GetComponent<TriggerCollider>();
        audioSource = GetComponent<AudioSource>();
        myHealth = GetComponent<health>();
        playerHealthBarText.text = "Health: " + myHealth.healthValue;
        if (enemy != null)
        {
            for (int i = 0; i < totalenemies; i++)
            {
               // enemies[i] = new GameObject();
                instatiateEnemy();
            }
        }
        crossHairShooting.enabled = false;
       

       // ringPrefab = Resources.Load("Prefabs/ring") as GameObject;
        // animator = GetComponent<Animator>();
        //  animator = gameObject.GetComponent<Animator>();

        

    }

    private void instatiateEnemy()
    {
        
        enemyPosition = UnityEngine.Random.insideUnitSphere * 1000;
        Instantiate(enemy, enemyPosition, transform.rotation);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        seed += 1 ;

        

        Turn();
        if(speed>0)
            Thrust();
        
        //if(enemies != null && enemies.Length < 10)
        //{
        //    instatiateEnemy();
        //}


        Debug.Log(isTriggered);


        // shooting
        //if(Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        //        {
        //            nextTimeToFire = Time.time + 1 / FireRate;
        //            shoot();
        //        }

        if (isShootButtonPressed)
            shoot();
        if (myHealth.healthValue > 0)
        {
            playerHealthBarText.text = "Health: " + myHealth.healthValue;
        }else
        {
            playerHealthBarText.text = "Health: 0";
        }
        if (score > 0)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            scoreText.text = "Score: 0";
        }


        if (isTriggered == true)
        {

            // Destroy(ring);
            score += 1;
            isTriggered = false;
            instantiateRing();
            
        }


  
    }

    public void shoot()
    {
        
        
        gunFire.Play();
        audioSource.PlayOneShot(shootingClip);
        RaycastHit hit;
     
       // Debug.DrawRay(gunFire.transform.position,  gunFire.transform.forward, Color.yellow );
    
        if (Physics.Raycast(gunFire.transform.position, gunFire.transform.forward, out hit, range))
        {
            Debug.Log("Hit");
            crossHairShooting.enabled = true;


            Debug.Log(hit.transform.name);



            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 5f);
            }
            GameObject impactgo = Instantiate(impacctFire, hit.transform.position, Quaternion.LookRotation(-hit.normal));

            health healthh = hit.transform.GetComponent<health>();
            if (healthh != null)
            {
                healthh.TakeDamage(damage);
            }

            Destroy(impactgo, 2f);
            Invoke("disableCrossHair", 1f);
        }







    }

    private void disableCrossHair()
    {
        crossHairShooting.enabled = false;
    }

    private void Turn()
    {


        if (joyStick.Vertical > 0.3f || joyStick.Vertical < -0.3f || joyStick.Horizontal > 0.3f || joyStick.Horizontal < -0.3f)
        {
          
            

            pitch = joyStick.Vertical * turnspeed * Time.deltaTime;
            yaw = joyStick.Horizontal * turnspeed * Time.deltaTime;
            //roll = Input.GetAxis("Roll") * turnspeed * Time.deltaTime;
            transform.Rotate(-pitch, yaw, transform.rotation.z);
           
        }


    
        SpeedUi.text = "Speed: " + speed;
    }

    private void Thrust()
    {

        for (int i = 0; i < thrusters.Length; i++)
        {
            if (thrusters[i].GetComponent<Thrusters>() != null)
            {
                thrusters[i].GetComponent<Thrusters>().Activate(true);


             
            }


        }
        transform.position += transform.forward * speed * Time.deltaTime ;

    }

   

    private void OnParticleCollision(GameObject other)
    {

        Debug.Log("Collision");

        // explosion

        var Bomb = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(Bomb, 2f);

        // Remove ParticleRIngs and current jet

        rigidbodyJet.AddExplosionForce(5f, transform.position, 150);
        Destroy(other);

        if(!cameras[1].enabled)
            cameras[1].enabled = true;
        audioSource.PlayOneShot(explosionClip);
        Destroy(this.gameObject);


        // Destroy(explosionEffect);





    }

   


    public void changeSpeed()
    {
        speed += 0.5f;
    }


    public void pointerDown()
    {

       
        isShootButtonPressed = true;
    }

    public void pointerUp()
    {
        
       
        isShootButtonPressed = false;
        if (gunFire.isPlaying)
            gunFire.Stop();
        //speed = 0f;

    }


    public void adjustSpeed(float newSpeed)
    {
        speed = newSpeed;
    }


    public void instantiateRing()
    {

        UnityEngine.Random.InitState(seed);
        Vector3 ringPosition = new Vector3();
        Vector3 ringScale = ring.transform.localScale;

        float a = UnityEngine.Random.value;
        Vector2 point;

        Vector2 intialRange = new Vector2(300, 450);

        if (a % 2 == 0)
        {
            if (score <= 10)
            {
                point =  UnityEngine.Random.insideUnitCircle * 150; 
                ringPosition = new Vector3(transform.position.x + point.x, transform.position.y + point.y, transform.position.z + UnityEngine.Random.Range(intialRange.x, intialRange.y));
            }else
            {
                point = UnityEngine.Random.insideUnitCircle * 100;
                ringPosition = new Vector3(transform.position.x + point.x, transform.position.y + point.y, transform.position.z + UnityEngine.Random.Range(intialRange.x, intialRange.y));

            }

        }
        else
        {
            if (score <= 10)
            {
                point = UnityEngine.Random.insideUnitCircle * 150;
                ringPosition = new Vector3(transform.position.x - point.x, transform.position.y - point.y, transform.position.z + UnityEngine.Random.Range(intialRange.x, intialRange.y));
            }
            else
            {
                point = UnityEngine.Random.insideUnitCircle * 100;
                ringPosition = new Vector3(transform.position.x - point.x, transform.position.y - point.y, transform.position.z + UnityEngine.Random.Range(intialRange.x, intialRange.y));

            }
        }

        ringScale = new Vector3(UnityEngine.Random.Range(0.3f,1f), UnityEngine.Random.Range(0.3f, 1f), UnityEngine.Random.Range(0.3f, 1f));

        ring.transform.localScale = ringScale;

        if (ring != null)
            ring.transform.position = ringPosition;
        

        
        //Instantiate(ringPrefab, transform.position, transform.rotation);
    }


   
}
