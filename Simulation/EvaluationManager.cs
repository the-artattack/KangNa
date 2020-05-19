using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EvaluationManager : MonoBehaviour
{
    public Text level;

    public GameObject[] starsActivePanel;
    public GameObject[] starsNotActivePanel;

    private Evaluation evaluation;
    // Start is called before the first frame update
    private void Awake()
    {
        evaluation = GameObject.FindObjectOfType<Evaluation>();
    }
    void Start()
    {
        evaluation.LoadScore();
        resetStars();        
    }
    private void resetStars()
    {
        for(int i=0;i<4;i++)
        {
            starsNotActivePanel[i].SetActive(true);
            starsActivePanel[i].SetActive(false);
        }        
    }

    public void showStars()
    {
        for(int i=0;i<evaluation.level;i++)
        {
            starsActivePanel[i].SetActive(true);
        }
        showEvaluation();
    }   

    private void showEvaluation()
    {
        level.text = evaluation.getEvaluationString();        
    }

    public void newSimulation()
    {
        Debug.Log("New Simulation");
        SceneChanger.nextScene(2); //go to select mode (beginner or expert)

        //reset the money
        FirebaseInit.Instance.CurrentMoney = 100000;
    }
    public void exit()
    {
        Debug.Log("Exit App");
        Application.Quit();
    }

}
