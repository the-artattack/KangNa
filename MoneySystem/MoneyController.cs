using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public static List<Money> moneyList = new List<Money>();
    public MoneyList list;
    private Money money;
    private MoneyInterface moneySystem;
    public int count = 0;

    private void Start()
    {
        moneySystem = FindObjectOfType<PlayerCurrency>().GetComponent<MoneyInterface>();
    }

    public void bill(string type, string name)
    {
        Money temp;
        money = list.getMoney(type, name);
        if(money != null)
        {
            temp = addMoneyList(money);
            moneySystem.DeductMoney(temp.cost);
        }        
    }

    private Money addMoneyList(Money item)
    {
        Money temp;
        //if have this money in this list
        if (isOnList(item))
        {
            //increase cost
            moneyList.Where(i => i.name == item.name).ToList().ForEach(s => s.cost += item.cost);
            temp = moneyList.Where(i => i.name == item.name).SingleOrDefault();
            Debug.Log("Updated '" + temp.name + "' with total money: " + temp.cost);            
        }
        else
        {
            moneyList.Add(item);
            temp = item;
            Debug.Log("Total money of '" + temp.name + "' not changed: " + temp.cost);            
        }
        count = moneyList.Count;
        Debug.Log("Money List Count: " + moneyList.Count);        
        return temp;
    }

    private bool isOnList(Money item)
    {
        Money temp;
        bool isContains;
        temp = moneyList.Where(i => i.name == item.name).SingleOrDefault();
        if (temp != null)
        {            
            isContains = true;
        }
        else
        {
            isContains = false;
        }
        return isContains;
    }
}
