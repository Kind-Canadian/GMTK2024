using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilding : MonoBehaviour
{
    public GameObject bottom;
    public GameObject pblock;
    public GameObject tblock;
    public GameObject gblock;
    public GameManager game;
    //The list effectively mimics the blocks order on screen just in a list instead
    //index 0 is the base of the building and the highest index is the top
    public List<GameObject> pBlocks = new List<GameObject>();
    public List<GameObject> tBlocks = new List<GameObject>();
    public List<GameObject> gBlocks = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        pBlocks.Add(GameObject.FindGameObjectWithTag("pBase"));
        tBlocks.Add(GameObject.FindGameObjectWithTag("tBase"));
        gBlocks.Add(GameObject.FindGameObjectWithTag("gBase"));
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            pbuild();
            tbuild();
            gbuild();
        }
        */
        //Debug.Log(Mathf.Floor(game.Pst_Population / 40));
        if(Mathf.Ceil(game.Pst_Population/40) > pBlocks.Count)
        {
            pbuild();
        }

        if (Mathf.Ceil(game.Thf_Population / 20) > tBlocks.Count)
        {
            tbuild();
        }

        if (Mathf.Ceil(game.Grd_Population / 10) > gBlocks.Count)
        {
            gbuild();
        }

        if (Mathf.Ceil(game.Pst_Population/40) < pBlocks.Count && pBlocks.Count > 1)
        {
            pDestroy();
        }
        if (Mathf.Ceil(game.Thf_Population / 20) < tBlocks.Count && tBlocks.Count > 1)
        {
            tDestroy();
        }
        if (Mathf.Ceil(game.Grd_Population / 10) < gBlocks.Count && gBlocks.Count > 1)
        {
            gDestroy();
        }

        if (!pBlocks[0].CompareTag("Base"))
        {
            
            pBlocks[0].tag = "Base";
        }
        if(!pBlocks[pBlocks.Count - 1].CompareTag("Top") && pBlocks.Count > 1) 
        {
            pBlocks[pBlocks.Count - 2].tag = "Middle";
            pBlocks[pBlocks.Count - 1].tag = "Top";
        }
        if (!tBlocks[0].CompareTag("Base"))
        {
            tBlocks[0].tag = "Base";
        }
        if (!tBlocks[tBlocks.Count - 1].CompareTag("Top") && tBlocks.Count > 1)
        {
            tBlocks[tBlocks.Count - 2].tag = "Middle";
            tBlocks[tBlocks.Count - 1].tag = "Top";
        }

        if (!gBlocks[0].CompareTag("Base"))
        {
            
            gBlocks[0].tag = "Base";
        }
        if (!gBlocks[gBlocks.Count - 1].CompareTag("Top") && gBlocks.Count > 1)
        {
            gBlocks[gBlocks.Count - 2].tag = "Middle";
            gBlocks[gBlocks.Count - 1].tag = "Top";
        }

    }
    void pbuild()
    {
        GameObject addon = Instantiate(pblock, new Vector3(pBlocks[0].transform.position.x, pBlocks[pBlocks.Count - 1].transform.position.y + 10, pBlocks[0].transform.position.z), pBlocks[0].transform.rotation, null);
        pBlocks.Add(addon);
    }
    void tbuild()
    {
        GameObject addon = Instantiate(tblock, new Vector3(tBlocks[0].transform.position.x, tBlocks[tBlocks.Count - 1].transform.position.y + 10, tBlocks[0].transform.position.z), tBlocks[0].transform.rotation, null);
        tBlocks.Add(addon);
    }
    void gbuild()
    {
        GameObject addon = Instantiate(gblock, new Vector3(gBlocks[0].transform.position.x, gBlocks[gBlocks.Count - 1].transform.position.y + 10, gBlocks[0].transform.position.z), gBlocks[0].transform.rotation, null);
        gBlocks.Add(addon);
    }

    void pDestroy()
    {
        GameObject des = pBlocks[0];
        pBlocks.RemoveAt(0);
        Destroy(des);
    }

    void tDestroy()
    {
        GameObject des = tBlocks[0];
        tBlocks.RemoveAt(0);
        Destroy(des);
    }

    void gDestroy()
    {
        GameObject des = gBlocks[0];
        gBlocks.RemoveAt(0);
        Destroy(des);
    }
}
