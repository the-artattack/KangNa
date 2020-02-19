using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RainAnimation : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite rainBackground;
    // Start is called before the first frame update
    void Start()
    {
        SimulateParameters.onRainTrigger += onRaining;
    }

    private void onRaining()
    {
        backgroundImage.sprite = rainBackground;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
