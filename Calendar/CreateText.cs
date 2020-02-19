using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateText : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollTextPrefeb;

    private void Start()
    {
      
        //scrollView.verticalNormalizedPosition = 1;
    }
    public void generateText(string forecast)
    {
        GameObject scrollObj = Instantiate(scrollTextPrefeb);
        scrollObj.transform.SetParent(scrollContent.transform, false);
        scrollObj.transform.gameObject.GetComponent<Text>().text = forecast.ToString();
    }


}
