using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RiceShop : MonoBehaviour
{
    private Transform container;
    public GameObject shopItem; 
    private MoneyInterface moneySystem;

    public GameObject confirmationBox;
    public GameObject blackTransparency;
    private void Start()
    {
        container = transform.Find("container");   
        CostDataManager.onLoadData += LoadData;
        moneySystem = FindObjectOfType<PlayerCurrency>().GetComponent<MoneyInterface>();
    }

    private void LoadData()
    {
        //Debug.Log(FirebaseInit.Instance.riceType);
        int i = 1;
        if (FirebaseInit.Instance.riceType.Equals("ข้าวไวแสง"))
        {
            foreach (var key in CostList.CostInstance.sensitivityRice[0].Keys)
            {
                generateText(RiceName.GetRiceName(key), CostList.CostInstance.sensitivityRice[0][key].ToString(),i);
                i--;
                //Debug.Log(i + key);                
            }
        }
        else
        {
            foreach (var key in CostList.CostInstance.insensitivityRice[0].Keys)
            {
                generateText(RiceName.GetRiceName(key), CostList.CostInstance.insensitivityRice[0][key].ToString(),i);
                i--;
                //Debug.Log(i + key);
            }
        }
    }

    private void generateText(string name, string price, int positionIndex)
    {
        Debug.Log("GENERATE TEXT!");
        GameObject itemObject = Instantiate(shopItem, container);
        RectTransform shopItemRectTransform = itemObject.transform.GetComponent<RectTransform>();
        float shopItemHeight = 220f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, shopItemHeight * positionIndex);
        itemObject.transform.Find("Price").GetComponent<Text>().text = price;
        itemObject.transform.Find("RiceName").GetComponent<Text>().text = name;

        itemObject.GetComponent<Button>().onClick.AddListener(() => BuyConfirm(name, price));
    }
    
    private void BuyConfirm(string riceName, string price)
    {
        blackTransparency.SetActive(true);
        confirmationBox.SetActive(true);
        confirmationBox.transform.Find("RiceName").GetComponent<Text>().text = riceName;

        Button yesBtn = confirmationBox.transform.Find("YesButton").GetComponent<Button>();
        Button noBtn = confirmationBox.transform.Find("NoButton").GetComponent<Button>();
        yesBtn.onClick.AddListener(() => OnYesBtnPressed(riceName, price));
        noBtn.onClick.AddListener(onNoBtnPressed);        
    }

    private void OnYesBtnPressed(string riceName, string price)
    {
        Debug.Log("Yes button pressed!");
        BuyItem(riceName, price);
        confirmationBox.SetActive(false);        
    }

    private void onNoBtnPressed()
    {
        Debug.Log("No button pressed!");
        blackTransparency.SetActive(false);
        confirmationBox.SetActive(false);
    }

    private void BuyItem(string riceName, string price)
    {
        Debug.Log("Buy " + riceName);
        int ricePrice = Int32.Parse(price);
        Debug.Log(ricePrice);
        moneySystem.BuyRice(ricePrice);
        FirebaseInit.Instance._database.RootReference
                        .Child("Education")
                        .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
                        .Child("RiceName").SetValueAsync(riceName);
        Debug.Log("Current scene: " + FirebaseInit.Instance.CurrentScene);
        SceneChanger.nextScene(FirebaseInit.Instance.CurrentScene + 1);
    }     
}
