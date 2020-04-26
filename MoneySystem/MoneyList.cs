using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoneyList : MonoBehaviour
{    
    public List<Money> labor;
    public List<Money> meterial;
    public List<Money> truck;
    public Money rentLand;
    public Money equipmentDepreciation;
    public Money opportunityEquipment;
    public Money straw;

    public Money getLabor(string name)
    {
        Money temp = labor.Where(obj => obj.name == name).SingleOrDefault();
        return temp;
    }
    public Money getMeterial(string name)
    {
        Money temp = meterial.Where(obj => obj.name == name).SingleOrDefault();
        return temp;
    }
    public Money getTruck(string name)
    {
        Money temp = truck.Where(obj => obj.name == name).SingleOrDefault();
        return temp;
    }
    public Money getRentLand
    {
        get { return rentLand; }
    }
    public Money getEquipmentDepreciation
    {
        get { return equipmentDepreciation; }
    }
    public Money getOpportunityEquipment
    {
        get { return rentLand; }
    }
    public Money getStraw
    {
        get { return straw; }
    }
}
