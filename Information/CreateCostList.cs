using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCostList : MonoBehaviour
{
    public ScrollRect scrollView;
    public GameObject scrollContent;
    public GameObject scrollTextPrefeb;
    public ScrollSnapRect _scroll;
    private List<GameObject> objs = new List<GameObject>();
    private void Start()
    {
        objs.Clear();
        Debug.Log(FirebaseInit.Instance.riceType);
        if (FirebaseInit.Instance.riceType.Equals("ข้าวไวแสง"))
        {
            foreach (var key in CostList.CostInstance.sensitivityRice[0].Keys)
            {                
                 generateText(RiceName.GetRiceName(key), CostList.CostInstance.sensitivityRice[0][key].ToString());
                 Debug.Log(key);              
            }
        }
        else
        {
            foreach (var key in CostList.CostInstance.insensitivityRice[0].Keys)
            {
                generateText(RiceName.GetRiceName(key), CostList.CostInstance.insensitivityRice[0][key].ToString());
                Debug.Log(key);
            }
        }
    }

    public void generateText(string name, string price)
    {
        Debug.Log("GENERATE TEXT!");
        GameObject scrollObj = Instantiate(scrollTextPrefeb);
        scrollObj.transform.SetParent(scrollContent.transform, false);
        scrollObj.transform.Find("Price").gameObject.GetComponent<Text>().text = price;
        scrollObj.transform.Find("RiceName").gameObject.GetComponent<Text>().text = name;
        objs.Add(scrollObj);
    }    

    public void Buy()
    {
        int currentPage = _scroll.getSelectedPage();
        string riceName = objs[currentPage].transform.Find("RiceName").GetComponent<Text>().text;
        Debug.Log("current page: " + currentPage);
        Debug.Log(riceName);
        FirebaseInit.Instance._database.RootReference
                        .Child("Education")
                        .Child(FirebaseInit.Instance.auth.CurrentUser.UserId)
                        .Child("RiceName").SetValueAsync(riceName);

        //Set parameter : riceName on scene Simulation
        //Parameters.variableInstance.riceName = riceName;

        //Go to next scene
        SceneChanger.nextScene(FirebaseInit.Instance.CurrentScene+1);
    }
}
