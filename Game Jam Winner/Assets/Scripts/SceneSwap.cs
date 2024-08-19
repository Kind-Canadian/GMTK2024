using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        Debug.Log("SceneSwapped!");
        //Needs to be activated once the correct scene has be finished. 
        //Just place the scene name where it says "SceneName"
        //SceneManager.LoadScene(sceneName: "SceneName");
    }
}
