using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndStats : MonoBehaviour
{
    public TMP_Text DaysSurvivedText;
    public TMP_Text DeathMessageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DaysSurvivedText.text = (Info.days).ToString();
        DeathMessageText.text = (Info.deathmessage).ToString();
    }
}
