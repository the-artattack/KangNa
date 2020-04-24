using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseQuestion : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void solutionA(Question activeQuestion)
    {
        //ใส่ปุ๋ยที่มีไนโตรเจนที่เหมาะสม +1
        if (activeQuestion.topic == "โรคขอบใบแห้ง") 
        {            
            //Do something
        }
        //ใส่ปุ๋ยโปแตสเซียมคลอไรด์ +1
        else if (activeQuestion.topic == "โรคใบจุดสีน้ำตาล")
        {
            //
            //Do something
        }
        //ฉีดพ่นสารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคเมล็ดด่าง")
        {
            //Do something
        }
        //ใส่ปุ๋ยที่มีไนโตรเจนเยอะๆ +0
        else if (activeQuestion.topic == "โรคไหม้")
        {
            //Do something
        }
        //กำจัดวัชพืชใกล้แหล่งน้ำ +1
        else if (activeQuestion.topic == "โรคใบหงิก")
        {
            //Do something
        }
        //ใช้สารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคกาบใบแห้ง")
        {
            //Do something
        }
    }

    public void solutionB(Question activeQuestion)
    {
        //ระบายน้ำออกจากแปลง +0
        if (activeQuestion.topic == "โรคขอบใบแห้ง")
        {
            //Do something
        }
        //ปล่อยให้หายเองตามธรรมชาติ +0
        else if (activeQuestion.topic == "โรคใบจุดสีน้ำตาล")
        {
            //Do something
        }
        //กำจัดวัชพืช +0
        else if (activeQuestion.topic == "โรคเมล็ดด่าง")
        {
            //Do something
        }
        //ฉีดพ่นสารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคไหม้")
        {
            //Do something
        }
        //ฉีดยาป้องกันโรค +0
        else if (activeQuestion.topic == "โรคใบหงิก")
        {
            //Do something
        }
        //กำจัดวัชพืช +1
        else if (activeQuestion.topic == "โรคกาบใบแห้ง")
        {
            //Do something
        }
    }
}
