using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summary 
{
    //จำนวนไร่ที่ปลูก
    public int riceField;

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

    //คาดว่าจะได้รับ
    public double expectedYield; //ผลผลิตที่คาดว่าจะได้รับ
    public double expectedPrice; //ราคาที่คาดว่าจะขายได้ บาท/ตัน

   
    public double Wage()
    {
        return prepFieldCost + plantingCost + careCost + harvestCost;
    }

    public double MaterialCost()
    {
        return seedCost + soilCost + insecticideAndHerbicideCost + others;
    }

    public double OpportunityCost()
    {
        return (Wage() + MaterialCost()) * (7/100) * (4/12);
    }

    //ค่าเสื่อมอุปกรณ์
    public double EquipmentDepreciationCost()
    {
        return riceField * 21.76;
    }

    //ค่าโอกาสอุปกรณ์
    public double EquipmentOpportunityCost()
    {
        return riceField * 3.32;
    }

    public double TotalCost()
    {
        return Wage() + MaterialCost() + OpportunityCost() + landRentalFee + EquipmentDepreciationCost() + EquipmentOpportunityCost();
    }

    public double ExpectedIncome()
    {
        return expectedYield * expectedPrice/1000; //Baht per ton
    }

    public double ProfitOrLoss()
    {
        return ExpectedIncome() - TotalCost();
    }

}
