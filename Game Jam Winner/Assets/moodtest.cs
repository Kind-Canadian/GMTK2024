using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class moodtest : MonoBehaviour
{
    public GameObject img;
    public RawImage gay;
    public Texture some;
    // Start is called before the first frame update
    void Start()
    {
        gay = img.GetComponent<RawImage>();
        gay.texture = some;
    }

    // Update is called once per frame
    void Update()
    {
        gay.texture = some;
    }
}
