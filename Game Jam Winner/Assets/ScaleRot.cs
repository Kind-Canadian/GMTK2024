using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleRot : MonoBehaviour
{

    private Rigidbody rb;
    public Rigidbody rb1;
    public Rigidbody rb2;
    private float bouncetime;
    private bool canbounce = true;
    // Start is called before the first frame update
    void Start()
    {
        //transform.rotation = Quaternion.identity;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Need to prevent the pendilum from moving too far after it has it's velocity has been reversed
        //if you hit the angle cap then reverse the angular velocity to make it bounce back

        //transform.rotation = Quaternion.Euler(0, 0, rot);
        //Debug.Log(rb.angularVelocity.z);
        /*
        if(transform.rotation.z >= 10 * Mathf.Deg2Rad || transform.rotation.z <= -10 * Mathf.Deg2Rad && rb.angularVelocity.z > 0.2f &&canbounce == true)
        {
            Debug.Log("shit");
            rb.angularVelocity = rb.angularVelocity * -5f;
            canbounce = false;
            bouncetime = 60;
        }
        */
        /*
        if (transform.rotation.z >= 10 * Mathf.Deg2Rad || transform.rotation.z <= -10 * Mathf.Deg2Rad && rb.angularVelocity.z < 0.2f )
        {
            if(transform.rotation.z < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, -20);
            }
            if (transform.rotation.z > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 20);
            }
        }
        */
        if (bouncetime > 0)
        {
            bouncetime--;
        }
        if(bouncetime <= 0)
        {
            rb.angularVelocity = rb.angularVelocity * 0.5f;
            canbounce = true;
        }
        /*
        if(rb1.mass == rb2.mass )
        {
            if(transform.rotation.z < 0.002f && transform.rotation.z > -0.002f)
            {
                //Debug.Log("Pain");
                rb.angularVelocity = Vector3.forward * 0;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            

            
            if (rb.angularVelocity.z < 5f && rb.angularVelocity.z > 0.002f)
            {
                //Debug.Log("Fun");
                rb.angularVelocity = Vector3.forward * -0.1f;
            }
            if (rb.angularVelocity.z > -5f && rb.angularVelocity.z < -0.002f)
            {
                //Debug.Log("Fun");
                rb.angularVelocity = Vector3.forward * -0.1f;
            }

        }
        */

    }
}
