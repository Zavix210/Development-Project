using SimulationSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionTimelineAction : TimelineActionBase
{
    public override void ExecuteAction()
    {
        Simulation sim = Simulation.Instance;
        SimulationController controller = sim.Controller;
    }
}