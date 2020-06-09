using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformCamera : MonoBehaviour
{

    public Transform playerTransform;
    private Transform myTransform;
    Vector3 jetPosition = new Vector3();
    Vector3 myDefaultPos = new Vector3(0,20,-50);
    public float distanceDamp = 10f;
    public Vector3 velocity = Vector3.one;

    public float rotationalDamp = 10f;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerTransform != null)
        {

            /// 1st way- not smooth
            //jetPosition = playerTransform.transform.position;
            //transform.rotation = playerTransform.transform.rotation;
            //transform.position = new Vector3(jetPosition.x, jetPosition.y + 20, jetPosition.z - 50);


            /// 2nd way- good smooth- works for all objects and cameras also. But ideally not the best for camera movement
         /*   Vector3 toPos = (playerTransform.position) +  playerTransform.rotation * myDefaultPos;

            Vector3 currentPos = Vector3.Lerp(myTransform.position, toPos, distanceDamp * Time.deltaTime);

            myTransform.position = currentPos;

            Quaternion toRot = Quaternion.LookRotation(playerTransform.position - myTransform.position, playerTransform.up);

            Quaternion currentRot = Quaternion.Slerp(myTransform.rotation, toRot, rotationalDamp * Time.deltaTime);
            myTransform.rotation = currentRot;*/


            /// 3rd way- best for cameras

            Vector3 toPos = playerTransform.position + (playerTransform.rotation * myDefaultPos);
            Vector3 currentPos = Vector3.SmoothDamp(myTransform.position, toPos, ref velocity, distanceDamp);
            myTransform.position = currentPos;
            myTransform.LookAt(playerTransform,playerTransform.up);

            


        }

    }
}
