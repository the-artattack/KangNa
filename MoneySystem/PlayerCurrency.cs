using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCurrency : MonoBehaviour, MoneyInterface
{
    private float currentMoney = 100000;
    private int area;

    public Text money;
    // Start is called before the first frame update
    private void Awake()
    {
        getArea();
        ShowCurrentMoney();       
    }

    public void BuyRice(int price)
    {
        currentMoney -= (area * price);
        Debug.Log("Current money: " + currentMoney);
        updateCurrentMoney(currentMoney);
    }


    private void getArea()
    {
        area = Int32.Parse(FirebaseInit.Instance.area);
        Debug.Log("Area: " + area);
    }

    /* update current money to FirebaseInit.Instance.CurrentMoney when done something */
    public void updateCurrentMoney(float money)
    {
        FirebaseInit.Instance.CurrentMoney = money;
        this.money.text = currentMoney.ToString();
    }

    /* use this function except shop rice */
    public void SaveMoney()
    {
        FirebaseInit.Instance._database.RootReference
            .Child("Users").Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
            .Child("State")
            .Child("balance")
            .SetValueAsync(currentMoney);
        Debug.Log("save new balance");
    }

    public void ShowCurrentMoney()
    {
        currentMoney = FirebaseInit.Instance.CurrentMoney;
        money.text = currentMoney.ToString();
        Debug.Log("Current balance: " + FirebaseInit.Instance.CurrentMoney);
    }

    public void DeductMoney(float amount)
    {
        getArea();
        currentMoney -= (area * amount);
        Debug.Log("Current money: " + currentMoney);
        updateCurrentMoney(currentMoney);
    }    
}
