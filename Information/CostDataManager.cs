using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;

/** Managing all cost data by sync with real-time database*/
public class CostDataManager : MonoBehaviour
{
    public static event LoadData onLoadData;
    public delegate void LoadData();

    private void Awake()
    {    
        RaadAllData();
        ReadRiceType();
    }
    
    private void ReadRiceType()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Education")
            .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
            .Child("TypeOfRice").ValueChanged += GetRiceTypeValue;

    }

    private void GetRiceTypeValue(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError(e.DatabaseError.Message);
            return;
        }
        else
        {
            DataSnapshot data = e.Snapshot;
            Debug.Log("Type of rice:" + data.Value.ToString());
            FirebaseInit.Instance.riceType = data.Value.ToString();
            onLoadData?.Invoke();
        }
    }

    public void RaadAllData()
    {
        FirebaseDatabase.DefaultInstance.GetReference("CostLists")
            .ValueChanged += HandleValueChanged;
    }
    
    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        else
        {
            DataSnapshot data = args.Snapshot;            
            DisplayData(data);
        }       
    }

    private void DisplayData(DataSnapshot dataSnapshot)
    {
        CostList.CostInstance.otherCost.Clear();
        CostList.CostInstance.insensitivityRice.Clear();
        CostList.CostInstance.sensitivityRice.Clear();
        ReadSeeds(dataSnapshot.Child("Seeds"), "PhotoperiodInsensitivityRice", CostList.CostInstance.insensitivityRice);
        ReadSeeds(dataSnapshot.Child("Seeds"), "PhotoperiodSensitivityRice", CostList.CostInstance.sensitivityRice);
        ReadOtherCost(dataSnapshot.Child("OtherCost"));

        Debug.Log("SowingTruck: "+ CostList.CostInstance.otherCost[0]["SowingTruck_PerRai"]);
        Debug.Log("RD43: "+ CostList.CostInstance.insensitivityRice[0]["RD43_PerKg"]);
        Debug.Log("RD15:" + CostList.CostInstance.sensitivityRice[0]["RD15_PerKg"]);
        print("Number of element in other cost: " + CostList.CostInstance.otherCostSize);
        
    }
       
    private void ReadSeeds(DataSnapshot dataSnapshot, string path, List<Dictionary<string, object>> destinationDict)
    {
        DataSnapshot d = dataSnapshot.Child(path);
        var dict = d.Value as Dictionary<string, object>;
        if (dict != null)
        {
            destinationDict.Add(dict);
        }

    }

    private void ReadOtherCost(DataSnapshot dataSnapshot)
    {
        var dict = dataSnapshot.Value as Dictionary<string, object>;
        if (dict != null)
        {
            CostList.CostInstance.otherCost.Add(dict);      
        }              
    }

    
}
