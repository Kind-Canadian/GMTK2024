using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hang : MonoBehaviour
{
    public Transform hanging;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.CompareTag("LScale"))
        {
            transform.localPosition = new Vector3(-3, 0, 0);
        }
        if (gameObject.CompareTag("RScale"))
        {
            transform.localPosition = new Vector3(3, 0, 0);
        }
        if (gameObject.CompareTag("scale"))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localPosition = Vector3.zero;
        }
        
        //transform.Translate(new Vector3(hanging.position.x, hanging.position.y - 4, hanging.position.z));
        //transform.position = new Vector3(hanging.position.x,hanging.position.y - 4 ,hanging.position.z);
    }
}
