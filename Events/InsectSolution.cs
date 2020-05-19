using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectSolution : MonoBehaviour
{
    public MoneyController moneyController;
    private MoneyDisplay moneyDisplay;
    private Evaluation evaluation;

    private void Start()
    {
        evaluation = GameObject.FindObjectOfType<Evaluation>();
        moneyDisplay = GameObject.FindObjectOfType<MoneyDisplay>();
    }

    public void solutionA(Question activeQuestion, SimulateParameters parameters)
    {
        //ใช้แสงไฟฟ้าล่อแมลงและกำจัด +1
        if (activeQuestion.topic == "แมลงหล่า")
        {
            //Do something
            Debug.Log("ใช้แสงไฟฟ้าล่อแมลงและกำจัด");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
            evaluation.increaseScore();
            Events.RiceBlackBug = false;
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าแรงงาน", "ค่าดูแลรักษา");
        }

        //ปล่อยให้ตัวมวนเขียวดูดไข่จัดการ +1
        else if (activeQuestion.topic == "เพลี้ยกระโดดสีน้ำตาล")
        {
            //Do something
            Debug.Log("ปล่อยให้ตัวมวนเขียวดูดไข่จัดการ");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
            evaluation.increaseScore();
            Events.BrownPlantHopper = false;
        }
        //จับโดยกับดักแสงไฟ +1
        else if (activeQuestion.topic == "เพลี้ยจักจั่นสีเขียว")
        {
            //Do something
            Debug.Log("จับโดยกับดักแสงไฟ");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            evaluation.increaseScore();
            Events.GreenLeafHopper = false;
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ใส่ปุ๋ยไนโตรเจน 10 กก./ไร่ +0
        else if (activeQuestion.topic == "หนอนห่อใบข้าว")
        {
            //Do something
            Debug.Log("ใส่ปุ๋ยไนโตรเจน 10 กก./ไร่");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 4);

            Events.LeafFolder = false;
            moneyController.bill("ค่าวัสดุ", "ค่าปุ๋ย");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ค่าปุ๋ย", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //กำจัดวัชพืช +1
        else if (activeQuestion.topic == "แมลงบั่ว")
        {
            //Do something
            Debug.Log("กำจัดวัชพืช");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 3);
            evaluation.increaseScore();
            Events.RiceGallMidges = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดวัชพืช");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดวัชพืช", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ปล่อยน้ำเข้าแปลงนา +0
        else if (activeQuestion.topic == "หนอนปลอกข้าว")
        {
            //Do something
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);

            Events.RiceCaseWorm = false;
        }
        //ไขน้ำท่วมยอดข้าวทิ้งไว้ 1-2 วัน +1
        else if (activeQuestion.topic == "เพลี้ยไฟ")
        {
            //Do something
            Debug.Log("ไขน้ำท่วมยอดข้าวทิ้งไว้ 1-2 วัน");
            evaluation.increaseScore();
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 3);
            //Force open canal
            if (parameters.UseCanal == false)
            {
                parameters.UseCanal = true;
            }

            if (parameters.WaterVolume < parameters.WaterBaseLine + 50)
            {
                parameters.WaterVolume = parameters.WaterBaseLine + 50;
            }

            Events.Thrips = false;
        }
        //ใช้ยาฆ่าแมลง +1
        else if (activeQuestion.topic == "แมลงสิง")
        {
            //Do something
            Debug.Log("ใช้ยาฆ่าแมลง");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
            evaluation.increaseScore();
            Events.StinkBug = false;

            moneyController.bill("ค่าวัสดุ", "ยากำจัดแมลง");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดแมลง", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ไขน้ำเข้าแปลงนา +0
        else if (activeQuestion.topic == "เพลี้ยกระโดดหลังขาว")
        {
            //Do something
            Debug.Log("ไขน้ำเข้าแปลงนา");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 7);
            //Force open canal
            if (parameters.UseCanal == false)
            {
                parameters.UseCanal = true;
            }

            Events.WhiteBackedPlantHopper = false;
        }
    }

    public void solutionB(Question activeQuestion, SimulateParameters parameters)
    {
        //ปล่อยน้ำเข้าแปลง +0
        if (activeQuestion.topic == "แมลงหล่า")
        {
            //Do something
            Debug.Log("ปล่อยน้ำเข้าแปลง");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 6);
            //Force open canal
            if (parameters.UseCanal == false)
            {
                parameters.UseCanal = true;
            }

            Events.RiceBlackBug = false;
        }
        //กำจัดตัวมวนเขียวดูดไข่ +0
        else if (activeQuestion.topic == "เพลี้ยกระโดดสีน้ำตาล")
        {
            //Do something
            Debug.Log("กำจัดตัวมวนเขียวดูดไข่");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 6);

            Events.BrownPlantHopper = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดแมลง");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดแมลง", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ใช้ยาฆ่าแมลง +0
        else if (activeQuestion.topic == "เพลี้ยจักจั่นสีเขียว")
        {
            //Do something
            Debug.Log("ใช้ยาฆ่าแมลง");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 4);

            Events.GreenLeafHopper = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดแมลง");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดแมลง", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //กำจัดพืชอาศัย +1
        else if (activeQuestion.topic == "หนอนห่อใบข้าว")
        {
            //Do something
            Debug.Log("กำจัดพืชอาศัย");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 1);
            evaluation.increaseScore();
            Events.LeafFolder = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดวัชพืช");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดวัชพืช", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ใช้ยาฆ่าแมลง +0
        else if (activeQuestion.topic == "แมลงบั่ว")
        {
            //Do something
            Debug.Log("ใช้ยาฆ่าแมลง");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 7);

            Events.RiceGallMidges = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดแมลง");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดแมลง", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ระบายน้ำออกจากแปลงนา +1
        else if (activeQuestion.topic == "หนอนปลอกข้าว")
        {
            //Do something
            Debug.Log("ระบายน้ำออกจากแปลงนา");
            evaluation.increaseScore();
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 4); //only this line for +0 case
            parameters.WaterVolume = 0;
            //Force open canal
            if (parameters.UseCanal == false)
            {
                parameters.UseCanal = true;
            }

            Events.RiceCaseWorm = false;
        }
        //ใช้ยาฆ่าแมลง +0
        else if (activeQuestion.topic == "เพลี้ยไฟ")
        {
            //Do something
            Debug.Log("ใช้ยาฆ่าแมลง");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 10);

            Events.Thrips = false;
            moneyController.bill("ค่าวัสดุ", "ยากำจัดแมลง");
            moneyController.bill("ค่าแรงงาน", "ค่าดูแลรักษา");
            moneyDisplay.notifyMoney("ค่าวัสดุ", "ยากำจัดแมลง", "ค่าแรงงาน", "ค่าดูแลรักษา");
        }
        //ไขน้ำเข้าแปลงนา  +0
        else if (activeQuestion.topic == "แมลงสิง")
        {
            //Do something
            Debug.Log("ไขน้ำเข้าแปลงนา");
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);
            Events.StinkBug = false;
        }
        //ไขน้ำออกจากแปลงนา +1
        else if (activeQuestion.topic == "เพลี้ยกระโดดหลังขาว")
        {
            //Do something
            Debug.Log("ไขน้ำออกจากแปลงนา");
            evaluation.increaseScore();
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 6); //only this line for +0 case
            parameters.WaterVolume = 0;
            //Force open canal
            if (parameters.UseCanal == false)
            {
                parameters.UseCanal = true;
            }
            Events.WhiteBackedPlantHopper = false;
        }
    }
}
