using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummaryCalculation : MonoBehaviour
{
    public Summary summary;

    //Mill board
    public Text harvestDateMillBoard;
    public Text riceTypeMillBoard;
    public Text ricePricePredicted;
    public Text totalMillIncome;

    //Income board
    public Text harvestDateIncomeBoard;
    public Text riceTypeIncomeBoard;
    public Text riceWeight;
    public Text riceMoisture;
    public Text millTruck;

    //Bill board
    //1. ค่าแรงงาน
    public Text labor;
    public Text prepFieldCost; //ค่าเตรียมดิน
    public Text plantingCost; //ค่าปลูก
    public Text harvestCost; //ค่าเก็บเกี่ยว
    public Text careCost; //ค่าดูแลรักษา (รวมคนฉีดยาฆ่าแมลง คนใส่ปุ๋ย)

    //2. ค่าวัสดุ
    public Text meterial;
    public Text seedCost; //ค่าเมล็ดพันธุ์
    public Text soilCost; //ค่าปุ๋ย
    public Text insecticideCost; //ยากำจัดแมลง
    public Text diseaseCost; //ยากำจัดโรค
    public Text herbicideCost; //ยากำจัดวัชพืช
    public Text oilCost; //ค่าน้ำมัน

    //3. ค่ารถ
    public Text truck;
    public Text harvestTruck; //ค่ารถเก็บเกี่ยว
    public Text plantTruck; //ค่ารถหว่านเมล็ด

    //4. ค่าเช่าที่ดิน
    public Text landRentalFee;
    //5. ค่าเสื่อมอุปกรณ์
    public Text equipmentDepreciationCost;
    //6. ค่าเสียโอกาส
    public Text opportunityCost;
    //7. ค่าฟาง
    public Text straw;
    
    //Summary board
    public Text riceArea;
    public Text cost;
    public Text income;
    public Text profit;    

    public void createMillBoard()
    {
        //วันที่เก็บเกี่ยว
        harvestDateMillBoard.text = summary.harvestDate.ToString();
        //พันธุ์ข้าว
        riceTypeMillBoard.text = summary.riceType;
        //ราคารับซื้อข้าวเดือนนี้
        ricePricePredicted.text = summary.ricePricePredicted.ToString();
    }

    public void createIncomeBoard()
    {
        //วันที่เก็บเกี่ยว
        harvestDateIncomeBoard.text = summary.harvestDate.ToString();
        //พันธุ์ข้าว
        riceTypeIncomeBoard.text = summary.riceType;
        //ราคารับซื้อข้าวเดือนนี้
        ricePricePredicted.text = summary.ricePricePredicted.ToString();
        //น้ำหนักข้าวที่ชั่งได้
        riceWeight.text = summary.riceWeight.ToString();
        //ค่าความชื้น
        riceMoisture.text = summary.riceMoisture.ToString();
        //ค่ารถขนส่ง
        millTruck.text = summary.millTruck.ToString();
        //รวมเป็นเงิน
        totalMillIncome.text = string.Format("รวมเป็นเงิน {0} บาท", summary.IncomeFromMill().ToString());
    }

    public void createBill()
    {        
        Debug.Log("Bill created!");
        //1. ค่าแรงงาน
        //ค่าเตรียมดิน
        //ค่าปลูก 
        //ค่าดูแลรักษา
        //ค่าเก็บเกี่ยว

        //2. ค่าวัสดุ
        //ค่าเมล็ดพันธุ์
        seedCost.text = string.Format("{0} บาท", summary.seedCost.ToString());
        //ค่าปุ๋ย
        //ค่ายากำจัดวัชพืช
        //ค่ายากำจัดแมลง
        //ค่ายากำจัดโรค
        //ค่าวัสดุอื่น เช่น น้ำมัน

        //3. ค่ารถ
        //ค่ารถหว่านเมล็ดข้าว
        //ค่ารถเก็บเกี่ยว

        //4. ค่าเช่าที่ดิน
        landRentalFee.text = string.Format("{0} บาท", summary.landRentalFee.ToString());
        //5. ค่าเสียโอกาสอุปกรณ์
        //6. ค่าเสื่อมอุปกรณ์
        //7. ค่าฟาง

        //รายรับที่ได้จากโรงสี        
    }

    public void createSummary()
    {
        Debug.Log("Summary created!");
        riceArea.text = string.Format("{0} ไร่", summary.riceField.ToString());
        cost.text = string.Format("{0} บาท", summary.TotalCost().ToString());
        income.text = string.Format("{0} บาท", summary.ExpectedIncome().ToString());
        profit.text = string.Format("{0} บาท", summary.ProfitOrLoss().ToString());
    }
}
