using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MoneyDisplay : MonoBehaviour
{
    public GameObject box1;
    public GameObject box2;
    private Text name1;
    private Text name2;

    private MoneyList myList;
    public GameObject list;

    private void Start()
    {
        myList = list.GetComponent<MoneyList>();
        box1.SetActive(false);
        box2.SetActive(false);
        name1 = box1.transform.Find("description").GetComponent<Text>();
        name2 = box2.transform.Find("description").GetComponent<Text>();
    }

    private float getCost(string type, string name)
    {
        return myList.getMoney(type, name).cost;
    }

    public void notifyMoney(string type, string name)
    {
        box1.SetActive(true);        
        name1.text = name + " -" + getCost(type, name) + " บาท/ไร่";
        StartCoroutine(close());
    }
    public void notifyMoney(string type1, string name1, string type2, string name2)
    {
        box1.SetActive(true);
        box2.SetActive(true);
        this.name1.text = name1 + " -" + getCost(type1, name1) + " บาท/ไร่";
        this.name2.text = name2 + " -" + getCost(type2, name2) + " บาท/ไร่";
        StartCoroutine(close());
    }

    IEnumerator close()
    {
        yield return new WaitForSeconds(5.0f);
        box1.SetActive(false);
        box2.SetActive(false);
    }
}
