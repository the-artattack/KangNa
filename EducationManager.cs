using System.Collections;
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

    public MoneyController moneyController;
    // Start is called before the first frame update
    void Start()
    {
        saveModeToFirebase();
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
        FirebaseInit.Instance.area = landSize.text;
        Debug.Log(FirebaseInit.Instance.auth.CurrentUser.UserId);
        FirebaseInit.Instance._database.RootReference
                .Child("Education")
                .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
                .Child("TypeOfLand").Child(buttonValue).SetValueAsync(landSize.text);        
        SelectLandArea.SetActive(false);
        blackTransparency.SetActive(false);

        Debug.Log("Button Value: " + buttonValue);
        if(buttonValue == "RentLand")
        {
            StartCoroutine(bill());
        }
        SceneChanger.nextScene(5);
    }
    IEnumerator bill()
    {                
        moneyController.bill("ค่าเช่าที่ดิน", "ค่าเช่าที่ดิน");
        yield return new WaitForSeconds(3.0f);        
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
            SceneChanger.nextScene(7);
        });
    }

    private void onNoBtnPressed()
    {
        Debug.Log("No button pressed!");
        blackTransparency.SetActive(false);
        confirmationBox.SetActive(false);
    }

    private void saveModeToFirebase()
    {
        FirebaseInit.Instance._database.RootReference
            .Child("Users").Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
            .Child("Mode")
            .SetValueAsync(getMode(FirebaseInit.Instance.mode));
        Debug.Log("Save mode: " + FirebaseInit.Instance.mode);
    }
    private string getMode(int mode)
    {
        if (mode == 1)
        {
            return "expert";
        }
        else
        {
            return "beginner";
        }
    }
}
