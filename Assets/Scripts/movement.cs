using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    private Rigidbody rigidBody;
    public float speed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) )
        {
            Debug.Log("Space");
            //transform.position = new Vector3(transform.position.x, 100 * Time.deltaTime+ transform.position.y, transform.position.z);
            //rigidBody.AddRelativeForce(Vector3.up);
            
            transform.position = new Vector3(transform.position.x, speed * Time.deltaTime + transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //transform.position = new Vector3(transform.position.x, transform.position.y, 5 * Time.deltaTime + transform.position.z);
            rigidBody.AddRelativeForce(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
           // transform.position = new Vector3( transform.position.x, transform.position.y, transform.position.z - 50* Time.deltaTime); //vertical
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z );
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //  transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z + 10*Time.deltaTime);
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
    
}
