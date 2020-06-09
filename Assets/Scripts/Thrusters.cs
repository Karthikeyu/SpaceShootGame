using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Thrusters : MonoBehaviour
{

    private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  Activate(bool activ = true)
    {
        if (activ)
        {
            
            tr.enabled = activ;
        }else
        {
            tr.enabled = false;
        }
    }
}
