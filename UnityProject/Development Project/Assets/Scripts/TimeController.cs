using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class TimeController : SimulationComponentBase, IUnityHook
{
    private TimeDisplay timeDisplay;
    private float currentTime;
    private float timeLimit;
    private bool active;
    private bool expiredMessageSent;

    public TimeController(SimulationController controller) : base(controller)
    {
        // Self-register to the unity messaging
        Simulation simulation = Simulation.Instance;
        simulation.AddHook(this);
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {
        TimeDisplay tDisplay = GameObject.FindObjectOfType<TimeDisplay>();
        if(tDisplay != null)
        {
            timeDisplay = tDisplay;
            tDisplay.Initialize(this);
        }
        else
        {
            Debug.LogError("Failed to find time display object");
        }
    }

    public override void OnReceivedMessage(Message message)
    {
        switch (message.Route)
        {
            case (int)MessageDestination.SIMULATION_START:
                {
                    StartTimer();
                }
                break;
            case (int)MessageDestination.SIMULATION_END:
                {
                    StopTimer();
                }
                break;
        }
    }

    public void Update(float deltaTime)
    {
        if (active)
        {
            currentTime += Time.deltaTime;

            // Check whether time has finished
            if (currentTime >= timeLimit && !expiredMessageSent)
            {
                // Flag the message as sent to prevent per-frame message sending
                expiredMessageSent = true;

                // Propagate a message to notify all components that the timer has expired
                Message message = new Message((int)MessageDestination.TIMER_EXPIRED, "", this);
                Controller.PropagateMessage(message);
            }
        }
    }

    private void StartTimer()
    {
        currentTime = 0.0f;
        active = true;
        expiredMessageSent = false;
    }

    private void StopTimer()
    {
        currentTime = 0.0f;
        active = false;
        expiredMessageSent = false;
    }

    public void SetTimeLimit(float timeLimit)
    {
        this.timeLimit = timeLimit;
    }

    public string GetFormattedDisplayTime()
    {
        // Format string for MINUTES:SECONDS:MILLISECONDS
        // string displayStr = string.Format("{0:D2}:{1:D2}:{2:D3}", span.Minutes.ToString("D2"), span.Seconds.ToString("D2"), span.Milliseconds.ToString("D3"));

        float remainder = GetNonNegativeRemainingTime();
        TimeSpan span = TimeSpan.FromSeconds(remainder);
        string displayStr = string.Format("{0:D2}:{1:D2}", span.Minutes.ToString("D2"), span.Seconds.ToString("D2"));
        return displayStr;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public float GetRemainingTime()
    {
        return timeLimit - currentTime;
    }

    public float GetNonNegativeRemainingTime()
    {
        return Mathf.Clamp(GetRemainingTime(), 0, Mathf.Infinity);
    }

    public bool HasTimeExpired()
    {
        return GetRemainingTime() <= 0.0f;
    }
}
