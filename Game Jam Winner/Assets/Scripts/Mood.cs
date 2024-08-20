using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mood : MonoBehaviour
{
    public Texture[] moods;
    public Texture test;

    public GameManager game;
    public RawImage peasant;
    public RawImage thief;
    public RawImage guard;
    // Start is called before the first frame update
    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        peasant.texture = test;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Mathf.Floor(5 - (game.Pst_Happiness / 20)));
        
        //Debug.Log(game.Grd_Happiness);
        switch (Mathf.Floor(5 - (game.Pst_Happiness / 20)))
        {
            case 0:
                peasant.texture = moods[0];
                break;
            case 1:
                peasant.texture = moods[1];
                break;
            case 2:
                peasant.texture = moods[2];
                    break;
            case 3:
                peasant.texture = moods[3];
                break;
            case 4:
                peasant.texture = moods[4];
                break;
            default:
                break;
        }

        switch (Mathf.Floor(5 - (game.Thf_Happiness / 20)))
        {
            case 0:
                thief.texture = moods[0];
                break;
            case 1:
                thief.texture = moods[1];
                break;
            case 2:
                thief.texture = moods[2];
                break;
            case 3:
                thief.texture = moods[3];
                break;
            case 4:
                thief.texture = moods[4];
                break;
            default:
                break;
        }

        switch (Mathf.Floor(5 - (game.Grd_Happiness / 20)))
        {
            case 0:
                guard.texture = moods[0];
                break;
            case 1:
                guard.texture = moods[1];
                break;
            case 2:
                guard.texture = moods[2];
                break;
            case 3:
                guard.texture = moods[3];
                break;
            case 4:
                guard.texture = moods[4];
                break;
            default:
                break;
        }
    }
}
