using UnityEngine;

public class RiceName : MonoBehaviour
{
   public static string GetRiceName(string riceName)
    {
        string name = "";
        if (riceName.Equals("PathumThani1_PerKg"))
            name = "ข้าวปทุมธานี 1";
        else if (riceName.Equals("RD43_PerKg"))
            name = "ข้าว กข43";
        else if (riceName.Equals("SanPahTawng1_PerKg"))
            name = "ข้าวสันป่าตอง 1";
        else if (riceName.Equals("JasmineRice_PerKg"))
            name = "ข้าวหอมมะลิ";
        else if (riceName.Equals("RD15_PerKg"))
            name = "ข้าว กข15";
        else if (riceName.Equals("SungYodRice_PerKg"))
            name = "ข้าวสังหยด";
        return name;
    }

    public static string getRiceKey(string riceName)
    {
        string name = "";
        if(riceName.Equals("ข้าวปทุมธานี 1"))
        {
            name = "PathumThani1_PerKg";
        }
        else if (riceName.Equals("ข้าว กข43"))
        {
            name = "RD43_PerKg";
        }
        else if (riceName.Equals("ข้าวสันป่าตอง 1"))
        {
            name = "SanPahTawng1_PerKg";
        }
        else if (riceName.Equals("ข้าวหอมมะลิ"))
        {
            name = "JasmineRice_PerKg";
        }
        else if (riceName.Equals("ข้าว กข15"))
        {
            name = "RD15_PerKg";
        }
        else if (riceName.Equals("ข้าวสังหยด"))
        {
            name = "SungYodRice_PerKg";
        }
        return name;
    }

    public static string getRiceType(string riceType)
    {
        string name;
        if (riceType.Equals("ข้าวไวแสง"))
        {
            name = "PhotoperiodSensitivityRice";
        }
        else
        {
            name = "PhotoperiodInsensitivityRice";
        }
        return name;
    }

}
