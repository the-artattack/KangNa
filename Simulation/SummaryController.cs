using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryController : MonoBehaviour
{
    private SummaryDisplay summaryDisplay;
    private Summary summary;
    public GameObject millBoard;
    public GameObject IncomeBoard;
    public GameObject bill;
    public GameObject summaryBoard;
    public GameObject blackTransparency;

    public Animator animator;
    public Button nextButton;
    public Button okButton;


    private void Awake()
    {
        summaryDisplay = GameObject.FindObjectOfType<SummaryDisplay>();
        summary = GameObject.FindObjectOfType<Summary>();
    }
    private void Start()
    {        
        bill.SetActive(false);
        summaryBoard.SetActive(false);
        millBoard.SetActive(false);
        IncomeBoard.SetActive(false);
        blackTransparency.SetActive(false);
        nextButton.onClick.AddListener(createSummary);
        okButton.onClick.AddListener(EndingSummary);
        summary.printMoneyList();
    }

    /** แสดงราคารับซื้อข้าวจากโรงสีประจำวันนี้ */
    public void showMillBoard()
    {
        millBoard.SetActive(true);
        blackTransparency.SetActive(true);
        Invoke("playAnimation", 10.0f);
    }

    /** play truck animation */
    private void playAnimation()
    {
        millBoard.SetActive(false);
        blackTransparency.SetActive(false);
        animator.SetBool("isMove", true);
        StartCoroutine(showIncomeBoard());
    }

    /** แสดงหน้ารายรับที่ได้จากโรงสีข้าว */
    private IEnumerator showIncomeBoard()
    {
        yield return new WaitForSeconds(6.8f);
        IncomeBoard.SetActive(true);
        blackTransparency.SetActive(true);
        summaryDisplay.createIncomeBoard();
        Invoke("enableSummary", 10.0f);
    }
    /** แสดงรายการรายรับ - รายจ่าย */ 
    private void enableSummary()
    {
        bill.SetActive(true);
        IncomeBoard.SetActive(false);
        summaryBoard.SetActive(false);
        blackTransparency.SetActive(true);
        summaryDisplay.createBill();
    }
    /** แสดงผลสรุปต้นทุน รายได้ กำไร */
    private void createSummary()
    {
        summaryBoard.SetActive(true);
        bill.SetActive(false);
        summaryDisplay.createSummary();
    }
    /** end this scene */
    private void EndingSummary()
    {
        SceneChanger.nextScene(12);
    }

}
