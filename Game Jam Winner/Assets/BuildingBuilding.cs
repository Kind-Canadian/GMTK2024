using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBuilding : MonoBehaviour
{
    public GameObject bottom;
    public GameObject block;
    //The list effectively mimics the blocks order on screen just in a list instead
    //index 0 is the base of the building and the highest index is the top
    public List<GameObject> blocks = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        blocks.Add(GameObject.FindGameObjectWithTag("Base"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            build();
        }
        if (Input.GetMouseButtonDown(1) && blocks.Count > 1)
        {
            GameObject des = blocks[0];
            blocks.RemoveAt(0);
            Destroy(des);
        }
        if (!blocks[0].CompareTag("Base"))
        {
            blocks[0].tag = "Base";
        }
        if(!blocks[blocks.Count - 1].CompareTag("Top") && blocks.Count > 1) 
        {
            blocks[blocks.Count - 2].tag = "Middle";
            blocks[blocks.Count - 1].tag = "Top";
        }
        
    }
    void build()
    {
        GameObject addon = Instantiate(block, new Vector3(blocks[0].transform.position.x, blocks[blocks.Count - 1].transform.position.y + 10, blocks[0].transform.position.z), Quaternion.identity, null);
        blocks.Add(addon);
    }
}
