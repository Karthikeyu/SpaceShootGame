using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var moveDirection = (player.transform.position - transform.position);
        Debug.Log(moveDirection);

        Quaternion direction = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, direction, 10f * Time.deltaTime);


        //targetRB.MoveRotation(Quaternion.Euler(moveDirection.x, moveDirection.y, moveDirection.z) * targetRB.rotation);


       // targetRB.velocity = moveDirection * speed * Time.deltaTime;
    }
}
