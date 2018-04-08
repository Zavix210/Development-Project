﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimulationSystem;
using System;

public class VideoPlayer : SimulationComponentBase
{
    public VideoPlayer(SimulationController controller) : base(controller)
    {

    }

    public override bool IsMessageRouteValid(int route)
    {
        return route == (int)MessageDestination.DECISION_CHANGE;
    }

    public override void OnInitialize()
    {

    }

    public override void OnReceivedMessage(Message message)
    {
        // A decision was made (next video?)
        if(message.Route == (int)MessageDestination.DECISION_CHANGE)
        {
            // When a video has changed, this can be used to play the next video.

            // Consider how to load further videos here? Are all videos loaded into memory yet?
        }
    }
}
