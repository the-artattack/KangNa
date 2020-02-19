using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EducationManager : MonoBehaviour
{
    public Button riceTypeA;
    public Button riceTypeB;
    public Button landTypeA;
    public Button landTypeB;
    public GameObject confirmationBox;
    public GameObject SelectLandArea;
    public GameObject blackTransparency;
    public GameObject shopBox;
    public Text riceTypeText;
    public InputField landSize;
    public Button close;
    private string buttonValue;

    int currentAmount = 0;
    int increasePerClick = 10;
    public int max = 500;
    public int min = 10;

    // Start is called before the first frame update
    void Start()
    {

        if (riceTypeA != null && riceTypeB != null)
        {
            Button riceAButton = riceTypeA.GetComponent<Button>();
            riceAButton.onClick.AddListener(OnSelectRice);
            Button riceBButton = riceTypeB.GetComponent<Button>();
            riceBButton.onClick.AddListener(OnSelectRice);
        }
        else
        {
            Button landAButton = landTypeA.GetComponent<Button>();
            landAButton.onClick.AddListener(OnSelectLand);
            Button landBButton = landTypeB.GetComponent<Button>();
            landBButton.onClick.AddListener(OnSelectLand);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CloseBox()
    {
        Debug.Log("Close box!");
        confirmationBox.SetActive(false);
        SelectLandArea.SetActive(false); 
        blackTransparency.SetActive(false);
    }
    void OnSelectLand()
    {
        blackTransparency.SetActive(true);
        buttonValue = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonValue);
        //riceTypeText.text = buttonValue;

        if (buttonValue == "RentLand")
        {
            confirmationBox.SetActive(true);
            Button okButton = confirmationBox.transform.Find("SelectSizeButton").GetComponent<Button>();
            Button closeButton = confirmationBox.transform.Find("CloseButton").GetComponent<Button>();
            closeButton.onClick.AddListener(CloseBox);
            okButton.onClick.AddListener(SelectLandSize);       
        } 
        else
        {
            SelectLandSize();
        }
    }

    private void SelectLandSize()
    {
        confirmationBox.SetActive(false);
        SelectLandArea.SetActive(true);
        Button increaseButton = SelectLandArea.transform.Find("ButtonUp").GetComponent<Button>();
        Button decreaseButton = SelectLandArea.transform.Find("ButtonDown").GetComponent<Button>();
        Button okButton = SelectLandArea.transform.Find("okButton").GetComponent<Button>();
        Button closeButton = SelectLandArea.transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(CloseBox);
        increaseButton.onClick.AddListener(IncreaseCurrentLandSize);
        decreaseButton.onClick.AddListener(DecreaseCurrentLandSize);

        okButton.onClick.AddListener(LandSizeConfirm);        
    }

    void LandSizeConfirm()
    {
        Debug.Log(FirebaseInit.Instance.auth.CurrentUser.UserId);
        FirebaseInit.Instance._database.RootReference
                .Child("Education")
                .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
                .Child("TypeOfLand").Child(buttonValue).SetValueAsync(landSize.text);

        //Set parameter : area on scene Simulation
        //Parameters.variableInstance.area = landSize.text;

        SceneChanger.nextScene(7);
    }
    void IncreaseCurrentLandSize()
    {
        Debug.Log("Increase number");
        // clamp current value between min-max
        currentAmount = Mathf.Clamp(currentAmount + increasePerClick, min, max);
        landSize.text = currentAmount.ToString();
    }

    void DecreaseCurrentLandSize()
    {
        Debug.Log("Decrease number");
        currentAmount = Mathf.Clamp(currentAmount - increasePerClick, min, max);
        landSize.text = currentAmount.ToString();
    }
    void OnSelectRice()
    {
        blackTransparency.SetActive(true);
        //string userId = FirebaseInit.Instance.auth.CurrentUser.UserId; 
        buttonValue = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(buttonValue);
        riceTypeText.text = buttonValue;
        confirmationBox.SetActive(true);

        Button yesBtn = confirmationBox.transform.Find("YesButton").GetComponent<Button>();
        Button noBtn = confirmationBox.transform.Find("NoButton").GetComponent<Button>();
        yesBtn.onClick.AddListener(OnYesBtnPressed);
        noBtn.onClick.AddListener(onNoBtnPressed);
    }

    private void OnYesBtnPressed()
    {
        Debug.Log("Yes button pressed!");
        Debug.Log(FirebaseInit.Instance.auth.CurrentUser.UserId);
        FirebaseInit.Instance._database.RootReference
                .Child("Education")
                .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
                .Child("TypeOfRice").SetValueAsync(buttonValue);
        FirebaseInit.Instance.riceType = buttonValue;
        confirmationBox.SetActive(false);
        shopBox.SetActive(true);
        Button goShop = shopBox.transform.GetComponent<Button>();
        goShop.onClick.AddListener(() => {
            SceneChanger.nextScene(9);
        });
    }

    private void onNoBtnPressed()
    {
        Debug.Log("No button pressed!");
        blackTransparency.SetActive(false);
        confirmationBox.SetActive(false);
    } 
   
}
