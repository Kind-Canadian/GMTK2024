using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class musicSwag : MonoBehaviour
{
    private GameManager game;
    public bool FlipFlop = false;
    public AudioSource SongMain;
    public AudioSource SongSummary;
    

    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (game.SummaryUp == true && FlipFlop == false) {
            FlipFlop = true;
            SongMain.Stop();
            SongSummary.Play();
        }

        if (game.SummaryUp == false && FlipFlop == true) {
            FlipFlop = false;
            SongMain.Play();
            SongSummary.Stop();
        }

        if (game.DoFade) {
            SongSummary.volume = (0.1f + (0.9f - (Mathf.Sin(game.CardDelay*3.14f)*0.9f)) );
        }
    }

    public void funmute(bool muted)
    {
        
       
        if(muted)
        {
            
           gameObject.transform.position = new Vector3(80, 0, -1);
            AudioListener.volume = 0;

        }
        if(!muted)
        {
            
            gameObject.transform.position = new Vector3(80, 0, 1);
            
            AudioListener.volume = 1;
        }

    }
}
