using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class happiness : MonoBehaviour
{
    public ScaleTest test;
    public int hap;
    private bool tags = true;
    // Start is called before the first frame update
    void Start()
    {
        test = GameObject.FindGameObjectWithTag("scale").GetComponent<ScaleTest>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tags == true && gameObject.CompareTag("RScale")) 
        {
            hap = 5;
            tags = false;
        }
        if (tags == true && gameObject.CompareTag("LScale"))
        {
            hap = -5;
            tags = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (test.happiness == 50)
        {
            test.happiness += hap;
        }
    }
}
