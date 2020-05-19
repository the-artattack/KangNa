using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseSolution : MonoBehaviour
{
    public MoneyController moneyController;
    private Evaluation evaluation;
    private MoneyDisplay moneyDisplay;

    private void Start()
    {
        evaluation = GameObject.FindObjectOfType<Evaluation>();
        moneyDisplay = GameObject.FindObjectOfType<MoneyDisplay>();
    }

    public void solutionA(Question activeQuestion, SimulateParameters parameters)
    {
        //ใส่ปุ๋ยที่มีไนโตรเจนที่เหมาะสม +1
        if (activeQuestion.topic == "โรคขอบใบแห้ง") 
        {
            //Do something
            Debug.Log("ใส่ปุ๋ยที่มีไนโตรเจนที่เหมาะสม");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            evaluation.increaseScore();
            Events.BacterialBlight = false;
            moneyController.bill("ค่าวัสดุ", "ค่าปุ๋ย");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ค่าปุ๋ย", "ค่าแรงงาน", "ค่าดูแลรักษา");

        }
        //ใส่ปุ๋ยโปแตสเซียมคลอไรด์ +1
        else if (activeQuestion.topic == "โรคใบจุดสีน้ำตาล")
        {
            //Do something
            Debug.Log("ใส่ปุ๋ยโปแตสเซียมคลอไรด์");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            evaluation.increaseScore();
            Events.BrownSpot = false;
            moneyController.bill("ค่าวัสดุ", "ค่าปุ๋ย");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ค่าปุ๋ย", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ฉีดพ่นสารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคเมล็ดด่าง")
        {
            //Do something
            Debug.Log("ฉีดพ่นสารป้องกันกำจัดเชื้อรา");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            evaluation.increaseScore();
            Events.DirtyPanicle = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดโรค");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดโรค", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ใส่ปุ๋ยที่มีไนโตรเจนเยอะๆ +0
        else if (activeQuestion.topic == "โรคไหม้")
        {
            //Do something
            Debug.Log("ใส่ปุ๋ยที่มีไนโตรเจนเยอะๆ");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 4);
            Events.RiceBlast = false;
            moneyController.bill("ค่าวัสดุ", "ค่าปุ๋ย");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ค่าปุ๋ย", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //กำจัดวัชพืชใกล้แหล่งน้ำ +1
        else if (activeQuestion.topic == "โรคใบหงิก")
        {
            //Do something
            Debug.Log("กำจัดวัชพืชใกล้แหล่งน้ำ");
            evaluation.increaseScore();
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
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ใช้สารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคกาบใบแห้ง")
        {
            //Do something
            Debug.Log("ใช้สารป้องกันกำจัดเชื้อรา");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 3);
            evaluation.increaseScore();
            Events.SheathBlight = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดโรค");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดโรค", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
    }

    public void solutionB(Question activeQuestion, SimulateParameters parameters)
    {
        //ระบายน้ำออกจากแปลง +0
        if (activeQuestion.topic == "โรคขอบใบแห้ง")
        {
            //Do something
            Debug.Log("ระบายน้ำออกจากแปลง");
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
            Debug.Log("ปล่อยให้หายเองตามธรรมชาติ");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);

            Events.BrownSpot = false;
        }
        //กำจัดวัชพืช +0
        else if (activeQuestion.topic == "โรคเมล็ดด่าง")
        {
            //Do something
            Debug.Log("กำจัดวัชพืช");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);

            Events.DirtyPanicle = false;
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ฉีดพ่นสารป้องกันกำจัดเชื้อรา +1
        else if (activeQuestion.topic == "โรคไหม้")
        {
            //Do something
            Debug.Log("ฉีดพ่นสารป้องกันกำจัดเชื้อรา");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
            evaluation.increaseScore();
            Events.RiceBlast = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดโรค");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดโรค", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ฉีดยาป้องกันโรค +0
        else if (activeQuestion.topic == "โรคใบหงิก")
        {
            //Do something    
            Debug.Log("ฉีดยาป้องกันโรค");
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
            moneyController.bill("ค่าวัสดุ", "ยากำจัดโรค");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดโรค", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ใส่ปุ๋ย +0
        else if (activeQuestion.topic == "โรคกาบใบแห้ง")
        {
            //Do something
            Debug.Log("ใส่ปุ๋ย");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 7);
            Events.SheathBlight = false;
            moneyController.bill("ค่าวัสดุ", "ค่าปุ๋ย");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ค่าปุ๋ย", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
    }
}
