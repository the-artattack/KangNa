using UnityEngine;
using UnityEngine.UI;

public class UpCommingRain : MonoBehaviour
{    
    public GameObject questionBox;
    public GameObject blackTransparency;

    // public GameObject rain;

    // Start is called before the first frame update
    void Start()
    {
        MainGame.onRainForecastTrigger += onRaining;
    }

    public void onRaining(SimulateParameters parameters)
    {        
        questionTrigger();
    }

    private void questionTrigger()
    {
        questionBox.SetActive(true);
        blackTransparency.SetActive(true);

        Button btnA1 = questionBox.transform.Find("btnA1").GetComponent<Button>();
        Button btnA2 = questionBox.transform.Find("btnA2").GetComponent<Button>();
        btnA1.onClick.AddListener(yes);
        btnA2.onClick.AddListener(no);
    }

    private void no()
    {
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);       
    }

    private void yes()
    {
        questionBox.SetActive(false);
        blackTransparency.SetActive(false);        
    }
}
