using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MoneyInterface
{   
    void BuyRice(int price);
    void ShowCurrentMoney();
    void updateCurrentMoney(int money);
}
