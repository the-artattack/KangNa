using Firebase.Database;
using System;
using UnityEngine;

public class Summary : MonoBehaviour
{
    //จำนวนไร่ที่ปลูก
    public int riceField;

    //วันที่เก็บเกี่ยว
    public DateTime harvestDate;

    //พันธุ์ข้าว
    public string riceType;

    //ราคารับซื้อข้าวเดือนนี้
    public double ricePricePredicted;

    //น้ำหนักข้าวที่ชั่งได้
    public double riceWeight;

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
    public double seedCost;
    public double soilCost;
    public double insecticideAndHerbicideCost;
    public double others;

    //ค่าเช่าที่ดิน
    public double landRentalFee;

    public double equipmentDepreciation;
    public double opportunityEquipment;

    private void Start()
    {
        RentLandNoti.onVariableChanges += getRentFee;
        RiceTab.onRiceData += getSeedCost;
        RiceTab.onVariableChanges += getArea;
        SetUp();
    }

    private void SetUp()
    {
        Parameters.print();
        riceField = Int32.Parse(FirebaseInit.Instance.area);
        riceWeight = Parameters.parameters.RiceQuantity;
        harvestDate = Parameters.date;
        riceType = Parameters.riceType;
        getRicePricePredicted(harvestDate);
        riceMoisture = 16;
        millTruck = 500;
    }

    private void getSeedCost(string price)
    {
        int area = riceField * 20;
        seedCost = area * Int32.Parse(price); // 1 rai : 20 kg
        Debug.Log("SeedCost: " + seedCost);
    }

    private void getRentFee(int rentFee)
    {
        landRentalFee = rentFee;
        Debug.Log("rent: " + landRentalFee);
    }

    private void getArea(string area)
    {
        riceField = Int32.Parse(area);
        Debug.Log("area: " + riceField);
    }

    private void getRicePricePredicted(DateTime date)
    {
        FirebaseDatabase.DefaultInstance.GetReference("Rice Prediction")
           .Child("data").Child(date.Month + "-" + date.Year)
           .ValueChanged += readPrice;
    }

    private void readPrice(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            DataSnapshot data = e.Snapshot;
            ricePricePredicted = Convert.ToDouble(data.Value);
        }
    }

    public double IncomeFromMill()
    {
        int riceMoistureBase = 15;
        double weight;
        double income;
        weight = (riceWeight * (100 - riceMoisture)) / (100 - riceMoistureBase);
        income = ricePricePredicted * weight;
        return income;
    }

    //ค่าแรงงาน
    public double Labor()
    {
        return prepFieldCost + plantingCost + careCost + harvestCost;
    }

    //ค่าวัสดุ
    public double MaterialCost()
    {
        return seedCost + soilCost + insecticideAndHerbicideCost + others;
    }

    public double Truck()
    {
        return 0;
    }

    public double OpportunityCost()
    {
        return (Labor() + MaterialCost()) * (7/100) * (4/12);
    }

    //ค่าเสื่อมอุปกรณ์
    public double EquipmentDepreciationCost()
    {
        return riceField * Parameters.moneyList.getMoney("ค่าเสื่อมอุปกรณ์", "").cost;
    }

    //ค่าโอกาสอุปกรณ์
    public double EquipmentOpportunityCost()
    {
        return riceField * Parameters.moneyList.getMoney("ค่าเสียโอกาสอุปกรณ์", "").cost;
    }

    public double TotalCost()
    {
        return Labor() + MaterialCost() + OpportunityCost() + landRentalFee + EquipmentDepreciationCost() + EquipmentOpportunityCost();
    }

    public double ExpectedIncome()
    {
        return IncomeFromMill();
    }

    public double ProfitOrLoss()
    {
        return ExpectedIncome() - TotalCost();
    }

}
