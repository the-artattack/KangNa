using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummaryDisplay : MonoBehaviour
{
    private Summary summary;

    //Mill board
    public Text harvestDateMillBoard;
    public Text riceTypeMillBoard;
    public Text ricePricePredictedMillBoard;
    public Text totalMillIncome;

    //Income board
    public Text harvestDateIncomeBoard;
    public Text riceTypeIncomeBoard;
    public Text ricePricePredictedIncomeBoard;
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
    //6. ค่าเสียโอกาสอุปกรณ์
    public Text opportunityCost;
    //7. ค่าฟาง
    public Text straw;
    
    //Summary board
    public Text riceArea;
    public Text cost;
    public Text income;
    public Text profit;

    private void Awake()
    {
        summary = GameObject.FindObjectOfType<Summary>();
    }

    public void createMillBoard()
    {
        //วันที่เก็บเกี่ยว
        harvestDateMillBoard.text = string.Format("ประจำวันที่ {0}", ConvertThaiDate(summary.harvestDate));
        //พันธุ์ข้าว
        riceTypeMillBoard.text = summary.riceType;
        //ราคารับซื้อข้าวเดือนนี้
        ricePricePredictedMillBoard.text = string.Format("{0:0,0} บาท", summary.ricePricePredicted);
    }

    public void createIncomeBoard()
    {
        //วันที่เก็บเกี่ยว
        harvestDateIncomeBoard.text = string.Format("ประจำวันที่ {0}", ConvertThaiDate(summary.harvestDate));
        //พันธุ์ข้าว
        riceTypeIncomeBoard.text = summary.riceType;
        //ราคารับซื้อข้าวเดือนนี้
        ricePricePredictedIncomeBoard.text = string.Format("{0:0,0} บาท", summary.ricePricePredicted);
        //น้ำหนักข้าวที่ชั่งได้
        riceWeight.text = string.Format("{0} ตัน", summary.riceQuantity);
        //ค่าความชื้น
        riceMoisture.text = string.Format("{0}%", summary.riceMoisture);
        //ค่ารถขนส่ง
        millTruck.text = string.Format("{0:0,0} บาท", summary.millTruck);
        //รวมเป็นเงิน
        totalMillIncome.text = string.Format("รวมเป็นเงิน {0:0,0.0} บาท", summary.IncomeFromMill());
    }

    public void createBill()
    {        
        Debug.Log("Bill created!");
        //1. ค่าแรงงาน
        labor.text = string.Format("{0:0,0.##} บาท", summary.Labor());
        //ค่าเตรียมดิน
        prepFieldCost.text = string.Format("{0:0,0} บาท", summary.prepFieldCost);
        //ค่าปลูก 
        plantingCost.text = string.Format("{0:0,0} บาท", summary.plantingCost);
        //ค่าเก็บเกี่ยว
        harvestCost.text = string.Format("{0:0,0} บาท", summary.harvestCost);
        //ค่าดูแลรักษา
        careCost.text = string.Format("{0:0,0} บาท", summary.careCost);        

        //2. ค่าวัสดุ
        meterial.text = string.Format("{0:0,0.##} บาท", summary.MaterialCost());
        //ค่าเมล็ดพันธุ์
        seedCost.text = string.Format("{0:0,0} บาท", summary.seedCost);
        //ค่าปุ๋ย
        soilCost.text = string.Format("{0:0,0} บาท", summary.soilCost);
        //ค่ายากำจัดวัชพืช
        herbicideCost.text = string.Format("{0:0,0} บาท", summary.herbicideCost);
        //ค่ายากำจัดแมลง
        insecticideCost.text = string.Format("{0:0,0} บาท", summary.insecticideCost);
        //ค่ายากำจัดโรค
        diseaseCost.text = string.Format("{0:0,0} บาท", summary.diseaseCost);
        //ค่าวัสดุอื่น เช่น น้ำมัน
        oilCost.text = string.Format("{0:0,0} บาท", summary.oilCost);

        //3. ค่ารถ
        truck.text = string.Format("{0:0,0.##} บาท", summary.Truck());
        //ค่ารถหว่านเมล็ดข้าว
        plantTruck.text = string.Format("{0:0,0.##} บาท", summary.plantingTruck);
        //ค่ารถเก็บเกี่ยว
        harvestTruck.text = string.Format("{0:0,0.##} บาท", summary.harvestTruck);

        //4. ค่าเช่าที่ดิน
        landRentalFee.text = string.Format("{0:0,0.##} บาท", summary.landRentalFee);
        //5. ค่าเสียโอกาสอุปกรณ์
        opportunityCost.text = string.Format("{0:0,0.0} บาท", summary.EquipmentOpportunityCost());
        //6. ค่าเสื่อมอุปกรณ์
        equipmentDepreciationCost.text = string.Format("{0:0,0.0} บาท", summary.EquipmentDepreciationCost());
        //7. ค่าฟาง        
        straw.text = string.Format("{0:0,0} บาท", summary.Straw());
    }

    public void createSummary()
    {
        Debug.Log("Summary created!");
        riceArea.text = string.Format("{0} ไร่", summary.riceField);
        cost.text = string.Format("{0:0,0.0} บาท", summary.TotalCost());
        income.text = string.Format("{0:0,0.0} บาท", summary.ExpectedIncome());
        profit.text = string.Format("{0:0,0.0} บาท", summary.ProfitOrLoss());
    }

    private string ConvertThaiDate(DateTime time)
    {
        //Debug.Log("Month: " + time.Month + " Day: " + time.Day + " Year: " + (time.Year + 543));
        switch (time.Month)
        {
            case 1:
                return time.Day + " " + "มกราคม" + " " + (time.Year + 543);
            case 2:
                return time.Day + " " + "กุมภาพันธ์" + " " + (time.Year + 543);
            case 3:
                return time.Day + " " + "มีนาคม" + " " + (time.Year + 543);
            case 4:
                return time.Day + " " + "เมษายน" + " " + (time.Year + 543);
            case 5:
                return time.Day + " " + "พฤษภาคม" + " " + (time.Year + 543);
            case 6:
                return time.Day + " " + "มิถุนายน" + " " + (time.Year + 543);
            case 7:
                return time.Day + " " + "กรกฎาคม" + " " + (time.Year + 543);
            case 8:
                return time.Day + " " + "สิงหาคม" + " " + (time.Year + 543);
            case 9:
                return time.Day + " " + "กันยายน" + " " + (time.Year + 543);
            case 10:
                return time.Day + " " + "ตุลาคม" + " " + (time.Year + 543);
            case 11:
                return time.Day + " " + "พฤศจิกายน" + " " + (time.Year + 543);
            case 12:
                return time.Day + " " + "ธันวาคม" + " " + (time.Year + 543);
            default:
                return "Error";
        }
    }
}
