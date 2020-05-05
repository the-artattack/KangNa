using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoneyInterface
{   
    void BuyRice(int price);
    void DeductMoney(float amount);
    void ShowCurrentMoney();
    void updateCurrentMoney(float money);
    
}
