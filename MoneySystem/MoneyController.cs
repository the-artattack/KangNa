using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    protected static List<Cost> moneyList = new List<Cost>();
    public List<Cost> debugList = new List<Cost>();
    public GameObject list;
    private MoneyList myList;
    private Money item;
    private MoneyInterface moneySystem;
    public int count = 0;

    private void Start()
    {
        moneySystem = FindObjectOfType<PlayerCurrency>().GetComponent<MoneyInterface>();
        myList = list.GetComponent<MoneyList>();
    }

    public void bill(string type, string name)
    {
        Cost temp;
        item = myList.getMoney(type, name);
        if(item != null)
        {
            Debug.Log("Get money: " + item.cost);
            temp = addMoneyList(item.topic, item.cost);
            moneySystem.DeductMoney(temp.cost);
        }    
        else
        {
            Debug.Log("Can't find : " + name + " - on money list");
        }
    }

    private Cost addMoneyList(string name, float cost)
    {
        Cost temp;
        //if have this money in this list
        Cost money = isOnList(name);
        if (money != null)
        {
            //increase cost
            float total = cost + money.cost;
            Debug.Log(money.topic + " Updated cost - from: " + money.cost + " + with: " + cost);
            money.cost = total;
            temp = moneyList.Where(i => i.topic == name).SingleOrDefault();
            Debug.Log("Updated: '" + temp.topic + "' with total money: " + temp.cost);            
        }
        else
        {
            temp = new Cost(name, cost);
            moneyList.Add(temp);
            Debug.Log("Add new money: '" + name + "' with cost: " + cost);            
        }
        
        debugList = moneyList;
        count = moneyList.Count;
        Debug.Log("Money List Count: " + moneyList.Count);   
        return temp;
    }

    private Cost isOnList(string name)
    {
        Cost temp;
        temp = moneyList.Where(i => i.topic == name).SingleOrDefault();        
        return temp;
    }

    public static List<Cost> GetMoneyList()
    {
        return moneyList;
    }
}
