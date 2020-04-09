using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class RentLandNoti : MonoBehaviour
{
    private int balance;
    private int area;
    public GameObject rentLandNoti;
    public GameObject blackTransparency;

    public static event onVariableChange onVariableChanges;
    public delegate void onVariableChange(int balance);

    private MoneyInterface moneySystem;

    // Start is called before the first frame update
    void Start()
    {
        rentLandNoti.SetActive(false);
        blackTransparency.SetActive(false);
        moneySystem = FindObjectOfType<PlayerCurrency>().GetComponent<MoneyInterface>();
        RiceTab.onVariableChanges += getArea;
        checkArea();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkArea()
    {
        FirebaseDatabase.DefaultInstance.GetReference("Education")
            .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
            .Child("TypeOfLand")
            .ValueChanged += ReadTypeOfLand;
    }

    private void ReadTypeOfLand(object sender, ValueChangedEventArgs e)
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
                Debug.Log(child.Key.ToString());
                if (child.Key.ToString().Equals("RentLand"))
                {
                    rentLandNoti.SetActive(true);
                    blackTransparency.SetActive(true);
                }
            }
        }
    }

    public void onOk()
    {
        int rentFee = 1000 * area;
        balance -= rentFee;
        moneySystem.updateCurrentMoney(balance);
        Debug.Log("Current balance: " + balance);
        onVariableChanges?.Invoke(rentFee);
        rentLandNoti.SetActive(false);
        blackTransparency.SetActive(false);
    }

    public void getArea(string area)
    {
        this.area = Int32.Parse(area);
    }
}
