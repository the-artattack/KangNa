using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotRainAnimation : MonoBehaviour
{
    public Image backgroundImage;
    public Sprite normalBackground;
    // Start is called before the first frame update
    void Start()
    {
        SimulateParameters.onNotRainTrigger += onNotRaining;
    }

    private void onNotRaining()
    {
        backgroundImage.sprite = normalBackground;
    }
}
