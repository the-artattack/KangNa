using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InsectAnimation : MonoBehaviour
{
    public GameObject Insect;
    public GameObject insectProtected;
    public Animator insect1;
    public Animator insect2;
    public Animator insect3;
    public Animator insect4;
    public Animator insect5;
    public Animator insect6;
    public Animator insect7;
    public bool insecticide;

    public static event onInsect onUseInsecticide;
    public delegate void onInsect(bool insecticide);

    public static event OnParameterTrigger onParameterUpdateTrigger;
    public delegate void OnParameterTrigger(SimulateParameters parameters);

    // Start is called before the first frame update
    void Start()
    {
        Insect.SetActive(false);     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enable(SimulateParameters parameters)
    {        
        animateInsect();
        InsectSolution(parameters);
    }
    private void animateInsect()
    {
        Insect.SetActive(true);
        insect1.SetBool("isInsect", true);
        insect2.SetBool("isInsect", true);
        insect3.SetBool("isInsect", true);
        insect4.SetBool("isInsect", true);
        insect5.SetBool("isInsect", true);
        insect6.SetBool("isInsect", true);
        insect7.SetBool("isInsect", true);
    }

    private void disableInsect()
    {
        Insect.SetActive(false);
        insect1.SetBool("isInsect", false);
        insect2.SetBool("isInsect", false);
        insect3.SetBool("isInsect", false);
        insect4.SetBool("isInsect", false);
        insect5.SetBool("isInsect", false);
        insect6.SetBool("isInsect", false);
        insect7.SetBool("isInsect", false);
    }
   

    private void protect()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("button value: " + buttonValue);

        disableInsect();        
        insectProtected.SetActive(true);
        Button btnA1 = insectProtected.transform.Find("btnA1").GetComponent<Button>();
        Button btnA2 = insectProtected.transform.Find("btnA2").GetComponent<Button>();
        btnA1.onClick.AddListener(useInsecticide);
        btnA2.onClick.AddListener(notUseInsecticide);        
    }

    private void notUseInsecticide()
    {
        disableInsect();        
        insectProtected.SetActive(false);
        insecticide = false;
        onUseInsecticide?.Invoke(insecticide);
        MainGame.onInsectTrigger -= enable;
    }

    private void useInsecticide()
    {
        disableInsect();        
        insectProtected.SetActive(false);
        insecticide = true;
        onUseInsecticide?.Invoke(insecticide);
        MainGame.onInsectTrigger -= enable;
    }

    private void notProtect()
    {
        string buttonValue = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("button value: " + buttonValue);

        disableInsect();       
        MainGame.onInsectTrigger -= enable;
    }

    public void InsectSolution(SimulateParameters parameters)
    {
        if (parameters.useInsecticide)
        {
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 2);
        }
        else if (!parameters.useInsecticide)
        {
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 5);
        }
        else
            parameters.RiceQuantity = eventHandler.RiceReduction(parameters.RiceQuantity, 10);

        onParameterUpdateTrigger?.Invoke(parameters);
    }
}
