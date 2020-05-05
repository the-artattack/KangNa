using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InsectQuestion : MonoBehaviour
{
    public List<Question> insects;

    private new string name;
    public string getInsectName(string thaiName)
    {
        if(thaiName.Equals("เพลี้ยไฟ"))
        {
            name = "Thrips";
        }
        else if(thaiName.Equals("เพลี้ยกระโดดหลังขาว"))
        {
            name = "WhiteBackedPlantHopper";
        }
        else if (thaiName.Equals("เพลี้ยจักจั่นสีเขียว"))
        {
            name = "GreenLeafHopper";
        }
        else if (thaiName.Equals("หนอนห่อใบข้าว"))
        {
            name = "LeafFolder";
        }
        else if (thaiName.Equals("เพลี้ยกระโดดสีน้ำตาล"))
        {
            name = "BrownPlantHopper";
        }
        else if (thaiName.Equals("แมลงหล่า"))
        {
            name = "RiceBlackBug";
        }
        else if (thaiName.Equals("แมลงบั่ว"))
        {
            name = "RiceGallMidges";
        }
        else if (thaiName.Equals("หนอนปลอกข้าว"))
        {
            name = "RiceCaseWorm";
        }
        else if (thaiName.Equals("แมลงสิง"))
        {
            name = "StinkBug";
        }
        return name;
    }

    public Question getQuestion(string str)
    {        
        Question question = insects.Where(obj => obj.topic == str).SingleOrDefault();
        Debug.Log("Get question from: " + str);
        Debug.Log("result: " + question.topic);
        return question;
    }
}
