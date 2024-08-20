using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Day just needs to be replaced with the number of decisions and also tie the changing of the time of day when the decision is made instead of with mouse0
    private GameManager game;
    private float rot = -5;
    public int day = 10;
    private float shit;
    private Camera cam;
    private Light sunlight;
    private bool lighton = true;
    private Quaternion origin;
    private int prevcardcount = 0;
    private int prevdaycount = 0;
    public float DayEasing;
    
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.rotation;
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cam = Camera.main;
        sunlight = GetComponent<Light>();
        day = game.DailyCards - 1;
        prevdaycount = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //transform.rotation = Quaternion.Euler(transform.rotation.x * Mathf.Rad2Deg + 20, 0, 0);
        if (prevcardcount < game.CardsUsed)
        {
            DayEasing += 1;
            
            //shit = transform.rotation.x;
            
            day--;
            prevcardcount++;
        }

        if (DayEasing > 0) {
            DayEasing -= Time.deltaTime / 2;
            Quaternion initrot = transform.localRotation;
            rot = (-190 / game.DailyCards * Time.deltaTime / 2);
            transform.localRotation = initrot * Quaternion.Euler(0, rot, 0);
        }

        if (prevdaycount < game.Day) 
        {
            day = -2;
            prevdaycount = game.Day;
        }

        if(lighton)
        {
            sunlight.intensity = 1;
        }
        else
        {
            sunlight.intensity = 0;
        }
        if(day == -1 && DayEasing <= 0) // Nighttime
        {
            sunlight.intensity = 0;
            cam.clearFlags = CameraClearFlags.SolidColor;
        }
        if (day == -2) // Back to daytime
        {
            sunlight.intensity = 1;
            transform.rotation = origin;
            cam.clearFlags = CameraClearFlags.Skybox;

            day = game.DailyCards - 1;
            prevcardcount = 0;
        }
    }
}
