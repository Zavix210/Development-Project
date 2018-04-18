using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class TimeController : SimulationComponentBase
{
    private float currentTime;
    private float timeLimit;
    private DateTime snapshot;
    private TimeDisplay timeDisplay;

    public TimeController(SimulationController controller) : base(controller)
    {
        timeLimit = 10 * 1000;
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
        switch(message.Route)
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

    public float QueryTime()
    {
        DateTime time = DateTime.Now;
        TimeSpan diff = time - snapshot;
        float ms = diff.Milliseconds;
        snapshot = time;

        currentTime = ms;

        // Calculate the actual remaining time from the current
        float diffTime = timeLimit - currentTime;
        return diffTime;
    }

    private void StartTimer()
    {
        // Init time as now
        snapshot = DateTime.Now;
        // Query the current time
        QueryTime();
    }

    private void StopTimer()
    {
        currentTime = 0.0f;
    }
}
