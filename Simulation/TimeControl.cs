using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeControl : MonoBehaviour
{
    public float turnDuration = 1f;
    public float fastForwardMultiplier = 5f;
    public bool paused;
    public bool fastForward;

    public Sprite playImg;
    public Sprite pauseImg;
    public Sprite fastForwardImg;
    public Sprite playIdle;
    public Sprite pauseIdle;
    public Sprite fastForwardIdle;
    public Button playButton;
    public Button pauseButton;
    public Button fastForwardButton;

    public delegate void onTimeAdvanceHandler();
    public static event onTimeAdvanceHandler OnTimeAdvance;

    float advanceTimer;
    // Start is called before the first frame update
    void Start()
    {
        playButton.GetComponent<Image>().sprite = playImg;
        advanceTimer = turnDuration;
        SummaryCalculation.onSummary += Pause;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            advanceTimer -= Time.deltaTime * (fastForward ? fastForwardMultiplier : 1f);
            if (advanceTimer <= 0)
            {
                advanceTimer += turnDuration;
                OnTimeAdvance?.Invoke();
            }
        }
    }

    public void Step()
    {
        OnTimeAdvance?.Invoke();
    }

    public void Pause()
    {
        Debug.Log("Pause");
        paused = true;
        fastForward = false;
        pauseButton.GetComponent<Image>().sprite = pauseImg;
        playButton.GetComponent<Image>().sprite = playIdle;
        fastForwardButton.GetComponent<Image>().sprite = fastForwardIdle;
    }
    public void Play()
    {
        Debug.Log("Play");
        paused = false;
        fastForward = false;
        playButton.GetComponent<Image>().sprite = playImg;
        pauseButton.GetComponent<Image>().sprite = pauseIdle;
        fastForwardButton.GetComponent<Image>().sprite = fastForwardIdle;
    }

    public void FastForward()
    {
        Debug.Log("Fast forward");
        paused = false;
        fastForward = true;
        fastForwardButton.GetComponent<Image>().sprite = fastForwardImg;
        playButton.GetComponent<Image>().sprite = playIdle;
        pauseButton.GetComponent<Image>().sprite = pauseIdle;
    }
}
