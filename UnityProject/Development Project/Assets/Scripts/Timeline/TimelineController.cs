using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class TimelineController : SimulationComponentBase, ITimelineListener
{
    private Timeline timeline;

    public TimelineController(SimulationController controller) : base(controller)
    {
        timeline = new Timeline();
    }

    public override bool IsMessageRouteValid(int route)
    {
        return true;
    }

    public override void OnInitialize()
    {

    }

    public void OnPause(Timeline timeline)
    {
        //Message message = new Message((int)MessageDestination.SIMULATION_PAUSE)
    }

    public override void OnReceivedMessage(Message message)
    {

    }

    public void OnResume(Timeline timeline)
    {
        throw new NotImplementedException();
    }

    public void OnStart(Timeline timeline)
    {
        throw new NotImplementedException();
    }

    public void OnStop(Timeline timeline)
    {
        throw new NotImplementedException();
    }
}