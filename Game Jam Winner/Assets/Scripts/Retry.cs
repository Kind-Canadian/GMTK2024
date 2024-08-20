using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwapScene()
    {
        //Will Give an error if used
        //have to go to the build settings under file and do some stuff there
        //I didn't do it now because I feared that it could cause a merge error with Brennen that could not be resolved 
        //due to him not having access to the scene that I am trying to add to the build
        //SceneManager.LoadScene("TheMainMenu");
    }
}
