
using UnityEngine;

public class Target : MonoBehaviour
{
    
    public float speed = 50f;
    public float retreatingDistance = 50f;
    public float stoppingDistance = 50f;
    private health myHealth;
    private float timebtwShots;
    public float startTimeBtwShots;

    public GameObject firingBullets;

    private Transform player;
    private Rigidbody targetRB;
    public ParticleSystem gunFire;
    public float range = 100f;
    public ParticleSystem impacctFire;

    private AudioSource audioSource;
    public AudioClip shootingClip;



    private void Start()
    {

        if (GameObject.FindGameObjectWithTag("SpaceJet"))
        {
            player = GameObject.FindGameObjectWithTag("SpaceJet").transform;

        }
        audioSource = GetComponent<AudioSource>();
        targetRB = GetComponent<Rigidbody>();

        timebtwShots = startTimeBtwShots;
        myHealth = GetComponent<health>();


    }



    private void Update()
    {

        Debug.Log(myHealth.healthValue);
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
            {
                var moveDirection = (player.position - transform.position);


                Quaternion direction = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, direction, speed * 0.1f * Time.deltaTime);

                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            }
            else if (Vector3.Distance(transform.position, player.position) < stoppingDistance &&
               Vector3.Distance(transform.position, player.position) > retreatingDistance)
            {
                transform.position = this.transform.position;
                if (timebtwShots <= 0)
                {

                    //var bullet = Instantiate(firingBullets);
                    //Destroy(bullet, 2f);
                    shoot();
                }

            }
            else if (Vector3.Distance(transform.position, player.position) < retreatingDistance)
            {




                var moveDirection = (player.position - transform.position);


                Quaternion direction = Quaternion.LookRotation(-moveDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, direction, speed * 0.1f * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);

            }

        }


    }

    public void shoot()
    {
      

        gunFire.Play();
        

        RaycastHit hit;
        if (Physics.Raycast(gunFire.transform.position, gunFire.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);



            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 10f);
            }
            impacctFire.transform.position = hit.point;
            // GameObject impactgo = Instantiate(impacctFire, hit.transform.position, Quaternion.LookRotation(-hit.normal));
            impacctFire.Play();
            health healthh = hit.transform.GetComponent<health>();
            if (healthh != null)
            {
                healthh.TakeDamage(5f);
            }

           // Destroy(impactgo, 2f);
        }



    }

}
