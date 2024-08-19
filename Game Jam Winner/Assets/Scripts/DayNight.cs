using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Day just needs to be replaced with the number of decisions and also tie the changing of the time of day when the decision is made instead of with mouse0
    private GameManager game;
    private int rot = -5;
    private int day = 10;
    private float shit;
    private Camera cam;
    private Light sunlight;
    private bool lighton = true;
    private Quaternion origin;
    
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.rotation;
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cam = Camera.main;
        sunlight = GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.rotation = Quaternion.Euler(transform.rotation.x * Mathf.Rad2Deg + 20, 0, 0);
        if (Input.GetMouseButtonDown(0))
        {
            Quaternion initrot = transform.localRotation;
            //shit = transform.rotation.x;
            rot = (-190 / 10);
            transform.localRotation = initrot * Quaternion.Euler(0, rot, 0);
            day--;
            
        }
        if(lighton)
        {
            sunlight.intensity = 1;
        }
        else
        {
            sunlight.intensity = 0;
        }
        if(day == -1)
        {
            sunlight.intensity = 0;
        }
        if (day == -2)
        {
            sunlight.intensity = 1;
            transform.rotation = origin;

            day = 10;
        }
    }
}
