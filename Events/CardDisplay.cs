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

    private MoneyInterface moneySystem;
    public MoneyList moneyList;

    private void Start()
    {
        choiceA.onClick.AddListener(SelectChoiceA);
        choiceB.onClick.AddListener(SelectChoiceB);
        moneySystem = FindObjectOfType<PlayerCurrency>().GetComponent<MoneyInterface>();
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
    }

    private void SelectChoiceA()
    {
        printSelectedChoice();
        //หว่านเอง
        if (activeCard.topic == "PlantingMethod")
        {
            //Spend money
            moneySystem.DeductMoney(moneyList.getLabor("ค่าจ้างคน").cost);
        }
        //เก็บเกียวเอง
        else if (activeCard.topic == "HarvestMethod")
        {
            //Spend money
            moneySystem.DeductMoney(moneyList.getLabor("ค่าจ้างคน").cost);
        }
        CloseCardWindow();
    }

    private void SelectChoiceB()
    {
        printSelectedChoice();
        //รถหว่านเมล็ด
        if (activeCard.topic == "PlantingMethod")
        {
            //Spend money
            moneySystem.DeductMoney(moneyList.getTruck("รถหว่านเมล็ด").cost, false);
            moneySystem.DeductMoney(moneyList.getMeterial("ค่าน้ำมัน").cost);
        }
        //รถเก็บเกี่ยว
        else if (activeCard.topic == "HarvestMethod")
        {
            //Spend money
            moneySystem.DeductMoney(moneyList.getTruck("รถเก็บเกี่ยว").cost);
            moneySystem.DeductMoney(moneyList.getMeterial("ค่าน้ำมัน").cost);
        }
        CloseCardWindow();
    }

    private void printSelectedChoice()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        string selectedChoice;
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
