using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseSolution : MonoBehaviour
{
    public void solutionA(Question activeQuestion, SimulateParameters parameters)
    {
        //ใส่ปุ๋ยที่มีไนโตรเจนที่เหมาะสม +1
        if (activeQuestion.topic == "โรคขอบใบแห้ง") 
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            Evaluation.increaseScore(1);
            Events.BacterialBlight = false;
        }
        //ใส่ปุ๋ยโปแตสเซียมคลอไรด์ +1
        else if (activeQuestion.topic == "โรคใบจุดสีน้ำตาล")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            Evaluation.increaseScore(1);
            Events.BrownSpot = false;
        }
        //ฉีดพ่นสารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคเมล็ดด่าง")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            Evaluation.increaseScore(1);
            Events.DirtyPanicle = false;
        }
        //ใส่ปุ๋ยที่มีไนโตรเจนเยอะๆ +0
        else if (activeQuestion.topic == "โรคไหม้")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 4);

            Events.RiceBlast = false;
        }
        //กำจัดวัชพืชใกล้แหล่งน้ำ +1
        else if (activeQuestion.topic == "โรคใบหงิก")
        {
            Evaluation.increaseScore(1);
            //Do something
            if (RiceTab.RicePhase == "ระยะต้นกล้า")
            {
                parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 3);
            }
            else if (RiceTab.RicePhase == "ระยะแตกกอ")
            {
                parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
            }
            else if (RiceTab.RicePhase == "ระยะตั้งท้อง")
            {
                parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            }

            Events.RaggedStunt = false;
        }
        //ใช้สารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคกาบใบแห้ง")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 3);
            Evaluation.increaseScore(1);
            Events.SheathBlight = false;
        }
    }

    public void solutionB(Question activeQuestion, SimulateParameters parameters)
    {
        //ระบายน้ำออกจากแปลง +0
        if (activeQuestion.topic == "โรคขอบใบแห้ง")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 3);
            parameters.WaterVolume = 0;
            //Force open canal
            if (parameters.UseCanal == false)
            {
                parameters.UseCanal = true;
            }

            Events.BacterialBlight = false;
        }
        //ปล่อยให้หายเองตามธรรมชาติ +0
        else if (activeQuestion.topic == "โรคใบจุดสีน้ำตาล")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);

            Events.BrownSpot = false;
        }
        //กำจัดวัชพืช +0
        else if (activeQuestion.topic == "โรคเมล็ดด่าง")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);

            Events.DirtyPanicle = false;
        }
        //ฉีดพ่นสารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคไหม้")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
            Evaluation.increaseScore(1);
            Events.RiceBlast = false;
        }
        //ฉีดยาป้องกันโรค +0
        else if (activeQuestion.topic == "โรคใบหงิก")
        {
            //Do something            
            if (RiceTab.RicePhase == "ระยะต้นกล้า")
            {
                parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);
            }
            else if (RiceTab.RicePhase == "ระยะแตกกอ")
            {
                parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 4);
            }
            else if (RiceTab.RicePhase == "ระยะตั้งท้อง")
            {
                parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 3);
            }

            Events.RaggedStunt = false;
        }
        //ใส่ปุ๋ย +0
        else if (activeQuestion.topic == "โรคกาบใบแห้ง")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 7);
            Events.SheathBlight = false;
        }
    }
}
