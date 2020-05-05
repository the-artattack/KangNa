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

    public Money getMoney(string type, string name)
    {
        Money money = null;
        if (type.Equals("ค่าแรงงาน"))
        {
            money = getLabor(name);
        }
        else if (type.Equals("ค่าวัสดุ"))
        {
            money = getMeterial(name);
        }
        else if (type.Equals("ค่ารถ"))
        {
            money = getTruck(name);
        }
        else if (type.Equals("ค่าเช่าที่ดิน"))
        {
            money = rentLand;
        }
        else if (type.Equals("ค่าเสื่อมอุปกรณ์"))
        {
            money = equipmentDepreciation;
        }
        else if (type.Equals("ค่าเสียโอกาสอุปกรณ์"))
        {
            money = opportunityEquipment;
        }
        else if (type.Equals("ค่าฟาง"))
        {
            money = straw;
        }
        return money;
    }

    private Money getLabor(string name)
    {
        Money temp = labor.Where(obj => obj.name == name).SingleOrDefault();
        return temp;
    }
    private Money getMeterial(string name)
    {
        Money temp = meterial.Where(obj => obj.name == name).SingleOrDefault();
        return temp;
    }
    private Money getTruck(string name)
    {
        Money temp = truck.Where(obj => obj.name == name).SingleOrDefault();
        return temp;
    }    
}
