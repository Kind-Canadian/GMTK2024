using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public float Pst_Population = 75; // ID = 1.1
    public float Pst_Happiness = 80; // ID = 1.2
    public float Pst_PopGainRate = 10; // ID = 1.3
    public float Pst_TaxRate = 0.25f; // ID = 1.4
    
    public float Grd_Population = 15; // ID = 2.1
    public float Grd_Happiness  = 80; // ID = 2.2
    public float Grd_PayRate = 6; // ID = 2.3
    public float Grd_PayDay = 3; // ID 2.4
    
    public float Thf_Population = 10; // ID = 3.1
    public float Thf_Happiness = 50; // ID = 3.2

    public float Prev_Pst_Population;
    public float Prev_Pst_Happiness;
    public float Prev_Grd_Population;
    public float Prev_Grd_Happiness;
    public float Prev_Thf_Population;
    public float Prev_Thf_Happiness;
    public float Prev_Money;

    public float Money = 100; // ID 4.1
    
    public float MoneyDisplay;

    public string MainText;
    public string Choice1_Text;
    public string Choice2_Text;
    public string SummaryScreenText;
   
    public int CardNumber;
    public int CardsUsed;
    public int DailyCards = 1;

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
    public TMP_Text GuardPayText;
    public TMP_Text DayText;

    public TMP_Text SummaryTextObject;
    public TMP_Text SummaryButtonText;
    public Image BlackFade;
    public bool DoFade = false;

    public int Day = 1;

    public int GuardUnpaid = 0;

    public List<float[]> OptionChosen;

    public List<float[]> Choice1_Effects = new List<float[]>();
    public List<float[]> Choice2_Effects = new List<float[]>();

    public GameObject CardMenuObject;
    public bool ShowCardMenu = false;
    public GameObject SummaryMenuObject;
    public bool ShowSummaryMenu = false;

    public List<int> CardPool;
    public List<int> PosibleCards;

    public float CardDelay = -1;
    public bool DidLoseCheck = false;

    private int CardsPerDay;
    public float CardsPickedUp;

    public string KingdomName = "Rottenburg";

    public int DelayType;
    public int SummaryScreenType;
    public bool SummaryUp = true;

    public AudioClip SoundMoneyGain;
    public AudioClip SoundMoneyLose;
    public AudioClip SoundPageFlip;
    public AudioClip SoundCardPlace;
    public AudioClip SoundWakeHorn;
    public AudioClip SoundWarCry;
    
    void Start()
    {
        DayStart();
    }

    // Update is called once per frame
    void Update()
    {
        GuardPayText.text = "Guard Wage: " + Grd_PayRate + " shillings per guard";
        DayText.text = "Day: " + Day;

        if (Input.GetKeyDown(KeyCode.Space)) { 
            DayStart();
        }

        CardMenuObject.SetActive(ShowCardMenu);
        SummaryMenuObject.SetActive(ShowSummaryMenu);

        MoneyText.text = ((Mathf.Round(MoneyDisplay)) + " Shillings");
        SummaryTextObject.text = (SummaryScreenText);

        Pst_PopulationText.text = "Peasants<br>Population: " + (Pst_Population);
        Pst_HappinessText.text = "Peasant Happiness: " + (Pst_Happiness);
        Grd_PopulationText.text = "Guards<br>Popluation: " + (Grd_Population);
        Grd_HappinessText.text = "Guard Happiness: " + (Grd_Happiness);
        Thf_PopulationText.text = "Thieves<br>Population: " + (Thf_Population);
        Thf_HappinessText.text = "Thieves Happiness: " + (Thf_Happiness); 

        if (Money < 0) { 
            Money = 0; 
        }

        if (CardDelay < 1 && CardDelay >= 0) {
            if (DoFade) {
                CardDelay += Time.deltaTime / 6f;
                if (DelayType == 1 && CardDelay >= 0.5f && !DidLoseCheck) {
                    LoseCheck();
                    Day += 1;
                    CardsUsed = 0;
                    Debug.Log("did the thing");
                    AudioSource.PlayClipAtPoint(SoundWakeHorn, new Vector3(80f, 0f, -4.8f), 0.5f);
                    if (Thf_Happiness <= 20 && Thf_Population >= 10) {
                        AudioSource.PlayClipAtPoint(SoundWarCry, new Vector3(80f, 0f, -4.8f), 0.25f);
                    }
                }
            } else {
                CardDelay += Time.deltaTime * 1.5f;
            }
        }

        if (CardDelay >= 1) {
            switch (DelayType) {
                case 0: // Card
                    if (DailyCards <= CardsUsed) {
                        AudioSource.PlayClipAtPoint(SoundCardPlace, new Vector3(80f, 0f, -4.8f), 0.5f);
                        DayEnd();
                    } else {
                        ShowCard();
                    }
                    break;
                case 1: // Summary
                    AudioSource.PlayClipAtPoint(SoundCardPlace, new Vector3(80f, 0f, -4.8f), 0.5f);
                    DayStart();
                    break;
                case 2: // Day Start
                    ShowCard();
                    break;
                case 3: // First Day
                    ShowCard();
                    break;
                default:
                    break;
            }
            DoFade = false;
            CardDelay = -1;
        }

        if (DoFade) {
            BlackFade.color = new Color(BlackFade.color.r,BlackFade.color.g,BlackFade.color.b,(Mathf.Sin(CardDelay*3.14f)));
        } else {
            BlackFade.color = new Color(BlackFade.color.r,BlackFade.color.g,BlackFade.color.b,0);
        }

        if (Money > MoneyDisplay) {
            MoneyDisplay += Time.deltaTime * 30;
            if (Time.deltaTime * 30 > (Money - MoneyDisplay)) {
                MoneyDisplay = Money;
            }
            AudioSource.PlayClipAtPoint(SoundMoneyGain, new Vector3(80f, 0f, -4.8f), 0.1f);
            //Debug.Log("Display Money Increased By: " + (Time.deltaTime * 30));
        }

        if (Money < MoneyDisplay) {
            MoneyDisplay -= Time.deltaTime * 30;
            if (Time.deltaTime * 30 > (MoneyDisplay - Money)) {
                MoneyDisplay = Money;
            }
            AudioSource.PlayClipAtPoint(SoundMoneyLose, new Vector3(80f, 0f, -4.8f), 0.1f);
            //Debug.Log("Display Money Decreased By: -" + (Time.deltaTime * 30));
        }

        if (Pst_Population < 0) { Pst_Population = 0; }
        if (Pst_Happiness < 0) { Pst_Happiness = 0; }
        if (Pst_Happiness > 100) { Pst_Happiness = 100; }

        if (Grd_Population < 0) { Grd_Population = 0; }
        if (Grd_Happiness < 0) { Grd_Happiness = 0; }
        if (Grd_Happiness > 100) { Grd_Happiness = 100; }

        if (Thf_Population < 0) { Thf_Population = 0; }
        if (Thf_Happiness < 0) { Thf_Happiness = 0; }
        if (Thf_Happiness > 100) { Thf_Happiness = 100; }


    }

    public void ShowMessage()
    {
            SetCard(CardPool[-1]);
            MainTextObject.text = MainText;
            Choice1_TextObject.text = Choice1_Text;
            Choice2_TextObject.text = Choice2_Text;
            ShowCardMenu = true;
    }

    public void ShowCard()
    {
        if (CardPool.Count != 0) {
            SetCard(CardPool[0]);
            MainTextObject.text = MainText;
            Choice1_TextObject.text = Choice1_Text;
            Choice2_TextObject.text = Choice2_Text;
            ShowCardMenu = true;
            AudioSource.PlayClipAtPoint(SoundCardPlace, new Vector3(80f, 0f, -4.8f), 0.5f);
        }
    }
    
    public void HideCard()
    {
        CardPool.RemoveAt(0);
        CardsUsed += 1;
        ShowCardMenu = false;
        DelayType = 0;
        CardDelay = 0;
        Debug.Log("Card Hidden (Cards Used +1)");
        AudioSource.PlayClipAtPoint(SoundPageFlip, new Vector3(80f, 0f, -4.8f), 0.25f);
    }

    public void CloseSummary()
    {
        if (SummaryScreenType == 1) {
            DelayType = 1;
            CardDelay = 0;
            DidLoseCheck = false;
            DoFade = true;
        }

        if (SummaryScreenType == 2) {
            DelayType = 0;
            CardDelay = 0;
            SummaryUp = false;
        }

        if (SummaryScreenType == 3) {
            DelayType = 0;
            CardDelay = 0;
            SummaryUp = false;
        }

        AudioSource.PlayClipAtPoint(SoundPageFlip, new Vector3(80f, 0f, -4.8f), 0.25f);
        ShowSummaryMenu = false;

    }

    public void DayStart() 
    {
        
        CreateCardPool();

        if (Day != 1) {

            SummaryScreenText = "While you were asleep...<br>";

            // If unhappy enough (below 30), make less peasants come every day
            if (Pst_Happiness <= 30) {
                Pst_Population += Mathf.Round(Pst_PopGainRate * (Pst_Happiness/30));
                SummaryScreenText = SummaryScreenText + "<br>   - " +
                "Very few Peasants decided to move to your kingdom <br>";
            } else if (Pst_Happiness == 0){
                SummaryScreenText = SummaryScreenText + "<br>   - " +
                "No Peasants decided to move to your kingdom <br>";
            } else {
                Pst_Population += Pst_PopGainRate;
                SummaryScreenText = SummaryScreenText + "<br>   - " +
                "Some Peasants decided to move to your kingdom <br>";
            }

            // If unhappy enough (below 20), make thieves attempt to take over the castle
            if (Thf_Happiness <= 20 && Thf_Population >= 10) {
                Grd_Population -= Mathf.Floor(Thf_Population / 5);
                Thf_Population -= Mathf.Floor(Thf_Population / 5);
                SummaryScreenText = SummaryScreenText + "<br>   - " +
                "The Thieves attempted to take over the castle! Your Guards defended but you lost a few of them. <br>";
            }

            // If unhappy enough (below 50), make peasant population move to thieves
            if (Pst_Happiness <= 50) {
                Thf_Population += Mathf.Round(Pst_Population * 0.25f * (1 - Pst_Happiness/50));
                Pst_Population -= Mathf.Round(Pst_Population * 0.25f * (1 - Pst_Happiness/50));
                SummaryScreenText = SummaryScreenText + "<br>   - " +
                "Some Peasants got angry and moved to the Thieves village! <br>";
            }

            // If underpaid, make guards angry, or really angry if unhappy
            if (GuardUnpaid == 1) {
                if (Grd_Happiness <= 30) {
                    Grd_Population -= Mathf.Round(Grd_Population * 0.1f);
                    Grd_Happiness -= Mathf.Round(30);
                    SummaryScreenText = SummaryScreenText + "<br>   - " +
                    "The guards became unhappy as they were under paid. <br>";
                    SummaryScreenText = SummaryScreenText + "<br>   - " +
                    "Some guards decided to quit as they were under paid! <br>";
                } else {
                    Grd_Happiness -= Mathf.Round(30);
                    SummaryScreenText = SummaryScreenText + "<br>   - " +
                    "The guards became unhappy as they were under paid. <br>";
                }
                GuardUnpaid = 0;
            }

            // If unpaid, make guards extra angry
            if (GuardUnpaid == 2) {
                Grd_Population -= Mathf.Round(Grd_Population * 0.1f);
                Grd_Happiness -= Mathf.Round(50);
                SummaryScreenText = SummaryScreenText + "<br>   - " +
                "The guards became unhappy as they weren't paid. <br>";
                SummaryScreenText = SummaryScreenText + "<br>   - " +
                "Some guards decided to quit as they weren't paid! <br>";
                GuardUnpaid = 0;
            }

            // 
            if (Random.Range(0,100) <= 15 && Grd_Population*3 >= Thf_Population) {
                Thf_Population += Mathf.Round(Random.Range(8,16));
                Thf_Happiness += Mathf.Round(25);
                SummaryScreenText = SummaryScreenText + "<br>   - " +
                "A group of Thieves snuck their way into the kingdom <br>";
            }



            

            SummaryButtonText.text = "Start Day<br>--->";
            SummaryScreenType = 2;
            ShowSummaryMenu = true;
            
        } else {
            SummaryScreenText = "   To my beloved child, <br>" +

            "<br>It is clear that my days of ruling are over. With my eventual passing marching ever closer, " +
            "I have decided to write you this letter in advance to teach you how to be as good a ruler as I have been." +

            "<br><br>Firstly, the peasants are responsible for the kingdom's profit. Make sure you keep them appeased!" +

            "<br><br>Thieves are cruel individuals who prove to be a great threat if they’re in a steamy mood." +

            "<br><br>Your guards will protect your village from the ones who want it destroyed. Never forget that a happy guard is a loyal one." +

            "<br><br>I’m sure you’ll be an amazing ruler! Just remember these words to go by and you’ll PROBABLY end up fine." +

            "<br><br>Your Father," +
            "<br>King Gibblesworth."
            ;
            SummaryButtonText.text = "Start Day<br>--->";
            SummaryScreenType = 3;
            ShowSummaryMenu = true;
        }

            Prev_Pst_Population = Pst_Population;
            Prev_Pst_Happiness = Pst_Happiness;
            Prev_Grd_Population = Grd_Population;
            Prev_Grd_Happiness = Grd_Happiness;
            Prev_Thf_Population = Thf_Population;
            Prev_Thf_Happiness = Thf_Happiness;
            Prev_Money = Money;
        
    }

    public void DayEnd() 
    {

        SummaryScreenText = "   ~ Day Summary ~<br>" + 
        "<br>Money" + 
        "<br>   Money Gained: " + (Money - Prev_Money) + 
        "<br>" + 
        "<br>Peasants" + 
        "<br>   Population Gained: " +  (Pst_Population - Prev_Pst_Population) +
        "<br>   Happiness Gained: " +  (Pst_Happiness - Prev_Pst_Happiness) +
        "<br>" + 
        "<br>Guards" + 
        "<br>   Population Gained: " +  (Grd_Population - Prev_Grd_Population) +
        "<br>   Happiness Gained: " +  (Grd_Happiness - Prev_Grd_Happiness) +
        "<br>" + 
        "<br>Thieves" + 
        "<br>   Population Gained: " +  (Thf_Population - Prev_Thf_Population) +
        "<br>   Happiness Gained: " +  (Thf_Happiness - Prev_Thf_Happiness) +
        "<br>" + 
        "<br>   ~ Day End Expenses ~" + 
        "<br>" + 
        "<br>Tax Income: " + (Mathf.Round(Pst_TaxRate * Pst_Population)) + " shillings" +
        "<br>Guard Pay: ";

        if (Day % Grd_PayDay == 0) {
            SummaryScreenText = (SummaryScreenText) + "-" + (Mathf.Round(Grd_PayRate * Grd_Population)) + " shillings";
        } else if (Day % Grd_PayDay == 2) {
            SummaryScreenText = (SummaryScreenText) + "IN 1 DAYS";
        } else if (Day % Grd_PayDay == 1) {
            SummaryScreenText = (SummaryScreenText) + "IN 2 DAYS";
        }



        Money += Mathf.Round(Pst_TaxRate * Pst_Population);

        if (Day % Grd_PayDay == 0) {
            if (Money >= (Grd_PayRate*Grd_Population)) {
                GuardUnpaid = 0;
                Debug.Log("Guards Payed Full");
            } else if (Money > (Grd_PayRate/8*Grd_Population)) {
                GuardUnpaid = 1;
                Debug.Log("Guards Underpaid");
            } else {
                GuardUnpaid = 2;
                Debug.Log("Guards Unpaid");
            }

            Money -= Mathf.Round(Grd_PayRate * Grd_Population);
        }

        SummaryButtonText.text = "Go To Sleep";
        SummaryScreenType = 1;
        DelayType = 1;
        CardDelay = 0;
        SummaryUp = true;
        ShowSummaryMenu = true;
    }

    public void LoseCheck()
    {
        if (Grd_Happiness <= 10 && Grd_Population >= 15) {
            Info.deathmessage = "The Guards decided you weren't quite fit to be king.";
            LoseGame();
        }

        if (Thf_Happiness <= 80 && (Grd_Population * 2) <= Thf_Population && Thf_Population > 10) {
            Info.deathmessage = "The Thieves overpowered the guards and took over the kingdom";
            LoseGame();
        }

        DidLoseCheck = true;
    }

    public void LoseGame()
    {
        Info.days = Day;
        Debug.Log("Loaded Death Scene");
        SceneManager.LoadScene(2);
        

    }


    public void Option_Pressed(int Number)
    {
        Debug.Log("Option " + Number + " Pressed");
        
        switch (Number) {
            case 1:
                OptionChosen = Choice1_Effects;
                break;
            case 2:
                OptionChosen = Choice2_Effects;
                break;
            default:
                OptionChosen = Choice1_Effects;
                break;
        }

        for (int f = 0; f < OptionChosen.Count; f++) {
            if ((OptionChosen[f][0] == 4.1f) && (-OptionChosen[f][1] > Money)) {
                Debug.Log("Not Enough Money");
                AudioSource.PlayClipAtPoint(SoundMoneyLose, new Vector3(80f, 0f, -4.8f), 0.5f);
                return;
            }
        }

        for (int i = 0; i < OptionChosen.Count; i++) {
            if (Random.Range(0, 100) <= OptionChosen[i][2]) {
                switch (OptionChosen[i][0]) {
                    case 1.1f: // Peasant Population
                        Pst_Population += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Peasant Population");
                        break;
                    case 1.2f: // Peasant Happiness
                        Pst_Happiness += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Peasant Happiness");
                        break;
                    case 1.3f: // Peasant Pop Gain Rate
                        Pst_PopGainRate += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Peasant Pop Gain Rate");
                        break;
                    case 1.4f: // Peasant Tax Rate
                        Pst_TaxRate += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Peasant Tax Rate");
                        break;
                    case 2.1f: // Guard Population
                        Grd_Population += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Guard Population");
                        break;
                    case 2.2f: // Guard Happiness
                        Grd_Happiness += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Guard Happiness");
                        break;
                    case 2.3f: // Guard Pay Rate
                        Grd_PayRate += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Guard Pay Rate");
                        break;
                    case 3.1f: // Thieves Population
                        Thf_Population += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Thieves Population");
                        break;
                    case 3.2f: // Thieves Happiness
                        Thf_Happiness += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Thieves Happiness");
                        break;
                    case 4.1f: // Money
                        Money += OptionChosen[i][1];
                        Debug.Log((OptionChosen[i][1]) + " Money");
                        break;
                    default:
                        break;
                }
            }
        }
        
        HideCard();
        
    }

    public void CreateCardPool()
    {
        PosibleCards.Clear();

        PosibleCards.Add(4);
        PosibleCards.Add(6);
        PosibleCards.Add(7);
        PosibleCards.Add(8);
        PosibleCards.Add(9);
        PosibleCards.Add(12);
        PosibleCards.Add(14);
        PosibleCards.Add(15);
        PosibleCards.Add(17);
        PosibleCards.Add(18);
        PosibleCards.Add(19);
        PosibleCards.Add(20);
        PosibleCards.Add(22);
        PosibleCards.Add(23);
        PosibleCards.Add(24);
        PosibleCards.Add(25);
        PosibleCards.Add(26);
        PosibleCards.Add(27);
        PosibleCards.Add(28);
        PosibleCards.Add(29);
        PosibleCards.Add(30);
        PosibleCards.Add(31);

        if (Pst_Happiness >= 80) {
            PosibleCards.Add(10);
        }

        if (Grd_Happiness <= 40) {
            PosibleCards.Add(11);
        }

        if (Grd_Happiness >= 80) {
            PosibleCards.Add(13);
        }

        if (Grd_Happiness >= 80 && Grd_Population >= 20) {
            PosibleCards.Add(21);
        }

        if (Pst_Happiness <= 50) {
            PosibleCards.Add(16);
        }



        for (int i = 0; i < DailyCards; i++) {
            CardPool.Add(PosibleCards[Random.Range(0,PosibleCards.Count)]);
        }

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
                "On behalf of the peasants here in " + KingdomName + ", we request a payment of 20 shillings to spend on better pitchforks for the workers " + 
                "as the old ones have gotten quite rusted and broken.";
                // Choice 1 - Give
                Choice1_Text = "Better tools mean better working peasants!";
                Choice1_Effects.Add(new float[] {4.1f, -20, 100}); // Money
                Choice1_Effects.Add(new float[] {1.2f, 5, 100}); // Peasant Happiness
                // Choice 2 - Reject
                Choice2_Text = "Only a poor workman blames his tools";
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
                Choice1_Effects.Add(new float[] {4.1f, -25, 100});
                Choice1_Effects.Add(new float[] {2.2f, 5, 100});
                // Choice 2 - 
                Choice2_Text = "Those swords are good enough.";
                Choice2_Effects.Add(new float[] {2.2f, -5, 100});
                break;
            case 7: // Peasants want farmland
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "We’re worried about the food situation. If you give us peasants more farmland," +  
                " we’ll be able to provide enough food for everyone." +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Grow me something good!"; // Text for option 1
                Choice1_Effects.Add(new float[] {1.2f, 7, 100});
                Choice1_Effects.Add(new float[] {2.2f, 5, 100});
                Choice1_Effects.Add(new float[] {3.2f, 5, 50});
                Choice1_Effects.Add(new float[] {4.1f, -20, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "We need to salvage the food we have"; // Text for option 2
                Choice2_Effects.Add(new float[] {1.2f, -7, 100});
                Choice2_Effects.Add(new float[] {2.2f, -5, 100});
                Choice2_Effects.Add(new float[] {3.2f, -5, 50});
                break;
            case 8: // Kevith
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                " My Liege," +  
                "<br>Our patrol found a knight from a neighbouring kingdom named Kevinth " +
                "<br>May we add him to our ranks?";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "The more the merrier "; // Text for option 1
                Choice1_Effects.Add(new float[] {2.1f, 1, 100});
                Choice1_Effects.Add(new float[] {2.2f, 1, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "He can’t be trusted "; // Text for option 2
                Choice2_Effects.Add(new float[] {2.2f, -1, 100}); 
                break; // leave this
            case 9: // Lollipop
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "My mother wouldn’t get me a lollipop! " +  
                "<br>Could I please, please, PLEASE get one!!!" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Oh, well how could I say no!"; // Text for option 1
                Choice1_Effects.Add(new float[] {1.2f, 1, 100});
                Choice1_Effects.Add(new float[] {4.1f, -2, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "No, now get away from me filthy child!"; // Text for option 2
                Choice2_Effects.Add(new float[] {1.2f, -1, 100});
                break; // leave this
            case 10: // Peasants want hiring (peasants happy)
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Some peasants are willing to become loyal guards for proper compensation. " +  
                "<br>Would you like to hire them?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "The more the merrier!"; // Text for option 1
                Choice1_Effects.Add(new float[] {1.1f, -4, 100});
                Choice1_Effects.Add(new float[] {2.1f, 4, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "I’m not looking to hire"; // Text for option 2
                Choice2_Effects.Add(new float[] {0f, 0, 100}); 
                break; // leave this
            case 11: // Guards not happy (guards angry)
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "We guards haven’t been compensated enough for our hard work! We’re not thrilled." +  
                " " +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "I’ll give you all a raise."; // Text for option 1
                Choice1_Effects.Add(new float[] {2.3f, 1, 100});
                Choice1_Effects.Add(new float[] {2.2f, 30, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "I hope to pay you soon."; // Text for option 2
                Choice2_Effects.Add(new float[] {0f, 0, 100}); 
                Choice2_Effects.Add(new float[] {0f, 0, 100}); 
                break; // leave this
            case 12: // Theatre in town
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Would you like to host a theatre play in our town?" +  
                "<br>It will only cost about 50 shillings to host and will most likely bring new people into town!" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "I hope it’s a comedy!"; // Text for option 1
                Choice1_Effects.Add(new float[] {4.1f, -50, 100});
                Choice1_Effects.Add(new float[] {1.1f, 10, 100});
                Choice1_Effects.Add(new float[] {1.2f, 10, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "I hate plays."; // Text for option 2
                Choice2_Effects.Add(new float[] {1.2f, -2, 100}); 
                Choice2_Effects.Add(new float[] {3.2f, 5, 100}); 
                break; // leave this
            case 13: // Guard abusing power (guards happy)
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Some peasants are accusing a guard of abusing his power to take a loaf of bread from an innocent man!" +  
                "<br><br> The guard denies these accusations, saying that the man instead was offering it as thanks for his hard work." +
                "<br><br> what should we do?";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Side with the peasants"; // Text for option 1
                Choice1_Effects.Add(new float[] {1.2f, 3, 100});
                Choice1_Effects.Add(new float[] {2.1f, -1, 100});
                Choice1_Effects.Add(new float[] {2.2f, -5, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "Side with the guard"; // Text for option 2
                Choice2_Effects.Add(new float[] {2.2f, 5, 100}); 
                Choice2_Effects.Add(new float[] {1.1f, -3, 100}); 
                Choice2_Effects.Add(new float[] {1.2f, -5, 100}); 
                break; // leave this
            case 14: // Holy hand grenade
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Sire, " +  
                " <br> A priest has informed me of a blessed weapon of great power:" +
                " <br> A holy hand grenade. Might we acquire one? ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = " Tis but a scratch… On the wallet"; // Text for option 1
                Choice1_Effects.Add(new float[] {2.2f, 40, 100});
                Choice1_Effects.Add(new float[] {4.1f, -100, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "I FART in your general direction "; // Text for option 2
                Choice2_Effects.Add(new float[] {2.2f, -1, 100}); 
                break; // leave this
            case 15: // Straving family
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "My Family and I are starving. <br>We lack any amount of money to purchase bread." +  
                " <br> Would you spare some change?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = " Anything for my people"; // Text for option 1
                Choice1_Effects.Add(new float[] {4.1f, -10, 100});
                Choice1_Effects.Add(new float[] {1.2f, 5, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "Work harder "; // Text for option 2
                Choice2_Effects.Add(new float[] {3.1f, 5, 100}); 
                Choice2_Effects.Add(new float[] {1.1f, -5, 100}); 
                Choice2_Effects.Add(new float[] {1.2f, -5, 100}); 
                break; // leave this
            case 16: // Angry person (peasants mad)
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Your decisions have been a disaster for the kingdom! " +  
                " <br> You are nothing but a simpleton! " +
                "<br> I am leaving! ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "alright "; // Text for option 1
                Choice1_Effects.Add(new float[] {1.1f, -1, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = " Treason is punishable by death"; // Text for option 2
                Choice2_Effects.Add(new float[] {1.1f, -1, 100}); 
                break; // leave this
            case 17: // Thief hideout
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Your Majesty," +  
                "<br>We found a thief hideout nearby! Shall we raid it?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Get rid of those pests!"; // Text for option 1
                Choice1_Effects.Add(new float[] {3.1f, -4, 75});
                Choice1_Effects.Add(new float[] {3.2f, -7, 100});
                Choice1_Effects.Add(new float[] {2.1f, -2, 50});
                Choice1_Effects.Add(new float[] {4.1f, 25, 50});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "They aren’t a threat"; // Text for option 2
                Choice2_Effects.Add(new float[] {3.2f, 10, 75}); 
                Choice2_Effects.Add(new float[] {1.2f, -5, 40}); 
                break; // leave this
            case 18: // Burnt house
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "One of the buildings in the village was burnt down! Would you like to rebuild it?" +  
                " " +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Of course"; // Text for option 1
                Choice1_Effects.Add(new float[] {4.1f, -20, 100});
                Choice1_Effects.Add(new float[] {1.2f, 8, 100});
                Choice1_Effects.Add(new float[] {1.1f, -3, 30});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "This isn’t a big issue"; // Text for option 2
                Choice2_Effects.Add(new float[] {1.2f, -5, 100}); 
                Choice2_Effects.Add(new float[] {1.1f, -5, 50}); 
                break; // leave this
            case 19: // Gem Thief
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "A thief is attempting to steal some of your gems!" +  
                " " +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Stop them!"; // Text for option 1
                Choice1_Effects.Add(new float[] {3.1f, -1, 100});
                Choice1_Effects.Add(new float[] {3.2f, -5, 100});
                Choice1_Effects.Add(new float[] {2.1f, -1, 40});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "Let him go."; // Text for option 2
                Choice2_Effects.Add(new float[] {3.2f, 7, 100}); 
                Choice2_Effects.Add(new float[] {1.2f, -5, 75}); 
                Choice2_Effects.Add(new float[] {4.1f, -6, 50}); 
                break; // leave this
            case 20: // MR BEAST
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "A fowl beast has invaded the village!" +  
                " " +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Send in my guards!"; // Text for option 1
                Choice1_Effects.Add(new float[] {2.1f, -3, 30});
                Choice1_Effects.Add(new float[] {1.1f, -1, 20}); 
                Choice1_Effects.Add(new float[] {1.2f, 6, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "Just one of my guards will be enough!"; // Text for option 2
                Choice2_Effects.Add(new float[] {2.1f, -1, 90}); 
                Choice2_Effects.Add(new float[] {1.1f, -2, 30}); 
                Choice2_Effects.Add(new float[] {1.2f, 3, 100}); 
                break; // leave this
            case 21: // Raid rival kingdom (high guards and happiness)
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Sire," +  
                "<br>There is a rival kingdom nearby! Should we raid them?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Take them out!"; // Text for option 1
                Choice1_Effects.Add(new float[] {2.1f, -5, 35});
                Choice1_Effects.Add(new float[] {1.1f, 6, 40});
                Choice1_Effects.Add(new float[] {1.2f, 6, 100});
                Choice1_Effects.Add(new float[] {4.1f, 30, 50});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "They won’t be a threat"; // Text for option 2
                Choice2_Effects.Add(new float[] {0f, 0, 100}); 
                Choice2_Effects.Add(new float[] {0f, 0, 100}); 
                break; // leave this
            case 22: // Buddy Stealing
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Hey, scum!" +  
                "<br>One of my buddies got caught stealing and is getting executed tomorrow. If you free him, I’ll put in a good word about you to the thieves." +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "You can watch his execution!"; // Text for option 1
                Choice1_Effects.Add(new float[] {3.2f, -5, 100});
                Choice1_Effects.Add(new float[] {1.2f, 5, 100});
                Choice1_Effects.Add(new float[] {3.1f, -1, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "Keep this between us"; // Text for option 2
                Choice2_Effects.Add(new float[] {3.2f, 15, 100}); 
                Choice2_Effects.Add(new float[] {1.2f, -7, 40}); 
                Choice2_Effects.Add(new float[] {2.2f, -7, 40}); 
                break; // leave this
            case 23: // Thief leader
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Your Majesty!" +  
                "<br>We found one of the leaders of the thieves guild! Should we assassinate him?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "I expect to see them dead!"; // Text for option 1
                Choice1_Effects.Add(new float[] {2.1f, -4, 40});
                Choice1_Effects.Add(new float[] {3.1f, -7, 65});
                Choice1_Effects.Add(new float[] {1.2f, 10, 100});
                Choice1_Effects.Add(new float[] {3.2f, -10, 100});
                Choice1_Effects.Add(new float[] {4.1f, 25, 40});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "It’s not worth the risk"; // Text for option 2
                Choice2_Effects.Add(new float[] {3.2f, 5, 100}); 
                Choice2_Effects.Add(new float[] {2.2f, -5, 50}); 
                break; // leave this
            case 24: // Guard break
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Sire," +  
                "We guards are getting rather tired from standing at our posts for so long. Could we receive a small break?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "A guard with energy is a deadly one!"; // Text for option 1
                Choice1_Effects.Add(new float[] {2.2f, 12, 100});
                Choice1_Effects.Add(new float[] {1.1f, -5, 50});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "No, we need to stay on high alert"; // Text for option 2
                Choice2_Effects.Add(new float[] {2.2f, -5, 100}); 
                Choice2_Effects.Add(new float[] {1.2f, 3, 100}); 
                break; // leave this
            case 25: // Secret lover
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Dear my beloved," +  
                "I would love to see you! But I need the money to get to you. Could you spare some gold so that we can be together for the evening?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Of course, my love!"; // Text for option 1
                Choice1_Effects.Add(new float[] {3.2f, 1, 100});
                Choice1_Effects.Add(new float[] {4.1f, -10, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "Sorry, I’m too busy"; // Text for option 2
                Choice2_Effects.Add(new float[] {3.2f, -1, 100}); 
                Choice2_Effects.Add(new float[] {0f, 0, 100}); 
                break; // leave this
            case 26: // Rich cave
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Your Majesty," +  
                "<br>We found an eerie cave filled with gold nearby. Should we take the loot?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Of course!"; // Text for option 1
                Choice1_Effects.Add(new float[] {2.1f, -2, 50});
                Choice1_Effects.Add(new float[] {4.1f, 20, 50});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "It’s too sketchy"; // Text for option 2
                Choice2_Effects.Add(new float[] {2.2f, -1, 100}); 
                Choice2_Effects.Add(new float[] {0f, 0, 100}); 
                break; // leave this
            case 27: // uhhh?
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Erg." +  
                "<br>Grrg ug brug frmmm hrn?" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "Uuh. Yes?"; // Text for option 1
                Choice1_Effects.Add(new float[] {4.1f, -5, 100});
                Choice1_Effects.Add(new float[] {0f, 0, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "I don’t think so?"; // Text for option 2
                Choice2_Effects.Add(new float[] {1.1f, -1, 100}); 
                Choice2_Effects.Add(new float[] {2.1f, -1, 30}); 
                break; // leave this
            case 28: // Thief attack
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Help! It’s urgent!" +  
                "<br>The thieves are trying to attack our village!" +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "I’ll get my guards on it!"; // Text for option 1
                Choice1_Effects.Add(new float[] {2.1f, -4, 35});
                Choice1_Effects.Add(new float[] {1.2f, 5, 100});
                Choice1_Effects.Add(new float[] {1.1f, -5, 30});
                Choice1_Effects.Add(new float[] {3.2f, -6, 30});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "They’re busy. You got this!"; // Text for option 2
                Choice2_Effects.Add(new float[] {1.2f, -6, 100}); 
                Choice2_Effects.Add(new float[] {1.1f, -10, 100}); 
                Choice2_Effects.Add(new float[] {3.2f, 6, 100}); 
                break; // leave this
            case 29: // Peasants playing with fire
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "We have a problem." +  
                "<br>Some of the peasants are playing with fire." +
                " ";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "I’ll have someone stop them!"; // Text for option 1
                Choice1_Effects.Add(new float[] {1.1f, -3, 60});
                Choice1_Effects.Add(new float[] {2.1f, -1, 35});
                Choice1_Effects.Add(new float[] {1.2f, -4, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "They’ll learn their lesson!"; // Text for option 2
                Choice2_Effects.Add(new float[] {1.1f, -6, 100}); 
                Choice2_Effects.Add(new float[] {1.2f, 3, 100}); 
                break; // leave this
            case 30: // Thieves leaving
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Hey, Scum!" +  
                "<br>Some of us thieves are willing to leave your rotten kingdom if you pay us some gold." +
                "<br>Sounds like a deal, huh?";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "I will not let you annoy other villages!"; // Text for option 1
                Choice1_Effects.Add(new float[] {3.2f, -4, 100});
                Choice1_Effects.Add(new float[] {0f, 0, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "Fine. Get out of here!"; // Text for option 2
                Choice2_Effects.Add(new float[] {4.1f, -27, 100}); 
                Choice2_Effects.Add(new float[] {3.1f, -5, 70}); 
                break; // leave this
            case 31: // dnt knw hw 2 rght
                MainText = // the three lines below are just for text (use <br> for line break), put in the " "
                "Der Majersty," +  
                "<br>Som of us dnt know hwo 2 right well." +
                "<br>can yuo help?";
                // Choice 1 - // (this is just a comment)
                Choice1_Text = "I’ll hire you a tutor"; // Text for option 1
                Choice1_Effects.Add(new float[] {4.1f, 7, 100});
                Choice1_Effects.Add(new float[] {1.2f, 3, 100});
                // Choice 2 - // (this is just a comment)
                Choice2_Text = "You can write good enough"; // Text for option 2
                Choice2_Effects.Add(new float[] {1.2f, -1, 25}); 
                Choice2_Effects.Add(new float[] {0f, 0, 100}); 
                break; // leave this




            default:
                MainText = "oops this broke";
                Choice1_Text = "uh oh!";
                Choice1_Effects.Add(new float[] {0f, 0, 100});
                Choice2_Text = "oh no!";
                Choice2_Effects.Add(new float[] {0f, 0, 100});
                break;
        }
    }
}


