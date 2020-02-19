using System.Collections.Generic;
using UnityEngine;

public class CostList : MonoBehaviour
{
    public static CostList CostInstance { get; private set; }
    public List<Dictionary<string, object>> otherCost = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> insensitivityRice = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> sensitivityRice = new List<Dictionary<string, object>>();
    public int otherCostSize = 0;
    public int insensitivityRiceSize = 0;
    public int sensitivityRiceSize = 0;

    void Awake()
    {
        if (CostInstance == null)
        {
            CostInstance = this;
            otherCost.Clear();
            insensitivityRice.Clear();
            sensitivityRice.Clear();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(otherCostSize != GetOtherCostSize())
        {
            otherCostSize = GetOtherCostSize();
        }
        if(insensitivityRiceSize != GetInsensitivitySize())
        {
            insensitivityRiceSize = GetInsensitivitySize();
        }
        if (sensitivityRiceSize != GetSensitivitySize()) 
        {
            sensitivityRiceSize = GetSensitivitySize();
        }
    }
   
    private int GetOtherCostSize()
    {
        var count = 0;
        foreach (var x in otherCost)
        {
            foreach (var key in x.Keys)
                count++;            
        }
        return count;
    }

    private int GetInsensitivitySize()
    {
        var count = 0;
        foreach (var x in insensitivityRice)
        {
            foreach (var key in x.Keys)
                count++;
        }
        return count;
    }

    private int GetSensitivitySize()
    {
        var count = 0;
        foreach (var x in sensitivityRice)
        {
            foreach (var key in x.Keys)
                count++;
        }
        return count;
    }

    public object GetPriceOtherCost(string key)
    {
        return otherCost[0][key];
    }

    public object GetPriceInsensitivityRice(string key)
    {
        return insensitivityRice[0][key];
    }

    public object GetPriceSensitivityRice(string key)
    {
        return sensitivityRice[0][key];
    }
}
