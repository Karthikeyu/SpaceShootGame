using UnityEngine;

using UnityEngine.UI;

public class health : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip explosionClip;

    public float healthValue = 50f;
    public GameObject explosionBombEffect;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    public void TakeDamage(float amount)
    {


        healthValue -= amount;
       
        
        if (healthValue <= 0)
        {
            Die();

        }
    }

    

    private void Die()
    {


        var Bomb = Instantiate(explosionBombEffect, transform.position, transform.rotation);
        audioSource.PlayOneShot(explosionClip);
        Destroy(Bomb, 2f);
        Destroy(gameObject,1f);


    }
}
