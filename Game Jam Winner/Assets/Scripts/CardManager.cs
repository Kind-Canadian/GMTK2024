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
    public float Grd_PayDay = 3; // ID 2.4
    
    public float Thf_Population = 10; // ID = 3.1
    public float Thf_Happiness = 50; // ID = 3.2

    public float Money = 100; // ID 4.1
    
    public float MoneyDisplay;

    public string MainText;
    public string Choice1_Text;
    public string Choice2_Text;
   
    public int CardNumber;
    public int CardsUsed;
    public int DailyCards = 20;

    public TMP_Text MainTextObject;
    public TMP_Text Choice1_TextObject;
    public TMP_Text Choice2_TextObject;
    //public TMP_Text Choice1_MoneyChangeText;
    //public TMP_Text Choice2_MoneyChangeText;
    public TMP_Text MoneyText;
    public TMP_Text Pst_PopulationText;
    public TMP_Text Pst_HappinessText;
    public TMP_Text Grd_PopulationText;
    public TMP_Text Grd_HappinessText;
    public TMP_Text Thf_PopulationText;
    public TMP_Text Thf_HappinessText;

    public int Day = 0;

    public List<float[]> Choice1_Effects = new List<float[]>();
    public List<float[]> Choice2_Effects = new List<float[]>();

    public GameObject CardMenuObject;
    public bool ShowCardMenu;

    public List<int> CardPool;
    public List<int> PosibleCards;

    public float CardDelay = -1;

    private int CardsPerDay;
    public float CardsPickedUp;

    public string KingdomName = "Shitsenburg";


    // Start is called before the first frame update
    
    
    
    
    void Start()
    {
        DayStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { 
            DayStart();
        }

        CardMenuObject.SetActive(ShowCardMenu);

        MoneyText.text = ((Mathf.Round(MoneyDisplay)) + " Shillings");

        Pst_PopulationText.text = "Peasant Population: " + (Pst_Population);
        Pst_HappinessText.text = "Peasant Happiness: " + (Pst_Happiness);
        Grd_PopulationText.text = "Guard Popluation: " + (Grd_Population);
        Grd_HappinessText.text = "Guard Happiness: " + (Grd_Happiness);
        Thf_PopulationText.text = "Thieves Population: " + (Thf_Population);
        Thf_HappinessText.text = "Thieves Happiness: " + (Thf_Happiness); 

        if (Money < 0) { 
            Money = 0; 
        }

        if (CardDelay < 1 && CardDelay >= 0) {
            CardDelay += Time.deltaTime;
        }

        if (CardDelay >= 1) {
            ShowCard();
            CardDelay = -1;
        }

        if (Money > MoneyDisplay) {
            MoneyDisplay += 1 * Time.deltaTime * 30;
        }

        if (Money < MoneyDisplay) {
            MoneyDisplay -= 1 * Time.deltaTime * 30;
        }
    }

    public void ShowCard()
    {
        if (CardPool.Count != 0) {
            SetCard(CardPool[0]);
            MainTextObject.text = MainText;
            Choice1_TextObject.text = Choice1_Text;
            Choice2_TextObject.text = Choice2_Text;
            ShowCardMenu = true;
        }

    }
    
    public void HideCard()
    {
        CardPool.RemoveAt(0);
        CardsUsed += 1;
        ShowCardMenu = false;
        if (DailyCards <= CardsUsed) {
            DayEnd();
        } else {
            CardDelay = 0;
        }
    }

    public void DayStart() 
    {
        if (Day != 0) {

            // If unhappy enough (below 50), make peasant population move to thieves
            if (Pst_Happiness <= 50) {
                Pst_Population -= Pst_Population * 0.25f * (Pst_Happiness/50);
                Thf_Population += Pst_Population * 0.25f * (Pst_Happiness/50);
            }

            // If unhappy enough (below 30), make less peasants come every day
            if (Pst_Happiness <= 30) {
                Pst_Population += Pst_PopGainRate * (Pst_Happiness/30);
            } else {
                Pst_Population += Pst_PopGainRate;
            }
            
        }

        CardsUsed = 0;
        CreateCardPool();
        SetCard(0);
        ShowCard();
        Day += 1;
    }

    public void DayEnd() 
    {
        Money += (Pst_TaxRate * Pst_Population);

        if (Day % Grd_PayDay == 0) {
            Money -= (Grd_PayRate * Grd_Population);
        }
    }

    public void CreateCardPool()
    {
        PosibleCards.Clear();

        PosibleCards.Add(4);
        //PosibleCards.Add(5); // needs better wording tbh
        PosibleCards.Add(6);
        PosibleCards.Add(7);
        PosibleCards.Add(8);
        PosibleCards.Add(9);
        PosibleCards.Add(10);



        for (int i = 0; i < DailyCards; i++) {
            CardPool.Add(PosibleCards[Random.Range(0,PosibleCards.Count)]);
        }

    }

    public void Option1_Pressed()
    {
        Debug.Log("Option 1 Pressed");
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
        Debug.Log("Option 2 Pressed");
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
                MainText = 
                " " +  
                " " +
                " ";
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
                Choice1_Text = "Better tools mean better working peasants!";
                Choice1_Effects.Add(new float[] {4.1f, -20, 100}); // Money
                Choice1_Effects.Add(new float[] {1.2f, 5, 100}); // Peasant Happiness
                // Choice 2 - Reject
                Choice2_Text = "Only a poor workman always blames his tools";
                Choice2_Effects.Add(new float[] {1.2f, -5, 100}); // Peasant Happiness
                break;
            case 5: // Stuelenfrothian prince
                MainText = 
                "       Dear King of " + KingdomName + ". <br> I am informing you on behalf of a Stuelenfrothian " + 
                "prince that you are in line to inherit his fortune of gold and silver. <br> <br>to recieve this forture though, you must pay a 80 shilling depot fee, " + 
                "in order to pay for the carrier pidgeon costs.";
                // Choice 1 - Give
                Choice1_Text = "Reject";
                // Choice 2 - Reject
                Choice2_Text = "Accept";
                Choice2_Effects.Add(new float[] {4.1f, -80, 100}); // Money
                Choice2_Effects.Add(new float[] {4.1f, 580, 10}); // Money
                break;
            case 6: // Better weapons for guards
                MainText = 
                "Your Majesty, we’re concerned that our weaponry might not be sufficient enough " +  
                "for sudden attacks. May you spare some gold so that we may replace our swords " +
                "and cannons?";
                // Choice 1 - 
                Choice1_Text = "Well of course!";
                Choice1_Effects.Add(new float[] {4.1f, -30, 100});
                Choice1_Effects.Add(new float[] {2.2f, 5, 100});
                // Choice 2 - 
                Choice2_Text = "Those swords are good enough.";
                Choice2_Effects.Add(new float[] {2.2f, -5, 100});
                break;
            case 7: // Peasants need housing
                MainText = 
                "We peasants are quickly running out of resources for more housing. Could you " +  
                "give us some gold towards construction for the new families?" +
                " ";
                // Choice 1 - 
                Choice1_Text = " ";
                Choice1_Effects.Add(new float[] {0f, 0, 100});
                // Choice 2 - 
                Choice2_Text = " ";
                Choice2_Effects.Add(new float[] {0f, 0, 100});
                break;
            case 8: // Bailing thief out
                MainText = 
                "One of my friends got caught stealing and is soon on his way to execution. If you " +  
                "bail him out, I’ll put in a good word about you to the other thieves." +
                " ";
                // Choice 1 - 
                Choice1_Text = " ";
                Choice1_Effects.Add(new float[] {0f, 0, 100});
                // Choice 2 - 
                Choice2_Text = " ";
                Choice2_Effects.Add(new float[] {0f, 0, 100});
                break;
            case 9: // Peasants need farmland
                MainText = 
                "We’re worried about the food situation. If you give us peasants more farmland " +  
                "we’ll be able to provide enough food for everyone." +
                " ";
                // Choice 1 - 
                Choice1_Text = " ";
                Choice1_Effects.Add(new float[] {0f, 0, 100});
                // Choice 2 - 
                Choice2_Text = " ";
                Choice2_Effects.Add(new float[] {0f, 0, 100});
                break;
            case 10: // Attack rival camp
                MainText = 
                "Your Majesty, our scouts have noticed a rival camp near our kingdom, would you " +  
                "like us to attack them and maybe bring back some prisoners to help with work?" +
                " ";
                // Choice 1 - 
                Choice1_Text = " ";
                Choice1_Effects.Add(new float[] {0f, 0, 100});
                // Choice 2 - 
                Choice2_Text = " ";
                Choice2_Effects.Add(new float[] {0f, 0, 100});
                break;
          




            default:
                MainText = "oops this broke";
                Choice1_Text = "uh oh!";
                Choice2_Text = "oh no!";
                break;
        }
    }
}


