using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiseaseQuestion : MonoBehaviour
{
    public List<Question> diseases;

    private new string name;
    public string getDiseaseName(string thaiName)
    {
        if (thaiName.Equals("โรคขอบใบแห้ง"))
        {
            name = "BacterialBlight";
        }
        else if (thaiName.Equals("โรคไหม้"))
        {
            name = "RiceBlast";
        }
        else if (thaiName.Equals("โรคกาบใบแห้ง"))
        {
            name = "SheathBlight";
        }
        else if (thaiName.Equals("โรคใบหงิก"))
        {
            name = "RaggedStunt";
        }
        else if (thaiName.Equals("โรคเมล็ดด่าง"))
        {
            name = "DirtyPanicle";
        }
        else if (thaiName.Equals("โรคใบจุดสีน้ำตาล"))
        {
            name = "BrownSpot";
        }       
        return name;
    }

    public Question getQuestion(string str)
    {        
        Question question = diseases.Where(obj => obj.topic == str).SingleOrDefault();
        Debug.Log("Get question from: " + str);
        Debug.Log("result: " + question.topic);
        return question;
    }
}
