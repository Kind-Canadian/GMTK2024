using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTest : MonoBehaviour
{
    //could calculate the weight for each side when they are added (as a way to prevent the scale from not moving when there is two different weights)
    //more specifically for the system that prevents the scale from wandering when weights are equal
    //if the scale is for happiness (assuming that it is a percentage) 50% could be equal, not happy or sad, with above or below going on either side
    private Rigidbody rb;
    public GameObject weight;
    public float happiness = 50;
    [SerializeField]float rot;
    [SerializeField] private List<GameObject> happyweights = new List<GameObject>();
    [SerializeField] private List<GameObject> sadweights = new List<GameObject>();
    public Transform LScale;
    public Transform RScale;
    private float randx;
    private float randz;
    // Start is called before the first frame update
    void Start()
    {
        /*
        rb = gameObject.GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
        rb.solverIterations = 60;
        */
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation has to be x and not z because the bar is rotated 90 due to it facing the wrong direction
        /*
        if(rb.rotation.z < 0.5f * Mathf.Rad2Deg && rb.rotation.z > -0.5f * Mathf.Rad2Deg && happiness == 50)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        */
        if(happiness > 50 && transform.rotation.z > -10 * Mathf.Deg2Rad)
        {
            //Debug.Log("EGFHOISDFGHUSDFGLU");
            rot = Mathf.Lerp(transform.rotation.z, 1, 5 * Time.deltaTime);
            transform.Rotate(Vector3.forward * -40 * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.rotation.z), Quaternion.Euler(0, 0, -20),01f);
        }
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 20), 0.5f);
        if (happiness < 50 && transform.rotation.z < 10 * Mathf.Deg2Rad)
        {
            rot = Mathf.Lerp(-1f, 20, 5 * Time.deltaTime);
            transform.Rotate(Vector3.forward * 40 * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, 0, transform.rotation.z), Quaternion.Euler(0, 0, 20), 01f );
        }
        if(happiness == 50 && transform.rotation.z != 0)
        {
            transform.Rotate(Vector3.forward * transform.rotation.z * Mathf.Rad2Deg* -2 * Time.deltaTime);
        }
        //adds the weights to the corresponding sides if there are no blcoks to be removed from the other side
        if (Input.GetKeyDown(KeyCode.Q) && happiness <= 50)
        {
            randx = Random.Range(-0.25f, 0.25f);
            randz = Random.Range(-0.25f, 0.25f);
            GameObject sad = Instantiate(weight, new Vector3(LScale.position.x + randx,LScale.position.y - 1,LScale.position.z + randz), Quaternion.identity);
            sadweights.Add(sad);
            //make the happiness go up/down after the first block has touched it
            if(happiness < 50)
            {
                happiness -= 5;
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.Q) && happiness > 50)
        {
            Destroy(happyweights[0]);
            happyweights.RemoveAt(0);
            happiness -= 5;
        }
        if (Input.GetKeyDown(KeyCode.E) && happiness >= 50)
        {
            randx = Random.Range(-0.25f, 0.25f);
            randz = Random.Range(-0.25f, 0.25f);
            GameObject happy = Instantiate(weight, new Vector3(RScale.position.x, RScale.position.y - 1, RScale.position.z), Quaternion.identity);
            happyweights.Add(happy);
            if(happiness > 50)
            {
                happiness += 5;
            }
            
        }
        else if(Input.GetKeyDown(KeyCode.E) && happiness < 50)
        {
            Destroy(sadweights[0]);
            sadweights.RemoveAt(0);
            happiness += 5;
        }
    }
}
