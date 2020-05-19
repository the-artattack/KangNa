using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{
    public GameObject cardWindow;
    public GameObject blackTransparency;

    public Text headerText;
    public Button choiceA;
    public Button choiceB;
    public Image aImage;
    public Image bImage;

    private Card activeCard;
    public MoneyController moneyController;

    public new Animation animation;
    private string selectedChoice;

    public TimeControl timeControl;
    private MoneyDisplay moneyDisplay;

    private void Start()
    {
        moneyDisplay = GameObject.FindObjectOfType<MoneyDisplay>();
        choiceA.onClick.AddListener(SelectChoiceA);
        choiceB.onClick.AddListener(SelectChoiceB);
    }        

    public void OpenCardWindow(Card card)
    {
        activeCard = card;
        cardWindow.SetActive(true);
        blackTransparency.SetActive(true);
        headerText.text = card.description;
        aImage.sprite = card.aImage;
        bImage.sprite = card.bImage;
        choiceA.GetComponentInChildren<Text>().text = card.choiceA;
        choiceB.GetComponentInChildren<Text>().text = card.choiceB;
        card.Print();
    }

    public void CloseCardWindow()
    {
        cardWindow.SetActive(false);
        blackTransparency.SetActive(false);
        EnableAnimation();
    }

    /** trigger animation */
    private void EnableAnimation()
    {
        if(activeCard.topic == "PlantingMethod")
        {
            animation.PlantingEnable(selectedChoice);
        }
        else
        {
            animation.HarvestEnable(selectedChoice);
        }
    }

    private void SelectChoiceA()
    {
        printSelectedChoice();
        timeControl.Play();
        //หว่านเอง
        if (activeCard.topic == "PlantingMethod")
        {
            //Spend money
            moneyController.bill("ค่าแรงงาน", "ค่าปลูก");
            moneyDisplay.notifyMoney("ค่าแรงงาน", "ค่าปลูก");
        }
        //เก็บเกียวเอง
        else if (activeCard.topic == "HarvestMethod")
        {
            //Spend money
            moneyController.bill("ค่าแรงงาน", "ค่าเก็บเกี่ยว");
            moneyDisplay.notifyMoney("ค่าแรงงาน", "ค่าเก็บเกี่ยว");
        }
        CloseCardWindow();
    }

    private void SelectChoiceB()
    {
        printSelectedChoice();
        timeControl.Play(); //Resume 
        //รถหว่านเมล็ด
        if (activeCard.topic == "PlantingMethod")
        {
            //Spend money
            moneyController.bill("ค่ารถ", "รถหว่านเมล็ด");
            moneyController.bill("ค่าวัสดุ", "ค่าน้ำมัน");
            moneyDisplay.notifyMoney("ค่ารถ", "รถหว่านเมล็ด", "ค่าวัสดุ", "ค่าน้ำมัน");
        }
        //รถเก็บเกี่ยว
        else if (activeCard.topic == "HarvestMethod")
        {
            //Spend money
            moneyController.bill("ค่ารถ", "รถเก็บเกี่ยว");
            moneyController.bill("ค่าวัสดุ", "ค่าน้ำมัน");
            moneyDisplay.notifyMoney("ค่ารถ", "รถเก็บเกี่ยว", "ค่าวัสดุ", "ค่าน้ำมัน");
        }
        CloseCardWindow();
    }

    private void printSelectedChoice()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;        
        if (buttonValue == "methodA")
        {
            selectedChoice = activeCard.choiceA;
        }
        else
        {
            selectedChoice = activeCard.choiceB;
        }
        Debug.Log(activeCard.topic + ": " + selectedChoice);
    }
}
