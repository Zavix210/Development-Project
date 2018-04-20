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

    public void Update(float deltaTime)
    {
        if (active)
        {
            currentTime += Time.deltaTime;

            // Check whether time has finished
            if (currentTime >= timeLimit)
            {
                // TODO: TIMES UP
            }
        }
    }

    private void StartTimer()
    {
        currentTime = 0.0f;
        active = true;
    }

    private void StopTimer()
    {
        currentTime = 0.0f;
        active = false;
    }

    public void SetTimeLimit(float timeLimit)
    {
        this.timeLimit = timeLimit;
    }

    public string GetFormattedDisplayTime()
    { 
        float remainder = timeLimit - currentTime;
        TimeSpan span = TimeSpan.FromSeconds(remainder);
        string displayStr = string.Format("{0:D2}:{1:D2}", span.Minutes, span.Seconds);
        return displayStr;
    }
}
