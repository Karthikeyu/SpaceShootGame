using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollider : MonoBehaviour
{

    public GameObject jet;


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.name == "PlayerJet")
        {
            Debug.Log("Entered into ring.");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "PlayerJet")
        {
            Debug.Log("Exited from ring.");
        }
    }



    private void OnTriggerEnter(Collider collision)
    {
       
        if (collision.name == "PlayerJet")
        {
           
            jet.GetComponent<spacem>().isTriggered = true;
            Debug.Log("Entered into ring.");
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.name == "PlayerJet")
        {

            Debug.Log("Exited from ring.");
        }
        
    }


}
