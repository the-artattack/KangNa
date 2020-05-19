using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

/** For pass values to another scene:
    - SimulateParameters
    - List of money (that used in simulation)
*/

public class Parameters : MonoBehaviour
{
    protected static SimulateParameters parameters;
    protected static List<Cost> userMoneyList = new List<Cost>();
    protected static MoneyList moneyList;
    protected static DateTime date;
    protected static string riceType = "";
    protected static int riceSeedCost = 0;
    protected static double ricePricePredicted = 0;
    protected static int score = 0;
    protected static int maxScore = 0;

    public static string print()
    {
        string text;
        Debug.Log("Rice quantity: " + parameters.RiceQuantity);
        Debug.Log("Water volume: " + parameters.WaterVolume);
        Debug.Log("List of money count: " + userMoneyList.Count);
        text = "Rice quantity: " + parameters.RiceQuantity + "\n"
            + "Water volume: " + parameters.WaterVolume + "\n"
            + "List of money count: " + userMoneyList.Count + "\n"
            + "Harvest date: " + date + "\n"
            + "Rice Type: " + riceType;
        return text;
    }

    public static void SetParameters(SimulateParameters data)
    {
        parameters = data;
    }

    public static void SetUserMoneyList(List<Cost> list)
    {
        userMoneyList = list;
    }
    public static void SetMoneyList(MoneyList money)
    {
        moneyList = money;
    }
    public static void SetDate(DateTime dateTime)
    {
        date = dateTime;
    }
    public static void SetRiceType(string type)
    {
        riceType = type;
    }
    public static void SetSeedCost(int cost)
    {
        riceSeedCost = cost;
    }
    public static void SetPricePrediction(double price)
    {
        ricePricePredicted = price;
    }
    public static void SetScore(int s)
    {
        score = s;
    }
    public static void SetMaxScore(int max)
    {
        maxScore = max;
    }
    public static SimulateParameters GetSimulateParameters()
    {
        return parameters;
    }
    public static List<Cost> GetUserMoneyList()
    {
        return userMoneyList;
    }
    public static MoneyList GetMoneyList()
    {
        return moneyList;
    }
    public static DateTime GetDate()
    {
        return date;
    }
    public static string GetRiceType()
    {
        return riceType;
    }
    public static int GetSeedCost()
    {
        return riceSeedCost;
    }
    public static double GetPricePrediction()
    {
        return ricePricePredicted;
    }
    public static int GetScore()
    {
        return score;
    }
    public static int GetMaxScore()
    {
        return maxScore;
    }
    
}
