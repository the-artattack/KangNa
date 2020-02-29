using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotRainAnimation : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite normalBackground;

    public static event OnParameterTrigger onParameterUpdateTrigger;
    public delegate void OnParameterTrigger(SimulateParameters parameters);

    // Start is called before the first frame update
    void Start()
    {
        MainGame.onNotRainTrigger += onNotRaining;
    }

    private void onNotRaining(SimulateParameters parameters)
    {
        backgroundImage.sprite = normalBackground;
        parameters.IsRain = false;

        onParameterUpdateTrigger?.Invoke(parameters);
    }
}
