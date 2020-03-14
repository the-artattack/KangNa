using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
public class RiceTab : MonoBehaviour
{
    public Text riceNameText;
    public Text ricePhaseText;
    public Text areaText;
    public Text harvestTimeRemainingText;
    public Text ricePrice;
    /** Tab1 : Rice */
    public string riceName;
    public string area;
    public static string ricePhase;    
    public int harvestTimeRemaining = 120;
    public int harvestDay = 120;
    private string price;

    /** Tab2: Weather */
    public string currentWeather;

    /** Tab3: Water */
    public bool useRain = true;

    public static event onVariableChange onVariableChanges;
    public delegate void onVariableChange(string area);

    public static event onRiceName onRiceData;
    public delegate void onRiceName(string price);

    public static event onHarvestTrigger onHarvest;
    public delegate void onHarvestTrigger();

    private void Start()
    {
       TurnControl.onRicePhase += checkRicePhase;
       readParameters();
       checkHarvestDays(); //check number of day in rice type      
    }


    public void checkHarvestDays()
    {       
        if(FirebaseInit.Instance.riceType.Equals("ข้าวไวแสง"))
        {
            harvestDay = 80;
        }
        else
        {
            harvestDay = 120;
        }
        Debug.Log("Number of days: " + harvestDay);
    }

    public void checkRicePhase(int day)
    {
        if (harvestDay == 120)
        {
            if (day >= 0 && day <= 20)
            {
                ricePhase = "ระยะต้นกล้า";
            }
            else if (day > 20 && day <= 40)
            {
                ricePhase = "ระยะแตกกอ";
            }
            else if (day > 40 && day <= 60)
            {
                ricePhase = "ระยะตั้งท้อง";
            }
            else if (day > 60 && day <= 90)
            {
                ricePhase = "ระยะออกรวง";
            }
            else if (day > 90 && day <= 120)
            {
                ricePhase = "ระยะเก็บเก็ยว";
            }           
        }
        else
        {
            if (day >= 0 && day <= 20)
            {
                ricePhase = "ระยะต้นกล้า";
            }
            else if (day > 20 && day <= 21)
            {
                ricePhase = "ระยะแตกกอ";
            }
            else if (day > 21 && day <= 25)
            {
                ricePhase = "ระยะตั้งท้อง";
            }
            else if (day > 25 && day <= 55)
            {
                ricePhase = "ระยะออกรวง";
            }
            else if (day > 55 && day <= 80)
            {
                ricePhase = "ระยะเก็บเก็ยว";
            }
        }

        ricePhaseText.text = ricePhase;
        checkHarvestTimeRemaining(day);
    }
    public void checkHarvestTimeRemaining(int day)
    {        
        harvestTimeRemaining = harvestDay - day;
        harvestTimeRemainingText.text = string.Format("อีก {0}  วัน ถึงเวลาเก็บเกี่ยว", harvestTimeRemaining);
        
        if(day == harvestDay)
        {
            onHarvest?.Invoke();
        }       
    }   

    public void readParameters()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Education")
            .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)   
            .ValueChanged += ReadEducation;
    }

    private void readRiceType()
    {

    }

    public void readRicePrice()
    {
        FirebaseDatabase.DefaultInstance.GetReference("CostLists")
            .Child("Seeds").Child(RiceName.getRiceType(FirebaseInit.Instance.riceType))
            .ValueChanged += readCosts;
    }
    private void readCosts(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            DataSnapshot data = e.Snapshot;
            foreach (var child in data.Children)
            {
                Debug.Log(child.Key.ToString() + " " + riceName);
                if(child.Key.ToString().Equals(RiceName.getRiceKey(riceName)))
                {
                    price = child.Value.ToString();                    
                }
            }
            ricePrice.text = string.Format("{0} บาท/กิโลกรัม", price);
            onRiceData?.Invoke(price);
        }
    }

    private void ReadEducation(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            DataSnapshot data = e.Snapshot;
            riceName = data.Child("RiceName").Value.ToString();
            riceNameText.text = riceName;

            foreach (var child in data.Child("TypeOfLand").Children)
            {
                area = child.Value.ToString();
            }
            areaText.text = string.Format("พื้นที่เพาะปลูก: {0} ไร่", area);
            onVariableChanges?.Invoke(area);
            readRicePrice();
        }
    }

    public static string RicePhase
    {
        get { return ricePhase; }
    }
}
