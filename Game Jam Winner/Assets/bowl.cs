using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.SetParent(GameObject.FindGameObjectWithTag("scale").transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
