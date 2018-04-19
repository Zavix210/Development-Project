using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class TimelineController : SimulationComponentBase
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

    public override void OnReceivedMessage(Message message)
    {
        switch (message.Route)
        {
            case (int)MessageDestination.SIMULATION_PAUSE:
                {
                    // Pause the timeline
                    timeline.SetPauseState(true);
                }
                break;
            case (int)MessageDestination.SIMULATION_RESUME:
                {
                    // Pause the timeline
                    timeline.SetPauseState(false);
                }
                break;
            case (int)MessageDestination.SIMULATION_START:
                {
                    timeline.Reset();
                }
                break;
            case (int)MessageDestination.SIMULATION_END:
                {
                    timeline.Reset();
                }
                break;
            case (int)MessageDestination.DECISION_CHANGE: // A Scene change has occurred
                {
                    //timeline.Reset();
                }
                break;
        }
    }
}