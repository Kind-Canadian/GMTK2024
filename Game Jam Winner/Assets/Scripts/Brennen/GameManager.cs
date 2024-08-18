using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public float Pst_Population = 80; // ID = 1.1
    public float Pst_Happiness = 80; // ID = 1.2
    public float Pst_PopGainRate = 20; // ID = 1.3
    public float Pst_TaxRate = 0.5f; // ID = 1.4
    
    public float Grd_Population = 15; // ID = 2.1
    public float Grd_Happiness  = 80; // ID = 2.2
    public float Grd_PayRate = 6; // ID = 2.3
    
    public float Thf_Population = 10; // ID = 3.1
    public float Thf_Happiness = 50; // ID = 3.2

    public float Money = 100; // ID 4.1

    public string MainText;
    public string Choice1_Text;
    public string Choice2_Text;
   
    public int CardNumber;

    public TMP_Text MainTextObject;
    public TMP_Text Choice1_TextObject;
    public TMP_Text Choice2_TextObject;
    public TMP_Text Choice1_MoneyChangeText;
    public TMP_Text Choice2_MoneyChangeText;
    public TMP_Text MoneyText;


    public List<float[]> Choice1_Effects = new List<float[]>();
    public List<float[]> Choice2_Effects = new List<float[]>();

    public GameObject CardMenuObject;
    public bool ShowCardMenu;

    public List<int> CardPool;

    private int CardsPerDay;
    public float CardsPickedUp;

    public string KingdomName = "Shitsenburg";


    // Start is called before the first frame update
    
    
    
    
    void Start()
    {
        SetCard(0);
        ShowCardMenu = false;

        //CardPool.Add(0);
        //CardPool.Add(1);
        CardPool.Add(4);
        CardPool.Add(5);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space) && !ShowCardMenu) {
            ShowCard();
        }

        CardMenuObject.SetActive(ShowCardMenu);
        MoneyText.text = (Money + "Shillings");
    }

    public void ShowCard()
    {
        if (CardPool.Count != 0) {
            CardNumber = Random.Range(0, CardPool.Count);
            SetCard(CardPool[CardNumber]);
            MainTextObject.text = MainText;
            Choice1_TextObject.text = Choice1_Text;
            Choice2_TextObject.text = Choice2_Text;
            ShowCardMenu = true;
        }

    }
    
    public void HideCard()
    {
        CardPool.RemoveAt(CardNumber);
        ShowCardMenu = false;
    }

    public void TurnCheck() 
    {
        
    }

    public void Option1_Pressed()
    {
        for (int i = 0; i < Choice1_Effects.Count; i++) {
            if (Random.Range(0, 100) <= Choice1_Effects[i][2]) {
                switch (Choice1_Effects[i][0]) {
                    case 1.1f: 
                        Pst_Population += Choice1_Effects[i][1];
                        Debug.Log((Choice1_Effects[i][1]) + " Peasant Population");
                        break;
                    case 1.2f: 
                        Pst_Happiness += Choice1_Effects[i][1];
                        Debug.Log((Choice1_Effects[i][1]) + " Peasant Happiness");
                       break;
                    case 4.1f: // Money
                        Money += Choice1_Effects[i][1];
                        Debug.Log((Choice1_Effects[i][1]) + " Money");
                       break;
                    default:
                        break;
            }
            }
        }
        HideCard();
    }

    public void Option2_Pressed()
    {
        for (int i = 0; i < Choice2_Effects.Count; i++) {
            if (Random.Range(0f, 100f) <= Choice2_Effects[i][2]) {
                switch (Choice2_Effects[i][0]) {
                    case 1.1f: // Peasant Population
                        Pst_Population += Choice2_Effects[i][1];
                        Debug.Log(Choice2_Effects[i][1] + " Peasant Population");
                        break;
                    case 1.2f: // Peasant Happiness
                        Pst_Happiness += Choice2_Effects[i][1];
                        Debug.Log(Choice2_Effects[i][1] + " Peasant Happiness");
                       break;
                    case 4.1f: // Money
                        Money += Choice2_Effects[i][1];
                        Debug.Log(Choice2_Effects[i][1] + " Money");
                       break;
                    default:
                        break;
            }
            }
        }
        HideCard();
    }



    public void SetCard(int CardID)
    {
        if (Choice1_Effects.Count != 0 || Choice2_Effects.Count != 0) {
            Choice1_Effects.Clear();
            Choice2_Effects.Clear();
        }
        
        switch (CardID) {
            case 0: // Single change test
                MainText = "test - single change";
                Choice1_Text = "+1";
                Choice2_Text = "-1";
                Choice1_Effects.Add(new float[] {1.1f, 1, 100});
                Choice2_Effects.Add(new float[] {1.1f, -1, 100});
                break;
            case 1: // Multiple changes test
                MainText = "test - multiple changes";
                Choice1_Text = "+1 & -1";
                Choice2_Text = "-1 & +1";
                Choice1_Effects.Add(new float[] {1.1f, 1, 100});
                Choice1_Effects.Add(new float[] {1.2f, -1, 100});
                Choice2_Effects.Add(new float[] {1.1f, -1, 100});
                Choice2_Effects.Add(new float[] {1.2f, 1, 100});
                break;
            case 2: // Chance test
                MainText = "test - chance";
                Choice1_Text = "50% chance";
                Choice2_Text = "1% chance";
                Choice1_Effects.Add(new float[] {1.1f, 1, 50});
                Choice2_Effects.Add(new float[] {1.1f, 1, 1});
                break;
            case 3: // Blank
                MainText = " ";
                // Choice 1 - 
                Choice1_Text = " ";
                Choice1_Effects.Add(new float[] {0f, 0, 100});
                // Choice 2 - 
                Choice2_Text = " ";
                Choice2_Effects.Add(new float[] {0f, 0, 100});
                break;
            case 4: // Peasants request tools
                MainText = 
                "On behalf of the pesents here in " + KingdomName + ", we request a payment of 40 shillings to spend on better pitchforks for the workers " + 
                "as the old ones have gotten quite rusted and broken.";
                // Choice 1 - Give
                Choice1_Text = "Give Funds";
                Choice1_Effects.Add(new float[] {4.1f, -40, 100}); // Money
                Choice1_Effects.Add(new float[] {1.2f, 10, 100}); // Peasant Happiness
                // Choice 2 - Reject
                Choice2_Text = "Reject";
                Choice2_Effects.Add(new float[] {1.2f, -5, 100}); // Peasant Happiness
                break;
            case 5: // Stuelenfrothian prince
                MainText = 
                "       Dear King of " + KingdomName + ". <br> I am informing you on behalf of a Stuelenfrothian " + 
                "prince that you are in line to inherit his fortune of gold and silver. <br>to recieve this forture though, you must pay a 80 shilling depot fee, " + 
                "in order to pay for the carrier pidgeon costs.";
                // Choice 1 - Give
                Choice1_Text = "Reject";
                // Choice 2 - Reject
                Choice2_Text = "Accept";
                Choice2_Effects.Add(new float[] {4.1f, -80, 100}); // Money
                Choice2_Effects.Add(new float[] {4.1f, 580, 1}); // Money
                break;




            default:
                MainText = "oops this broke";
                Choice1_Text = "uh oh!";
                Choice2_Text = "oh no!";
                break;
        }
    }
}
