using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float turnDuration = 1f;
    public float fastForwardMultiplier = 5f;
    public bool paused;
    public bool fastForward;

    public delegate void onTimeAdvanceHandler();
    public static event onTimeAdvanceHandler OnTimeAdvance;

    float advanceTimer;
    // Start is called before the first frame update
    void Start()
    {
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
    }
    public void Play()
    {
        Debug.Log("Play");
        paused = false;
        fastForward = false;
    }

    public void FastForward()
    {
        Debug.Log("Fast forward");
        paused = false;
        fastForward = true;
    }
}
