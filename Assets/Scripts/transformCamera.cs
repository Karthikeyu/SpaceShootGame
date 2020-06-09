using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformCamera : MonoBehaviour
{

    public GameObject Jet;
    Vector3 jetPosition = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Jet != null)
        {

            jetPosition = Jet.transform.position;

            transform.rotation = Jet.transform.rotation;
            

            transform.position = new Vector3(jetPosition.x, jetPosition.y + 5, jetPosition.z - 30);
        }

    }
}
