using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InsectAnimation : MonoBehaviour
{
    public GameObject Insect;
    public GameObject Insecticide;
    public Animator insect1;
    public Animator insect2;
    public Animator insect3;
    public Animator insect4;
    public Animator insect5;
    public Animator insect6;

    public static event OnParameterTrigger onParameterUpdateTrigger;
    public delegate void OnParameterTrigger(SimulateParameters parameters);

    // Start is called before the first frame update
    void Start()
    {
        Insect.SetActive(false);     
    }

    // Update is called once per frame
    public void enable(string insectType, SimulateParameters parameters)
    {        
        animateInsect();
        InsectSolution(parameters);
    }
    private void animateInsect()
    {
        Insect.SetActive(true);
        insect1.SetBool("isPlay", true);
        insect2.SetBool("isPlay", true);
        insect3.SetBool("isPlay", true);
        insect4.SetBool("isPlay", true);
        insect5.SetBool("isPlay", true);
        insect6.SetBool("isPlay", true);
    }

    public void disable(string insectType)
    {
        Insect.SetActive(false);
        insect1.SetBool("isDisable", false);
        insect2.SetBool("isDisable", false);
        insect3.SetBool("isDisable", false);
        insect4.SetBool("isDisable", false);
        insect5.SetBool("isDisable", false);
        insect6.SetBool("isDisable", false);
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
