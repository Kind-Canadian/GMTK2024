using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class music : MonoBehaviour
{

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
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
