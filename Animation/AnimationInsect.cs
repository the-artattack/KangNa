using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInsect : MonoBehaviour
{
    public GameObject Insect;
    public Animator insect1;
    public Animator insect2;
    public Animator insect3;
    public Animator insect4;
    public Animator insect5;
    public Animator insect6;
    public Animator insect7;

    // Start is called before the first frame update
    void Start()
    {
        Insect.SetActive(false);
        animateInsect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enable(SimulateParameters parameters)
    {
        animateInsect();
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
}
