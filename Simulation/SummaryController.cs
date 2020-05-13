using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummaryController : MonoBehaviour
{
    public SummaryCalculation summary;

    public GameObject millBoard;
    public GameObject IncomeBoard;
    public GameObject bill;
    public GameObject summaryBoard;
    public GameObject blackTransparency;

    public Animator animator;
    public Button nextButton;
    public Button okButton;

    private void Start()
    {
        bill.SetActive(false);
        summaryBoard.SetActive(false);
        millBoard.SetActive(false);
        IncomeBoard.SetActive(false);
        blackTransparency.SetActive(false);
        nextButton.onClick.AddListener(createSummary);
        okButton.onClick.AddListener(EndingSummary);
        showMillBoard();
    }

    private void showMillBoard()
    {
        millBoard.SetActive(true);
        blackTransparency.SetActive(true);
        summary.createMillBoard();
        Invoke("playAnimation", 10.0f);
    }

    private void playAnimation()
    {
        millBoard.SetActive(false);
        blackTransparency.SetActive(false);
        animator.SetBool("isMove", true);
        StartCoroutine(showIncomeBoard());
    }

    private IEnumerator showIncomeBoard()
    {
        yield return new WaitForSeconds(6.8f);
        IncomeBoard.SetActive(true);
        blackTransparency.SetActive(true);
        Invoke("enableSummary", 10.0f);
    }
    private void enableSummary()
    {
        bill.SetActive(true);
        summaryBoard.SetActive(false);
        blackTransparency.SetActive(true);
        summary.createBill();
    }
    private void createSummary()
    {
        summaryBoard.SetActive(true);
        bill.SetActive(false);
        summary.createSummary();
    }
    private void EndingSummary()
    {
        SceneChanger.nextScene(12);
    }

}
