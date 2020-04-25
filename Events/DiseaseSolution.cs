using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseSolution : MonoBehaviour
{  
    public void solutionA(Question activeQuestion)
    {
        //ใส่ปุ๋ยที่มีไนโตรเจนที่เหมาะสม +1
        if (activeQuestion.topic == "โรคขอบใบแห้ง") 
        {
            //Do something
            Events.BacterialBlight = false;
        }
        //ใส่ปุ๋ยโปแตสเซียมคลอไรด์ +1
        else if (activeQuestion.topic == "โรคใบจุดสีน้ำตาล")
        {
            //Do something
            Events.BrownSpot = false;
        }
        //ฉีดพ่นสารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคเมล็ดด่าง")
        {
            //Do something
            Events.DirtyPanicle = false;
        }
        //ใส่ปุ๋ยที่มีไนโตรเจนเยอะๆ +0
        else if (activeQuestion.topic == "โรคไหม้")
        {
            //Do something
            Events.RiceBlast = false;
        }
        //กำจัดวัชพืชใกล้แหล่งน้ำ +1
        else if (activeQuestion.topic == "โรคใบหงิก")
        {
            //Do something
            Events.RaggedStunt = false;
        }
        //ใช้สารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคกาบใบแห้ง")
        {
            //Do something
            Events.SheathBlight = false;
        }
    }

    public void solutionB(Question activeQuestion)
    {
        //ระบายน้ำออกจากแปลง +0
        if (activeQuestion.topic == "โรคขอบใบแห้ง")
        {
            //Do something
            Events.BacterialBlight = false;
        }
        //ปล่อยให้หายเองตามธรรมชาติ +0
        else if (activeQuestion.topic == "โรคใบจุดสีน้ำตาล")
        {
            //Do something
            Events.BrownSpot = false;
        }
        //กำจัดวัชพืช +0
        else if (activeQuestion.topic == "โรคเมล็ดด่าง")
        {
            //Do something
            Events.DirtyPanicle = false;
        }
        //ฉีดพ่นสารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคไหม้")
        {
            //Do something
            Events.RiceBlast = false;
        }
        //ฉีดยาป้องกันโรค +0
        else if (activeQuestion.topic == "โรคใบหงิก")
        {
            //Do something
            Events.RaggedStunt = false;
        }
        //กำจัดวัชพืช +1
        else if (activeQuestion.topic == "โรคกาบใบแห้ง")
        {
            //Do something
            Events.SheathBlight = false;
        }
    }
}
