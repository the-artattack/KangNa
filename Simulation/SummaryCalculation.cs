using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SummaryCalculation : MonoBehaviour
{
    public Summary summary = new Summary();
    public GameObject bill;
    public GameObject bill2;
    public GameObject blackTransparency;
    private double opportunityCost;
    private double equipmentDepreciationCost;
    private double equipmentOpportunityCost;

    public Text seedCost;
    public Text landRentalFee;
    public Text riceField;

    public Text prepFieldCost;
    public Text plantingCost;
    public Text careCost;
    public Text harvestCost;
    public Text soilCost;
    public Text insecticideAndHerbicideCost;
    public Text others;
    public Text opportunityCostText;
    public Text equipmentDepreciationCostText;
    public Text expectedPrice;
    public Text expectedYield;

    public Text riceArea;
    public Text cost;
    public Text income;
    public Text profit;

    public static event OnEventTrigger onSummary;
    public delegate void OnEventTrigger();

    // Start is called before the first frame update
    void Start()
    {
        RentLandNoti.onVariableChanges += getRentFee;
        RiceTab.onRiceData += getSeedCost;
        RiceTab.onVariableChanges += getArea;

        bill.SetActive(false);
        bill2.SetActive(false);
        blackTransparency.SetActive(false);
        summary.prepFieldCost = 1500;
        summary.plantingCost = 1570;
        summary.careCost = 3870;
        summary.harvestCost = 5470;
                
        summary.soilCost = 500;
        summary.insecticideAndHerbicideCost = 1800;
        summary.others = 1000;

        opportunityCost = summary.OpportunityCost();
        
        equipmentDepreciationCost = summary.EquipmentDepreciationCost();
        equipmentOpportunityCost = summary.EquipmentOpportunityCost();
        summary.expectedPrice = 14000;

        MainGame.onSummaryTrigger += enableSummary;
    }

    private void enableSummary(SimulateParameters parameters)
    {
        summary.expectedYield = parameters.RiceQuantity;
        createSummary();
    }

    private void getSeedCost(string price)
    {
        int area = summary.riceField * 20;
        summary.seedCost = area * Int32.Parse(price); // 1 rai : 20 kg
        Debug.Log("SeedCost: " + summary.seedCost);
    }

    private void getRentFee(int rentFee)
    {
        summary.landRentalFee = rentFee;
        Debug.Log("rent: " + summary.landRentalFee);
    }

    private void getArea(string area)
    {
        summary.riceField = Int32.Parse(area);
        Debug.Log("area: " + summary.riceField);
    }
    
    public void createSummary()
    {
        bill.SetActive(true);
        bill2.SetActive(false);
        blackTransparency.SetActive(true);
        Debug.Log("Bill created!");
        onSummary?.Invoke();

        seedCost.text = string.Format("{0} บาท",summary.seedCost.ToString());
        landRentalFee.text = string.Format("{0} บาท",summary.landRentalFee.ToString());
        riceField.text = string.Format("{0} กิโลกรัม",summary.riceField.ToString());
    }

    public void createNextSummary()
    {
        bill2.SetActive(true);
        bill.SetActive(false);

        riceArea.text = string.Format("{0} ไร่",summary.riceField.ToString());
        cost.text = string.Format("{0} บาท", summary.Wage().ToString());
        income.text = string.Format("{0} บาท",summary.ExpectedIncome().ToString());
        profit.text = string.Format("{0} บาท",summary.ProfitOrLoss().ToString());
    }

    public void EndingScene()
    {
        SceneChanger.nextScene(2);
    }
}
