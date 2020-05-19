using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Summary : MonoBehaviour
{
    private Parameters parameters;
    private SummaryDisplay summaryDisplay;
    private SummaryController summaryController;
    public bool taskComplete = false;
    //จำนวนไร่ที่ปลูก
    public int riceField;

    //วันที่เก็บเกี่ยว
    public DateTime harvestDate;

    //พันธุ์ข้าว
    public string riceType;

    //ราคารับซื้อข้าวเดือนนี้
    public double ricePricePredicted = 0;

    //น้ำหนักข้าวที่ชั่งได้
    public double riceQuantity;

    //ค่าความชื้นข้าว %
    public int riceMoisture;

    //ค่ารถขนส่งข้าวของโรงสี
    public int millTruck;


    //ค่าแรงงาน
    public double prepFieldCost;
    public double plantingCost;
    public double careCost;
    public double harvestCost;

    //ค่าวัสดุ
    public double seedCost; //ค่าเมล็ดพันธุ์
    public double soilCost; //ค่าปุ๋ย
    public double insecticideCost; //ยากำจัดแมลง
    public double diseaseCost; //ยากำจัดโรค
    public double herbicideCost; //ยากำจัดวัชพืช
    public double oilCost;

    //ค่ารถ
    public double harvestTruck;
    public double plantingTruck;

    //ค่าเช่าที่ดิน
    public double landRentalFee;

    public double equipmentDepreciation;
    public double opportunityEquipment;

    //ค่าฟาง
    public double straw;
    private void Awake()
    {
        parameters = GameObject.FindObjectOfType<Parameters>();
        summaryDisplay = GameObject.FindObjectOfType<SummaryDisplay>();
        summaryController = GameObject.FindObjectOfType<SummaryController>();
        riceType = Parameters.GetRiceType();
        harvestDate = Parameters.GetDate();
        StartCoroutine(getRicePricePredicted());
        saveEvaluationScore();
    }
    public string SetUp()
    {
        riceField = Int32.Parse(FirebaseInit.Instance.area);
        getQuantity();
        getSeedCost();                
        riceMoisture = 16;
        millTruck = 500;
        getLabor();
        getMaterial();
        getTruck();

        return "complete";
    }

    private void getQuantity()
    {
        //หน่วยตัน 
        riceQuantity = Math.Round(Parameters.GetSimulateParameters().RiceQuantity/1000, 2);
    }

    private void getSeedCost()
    {
        // 1 rai : 20kg (ข้าวหอมมะลิ105)
        int area = riceField * 20;
        seedCost = area * Parameters.GetSeedCost(); 
        Debug.Log("SeedCost: " + seedCost);
    }
    private IEnumerator WaitTask(Task task)
    {
        while (task.IsCompleted == false)
        {
            yield return null;
        }
        if (task.IsFaulted)
        {
            throw task.Exception;
        }
    }
    public IEnumerator getRicePricePredicted()
    {
        Debug.Log("Get rice price predicted from: " + harvestDate.Month + "-" + harvestDate.Year);
        Task<DataSnapshot> ricePrice = FirebaseDatabase.DefaultInstance
            .GetReference("Rice Prediction")
            .Child("data")
            .Child(harvestDate.Month + "-" + harvestDate.Year)
            .GetValueAsync();
        yield return WaitTask(ricePrice);

        if(ricePrice.Result.Value != null)
        {
            ricePricePredicted = Math.Round(Convert.ToDouble(ricePrice.Result.Value.ToString()), 0);
            summaryDisplay.createMillBoard();
            Debug.Log("Rice price predicted: " + ricePricePredicted); 
            taskComplete = true;
            SetUp();
            summaryController.showMillBoard();
        }
    }

    public double IncomeFromMill()
    {
        int riceMoistureBase = 15;
        double weight;
        double income;
        weight = (riceQuantity * (100 - riceMoisture)) / (100 - riceMoistureBase);
        income = ricePricePredicted * weight;
        return Math.Round(income, 2);
    }

    public double Straw()
    {
        straw = riceField * Parameters.GetMoneyList().getMoney("ค่าฟาง", "").cost;
        return straw;
    }
    //ค่าแรงงาน
    public double Labor()
    {
        //ค่าเตรียมดิน + ค่าปลูก + ค่าดูแลรักษา + ค่าเก็บเกี่ยว
        return prepFieldCost + plantingCost + careCost + harvestCost;
    }

    //ค่าวัสดุ
    public double MaterialCost()
    {
        //ค่าเมล็ดพันธุ์ + ค่าปุ๋ย + ค่ายากำจัดวัชพืช + ค่ายากำจัดแมลง + ค่ายากำจัดโรค + ค่าวัสดุอื่น เช่น น้ำมัน
        return seedCost + soilCost + insecticideCost + diseaseCost + herbicideCost + oilCost;
}

    public double Truck()
    {
        //ค่ารถหว่านเมล็ดข้าว + ค่ารถเก็บเกี่ยว
        return plantingTruck + harvestTruck;
    }

    //ค่าเสื่อมอุปกรณ์
    public double EquipmentDepreciationCost()
    {
        equipmentDepreciation = riceField * Parameters.GetMoneyList().getMoney("ค่าเสื่อมอุปกรณ์", "").cost;
        return Math.Round(equipmentDepreciation,2);
    }

    //ค่าเสียโอกาสอุปกรณ์
    public double EquipmentOpportunityCost()
    {
        opportunityEquipment =  riceField * Parameters.GetMoneyList().getMoney("ค่าเสียโอกาสอุปกรณ์", "").cost;
        return Math.Round(opportunityEquipment,2);
    }

    public double TotalCost()
    {
        double total = Labor() + MaterialCost() + Truck() + landRentalFee + equipmentDepreciation + opportunityEquipment;
        return Math.Round(total, 2);
    }

    public double ExpectedIncome()
    {
        return IncomeFromMill() + straw;
    }

    public double ProfitOrLoss()
    {
        return ExpectedIncome() - TotalCost();
    }

    public void printMoneyList()
    {
        Debug.Log("--------Printing user money list---------");
        Debug.Log("Money list count: " + Parameters.GetUserMoneyList().Count);
        foreach (Cost x in Parameters.GetUserMoneyList())
        {
            Debug.Log(x.topic + ": " + x.cost);            
        }
    }

    private float getMoney(string name)
    {
        Cost temp = Parameters.GetUserMoneyList().Where(obj => obj.topic == name).SingleOrDefault();
        if (temp == null)
        {
            return 0;
        }
        else
        {
            return temp.cost * riceField;
        }        
    }

    private void getLabor()
    {
        prepFieldCost = Parameters.GetMoneyList().getMoney("ค่าแรงงาน", "ค่าเตรียมดิน").cost * riceField;
        plantingCost = getMoney("ค่าปลูก");
        harvestCost = getMoney("ค่าเก็บเกี่ยว");
        careCost = getMoney("ค่าดูแลรักษา");
    }
    private void getMaterial()
    {
        soilCost = getMoney("ค่าปุ๋ย");
        insecticideCost = getMoney("ยากำจัดแมลง");
        diseaseCost = getMoney("ยากำจัดโรค");
        herbicideCost = getMoney("ยากำจัดวัชพืช");
        oilCost = getMoney("ค่าน้ำมัน");
    }
    private void getTruck()
    {
        plantingTruck = getMoney("รถหว่านเมล็ด");
        harvestTruck = getMoney("รถเก็บเกี่ยว");
    }

    private void saveEvaluationScore()
    {
        EvaluationBoard entry = new EvaluationBoard(Parameters.GetScore(), Parameters.GetMaxScore());
        Dictionary<string, System.Object> entryValues = entry.ToDictionary();
        Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>
        {
            ["/Education/" + FirebaseInit.Instance.auth.CurrentUser.UserId + "/Evaluation"] = entryValues
        };

        FirebaseInit.Instance._database.RootReference
            .UpdateChildrenAsync(childUpdates);

        Debug.Log("score: " + Parameters.GetScore() + " and max score: " + Parameters.GetMaxScore());
    }
}
