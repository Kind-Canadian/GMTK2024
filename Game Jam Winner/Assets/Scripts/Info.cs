using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info : MonoBehaviour
{
    public static int days;
    public static string deathmessage = "uhh oopsie this broke!";
    public TextMeshProUGUI tmp;
    public GameObject textmesh;
    public int test = 5;
    //public Text number;
    // Start is called before the first frame update
    void Start()
    {
        tmp = textmesh.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = test.ToString();
    }
}
